using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trailblazer
{
    /// <summary>
    /// Contains all the results of the policy validation operation.
    /// When no errors are found this result is considered valid.
    /// </summary>
    public class PolicyValidationResult
    {
        private readonly List<DomainError> _errors;

        /// <summary>
        /// Initializes a new instance of <see cref="PolicyValidationResult"/>.
        /// </summary>
        public PolicyValidationResult()
        {
            _errors = new List<DomainError>();
        }

        /// <summary>
        /// Gets the errors raised by the policy.
        /// </summary>
        /// <value>An enumerable list of <see cref="DomainError"/> objects.</value>
        public IEnumerable<DomainError> Errors => _errors;

        /// <summary>
        /// Gets whether the outcome of the policy validation operation is considered valid.
        /// </summary>
        /// <value><c>True</c> when there are no errors; Otherwise <c>False</c>.</value>
        public bool Isvalid => _errors.Count == 0;

        /// <summary>
        /// Records a domain error when the specified condition is not met.
        /// </summary>
        /// <param name="field">Field the error should be recorded for.</param>
        /// <param name="message">Message to record for the user.</param>
        /// <param name="condition">Condition that must be valid.</param>
        /// <returns>Returns an awaitable task.</returns>
        public Task AddDomainError(string field, string message, Func<bool> condition)
        {
            if (!condition())
            {
                _errors.Add(new DomainError(field, message));
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Records a domain error when the specified condition is not met.
        /// </summary>
        /// <param name="field">Field the error should be recorded for.</param>
        /// <param name="message">Message to record for the user.</param>
        /// <param name="condition">Condition that must be valid.</param>
        /// <returns>Returns an awaitable task.</returns>
        public async Task AddDomainError(string field, string message, Func<Task<bool>> condition)
        {
            var isRuleValid = await condition();

            if (!isRuleValid)
            {
                _errors.Add(new DomainError(field, message));
            }
        }
    }
}