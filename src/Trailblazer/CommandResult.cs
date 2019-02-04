using System.Collections.Generic;

namespace Trailblazer
{
    /// <summary>
    /// Records the outcome of a command.
    /// </summary>
    public abstract class CommandResult
    {
        /// <summary>
        /// Gets or sets the errors encountered during processing of the operation.
        /// </summary>
        public IEnumerable<DomainError> Errors { get; set;  }
        
    }
}