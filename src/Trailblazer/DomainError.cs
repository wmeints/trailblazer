namespace Trailblazer
{
    /// <summary>
    /// Domain errors tell the user that there's a problem with the operation that they want to perform.
    /// The domain error may point to a specific field but it's not required. When no field is specified
    /// the error is assumed to be on the object level.
    /// </summary>
    public class DomainError
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DomainError"/>
        /// </summary>
        /// <param name="field">Field the error occurred on.</param>
        /// <param name="message">Message to display.</param>
        public DomainError(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// Gets the message to display.
        /// </summary>
        public string Message { get; }
    }
}