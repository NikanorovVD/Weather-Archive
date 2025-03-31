namespace ServiceLayer.Models.Errors
{
    public class ValidationError
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }
        public ValidationError(Dictionary<string, IEnumerable<string>> errors)
        {
            Errors = errors;
        }
    }
}
