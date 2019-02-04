using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trailblazer
{
    /// <summary>
    /// Routes commands to the correct handlers in the application.
    /// </summary>
    public class CommandBus
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandBus"/>
        /// </summary>
        /// <param name="commandHandlers">Command handlers that should be registered with the command handler.</param>
        public CommandBus(IEnumerable<ICommandHandler> commandHandlers)
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
            
            foreach (var handler in commandHandlers)
            {
                _commandHandlers.Add(handler.RequestType, handler);
            }
        }

        /// <summary>
        /// Handles a command asynchronously.
        /// </summary>
        /// <param name="request">Incoming command data.</param>
        /// <typeparam name="TResponse">Type of response expected from the handler.</typeparam>
        /// <typeparam name="TRequest">Type of request to send to the handler.</typeparam>
        /// <returns>Returns the outcome of the command handler.</returns>
        /// <exception cref="KeyNotFoundException">Gets thrown when no command handler is registered for the specified request message.</exception>
        public async Task<TResponse> HandleAsync<TResponse, TRequest>(TRequest request)
            where TResponse : CommandResult
        {
            if (!_commandHandlers.TryGetValue(typeof(TRequest), out var handler))
            {
                throw new KeyNotFoundException("There's no command handler registered for this command.");
            }

            return (TResponse) await handler.HandleAsync(request);
        }
    }
}