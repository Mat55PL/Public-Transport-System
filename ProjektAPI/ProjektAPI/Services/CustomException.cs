namespace ProjektAPI.Services;

public class CustomException
{
    [Serializable]
    public class InvalidDepartmentException : Exception
    {
        public InvalidDepartmentException() { }
        public InvalidDepartmentException(string message) : base(message) { }
        public InvalidDepartmentException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDepartmentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}