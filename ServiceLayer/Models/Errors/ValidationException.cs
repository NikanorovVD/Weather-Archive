namespace ServiceLayer.Models.Errors
{
    public class ValidationException: Exception
    {
        public ValidationError Error { get; private set; }
        public ValidationException(ValidationError error) {
            Error = error;
        }
    }
}
