

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using GraphQL;
using GraphQL.Execution;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;

namespace GatewayApi.GraphQL.Exntensions
{
    public static class GraphqlExtensions
    {
        private static readonly IEnumerable<string> EmptyStringArray = new string[0];

 

        public static IEnumerable<string> IsValidRegEx<T>(this IGraphType type, IValue valueAst, string regExPattern, ValidationContext context, string reportMessage = "")
        {
            if (type is NonNullGraphType nonNull)
                return IsValidRegEx<T>(nonNull.ResolvedType, valueAst, regExPattern, context, reportMessage);
            if (type is ListGraphType listType)
            {
                if (valueAst is ListValue list)
                    foreach (IValue item in list.Values)
                        return IsValidRegEx<T>(listType.ResolvedType, item, regExPattern, context, reportMessage);
                else
                    return IsValidRegEx<T>(listType.ResolvedType, valueAst, regExPattern, context, reportMessage);
            }

            if (type is IInputObjectGraphType inputType)
            {
                // Values pass as Variable
                if (valueAst is VariableReference)
                {
                    if (!(context.Inputs.FirstOrDefault().Value is Dictionary<string, object> dict)) return EmptyStringArray;
                    var fieldAstsFromVariable = dict.Select(pair => {
                        var fieldType = inputType.GetField(pair.Key)?.ResolvedType;
                        return new ObjectField(pair.Key, pair.Value.AstFromValue(context.Schema, fieldType));
                    }).ToList();
                    return IsValidRegEx<T>(type, new ObjectValue(fieldAstsFromVariable), regExPattern, context, reportMessage);
                }

                if (!(valueAst is ObjectValue objValue))
                    return EmptyStringArray;
                var fields = inputType.Fields.ToList();
                List<ObjectField> fieldAsts = objValue.ObjectFields.ToList();
                var errors = new List<string>();

                var validateFields = inputType.Fields.Where(x => x.HasMetadata(typeof(T).Name));
                if (validateFields.Any())
                {
                    foreach (var validate in validateFields)
                    {
                        var field = fieldAsts.Find(x => x.Name == validate.Name);
                        if (field == null) continue;
                        IValue validateValue = field?.Value;
                        if (!Regex.IsMatch(validateValue?.Value.ToString(), regExPattern))
                        { 
                            if(string.IsNullOrEmpty(reportMessage))
                                errors.Add($"In field \"{validate.Name}\": Must match /{regExPattern}/ but {validateValue?.Value} does not.");
                            else
                                errors.Add($"In field \"{validate.Name}\": {reportMessage}");
                        }

                    }
                }
                foreach (var field in fields)
                {
                    var fieldAst = fieldAsts.Find(x => x.Name == field.Name);
                    var result = IsValidRegEx<T>(field.ResolvedType, fieldAst?.Value, regExPattern, context, reportMessage);
                    errors.AddRange(result.Select(err => $"In field \"{field.Name}\": {err}"));
                }
                return errors;
            }
            return EmptyStringArray;
        }



    }
}
