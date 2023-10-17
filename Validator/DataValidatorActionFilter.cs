using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace DataValidator.Validator
{
    public class DataValidatorActionFilter : IActionFilter
    {
        private readonly DataValidatorTypeFactory factory;
        private readonly ILogger<DataValidatorActionFilter> logger;

        public DataValidatorActionFilter(DataValidatorTypeFactory factory, ILogger<DataValidatorActionFilter> logger)
        {
            this.factory = factory;
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.ActionDescriptor.RouteValues["controller"];
            var validatorType = factory.GetValidatorType(context.Controller.GetType());

            if (validatorType == null)
            {
                logger.LogDebug($"Validation class with attribute '{typeof(DataValidatorAttribute)}' not defined for Controller : {controllerName}!");
                return;
            }

            string methodName = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.Name;
            MethodInfo methodInfo = validatorType.GetMethod(methodName);

            if (methodInfo == null)
            {
                logger.LogDebug($"Validation method not defined for '{controllerName}.{methodName}'!");
                return;
            }

            var validatorInstance = Activator.CreateInstance(validatorType);
            try
            {
                object[] arguments = GetArguments(methodInfo, context.ActionArguments);
                methodInfo.Invoke(validatorInstance, arguments);
                (validatorInstance as BaseDataValidator)?.Validate();
            }
            catch (TargetParameterCountException pex)
            {
                logger.LogError($"Validator failed for {methodName}!{Environment.NewLine}{pex.Message}");
            }
            catch (AggregateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private static object[] GetArguments(MethodInfo methodInfo, IDictionary<string, object> actionArguments)
        {
            var parameters = methodInfo.GetParameters();

            if (parameters.Length == actionArguments.Count)
            {
                return actionArguments.Values.ToArray();
            }

            if (parameters.Length <= 0)
            {
                return Array.Empty<object>();
            }

            var arguments = new List<object>();
            foreach (var item in parameters)
            {
                object data = actionArguments.TryGetValue(item.Name, out object value) ? value : null;
                arguments.Add(data);
            }

            return arguments.ToArray();
        }
    }
}