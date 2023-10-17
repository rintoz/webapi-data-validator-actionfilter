namespace DataValidator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataValidatorAttribute : Attribute
    {
        public Type ControllerType { get; }

        public DataValidatorAttribute(Type controllerType)
        {
            ControllerType = controllerType;
        }
    }
}