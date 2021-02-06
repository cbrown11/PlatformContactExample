using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayApi.GraphQL.Exntensions;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;

namespace Platform.GraphQL.ValidationRules
{
    public class NonEmptyStringValidationRule : IValidationRule
    {
        public Task<INodeVisitor> ValidateAsync(ValidationContext context)
        {
            return Task.FromResult((INodeVisitor)new EnterLeaveListener(_ =>
            {
                _.Match<Argument>(argAst =>
                {
                    var argDef = context.TypeInfo.GetArgument();
                    if (argDef == null) return;
                    var type = argDef.ResolvedType;
                    var errors = type.IsValidRegEx<NonEmptyStringValidationRule>(argAst.Value, @"^(?!\s*$).+", context,
                        "Empty string or has white space at the begin").ToList();
                    foreach (var error in errors)
                    {
                        var validationError = new ValidationError(
                                               context.OriginalQuery,
                                               "Empty or Whitespace",
                                               error);
                        context.ReportError(validationError);
                    }
                });
            }));
        }
    }
}