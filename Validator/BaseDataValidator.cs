namespace DataValidator.Validator
{
    public abstract class BaseDataValidator
    {
        private readonly List<Exception> exceptions = new();

        protected BaseDataValidator()
        {
        }

        protected internal void AddErrorMessage(string message)
        {
            exceptions.Add(new Exception(message));
        }

        protected internal void AddException(Exception exception)
        {
            exceptions.Add(exception);
        }

        public void Validate()
        {
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}