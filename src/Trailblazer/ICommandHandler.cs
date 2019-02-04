using System;
using System.Threading.Tasks;

namespace Trailblazer
{
    /// <summary>
    /// Implement this interface to build a generic command handler.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Handles a command.
        /// </summary>
        /// <param name="message">Incoming command data.</param>
        /// <returns>Output of the command handler.</returns>
        Task<CommandResult> HandleAsync(object message);
        
        /// <summary>
        /// Gets the type of command that is handled by the command handler.
        /// </summary>
        Type RequestType { get; }
    }
}