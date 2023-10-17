using System.Reflection;

namespace DataValidator.Validator
{
    public class DataValidatorTypeFactory
    {
        private static Dictionary<Type, Type> validators;
        public DataValidatorTypeFactory(ILogger<DataValidatorTypeFactory> logger)
        {
            validators = Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => type.GetCustomAttributes(typeof(DataValidatorAttribute), true).Any())
               .Select(i => new
               {
                   key = i.GetCustomAttribute<DataValidatorAttribute>().ControllerType,
                   value = i
               }).OrderBy(i => i.key.Name)
               .ToDictionary(i => i.key, i => i.value);

            var items = validators.Select(i => $"{"",-39}{i.Key.Name,-25} >> {i.Value.Name}");

            logger.LogInformation(string.Join("=", Enumerable.Repeat("=", 30)));
            logger.LogInformation($"Loading input data validators...{Environment.NewLine}{string.Join(Environment.NewLine, items)}");
            logger.LogInformation(string.Join("=", Enumerable.Repeat("=", 30)));
        }

        public Type GetValidatorType(Type controllerType)
        {
            validators.TryGetValue(controllerType, out var validator);
            return validator;
        }
    }
}