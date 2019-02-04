using System.Threading.Tasks;

namespace Trailblazer
{
    /// <summary>
    /// Defines a policy for validating input of a command handler.
    /// </summary>
    /// <typeparam name="TRequest">Type of object to validate.</typeparam>
    public abstract class Policy<TRequest>
    {
        /// <summary>
        /// Validates the input using the rules defined in this policy.
        /// </summary>
        /// <param name="request">Request to validate.</param>
        /// <returns>Returns the outcome of the validation operation.</returns>
        public abstract Task<PolicyValidationResult> ValidateAsync(TRequest request);
    }
}