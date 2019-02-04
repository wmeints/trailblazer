using System;
using System.Threading.Tasks;

namespace Trailblazer
{
    /// <summary>
    /// Classes derived from this type implement a command handler.
    /// </summary>
    /// <typeparam name="TRequest">Type of command to handle.</typeparam>
    /// <typeparam name="TResponse">Type of response to send.</typeparam>
    /// <typeparam name="TPolicy">Type of policy to implement for validating input data.</typeparam>
    public abstract class CommandHandler<TRequest, TResponse, TPolicy>: ICommandHandler
        where TResponse : CommandResult, new()
        where TPolicy : Policy<TRequest>
    {
        private readonly TPolicy _policy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler{TRequest, TResponse, TPolicy}"/> class.
        /// </summary>
        /// <param name="policy">Policy to use for validating the input of this command handler.</param>
        public CommandHandler(TPolicy policy)
        {
            _policy = policy;
        }

        /// <summary>
        /// Handles the incoming command.
        /// </summary>
        /// <param name="request">Request data to process.</param>
        /// <returns>Returns the result of the operation.</returns>
        public virtual async Task<TResponse> HandleAsync(TRequest request)
        {
            var validationResult = await _policy.ValidateAsync(request);

            if (validationResult.Isvalid)
            {
                return await ProcessAsync(request);
            }

            var result = new TResponse
            {
                Errors = validationResult.Errors
            };

            return result;
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="request">Incoming command data.</param>
        /// <returns>Returns the result of the operation.</returns>
        protected abstract Task<TResponse> ProcessAsync(TRequest request);

        /// <inheritdoc />
        async Task<CommandResult> ICommandHandler.HandleAsync(object message)
        {
            return await HandleAsync((TRequest) message);
        }

        /// <inheritdoc/>
        Type ICommandHandler.RequestType => typeof(TRequest);
    }
}