using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Trailblazer.Tests
{
    public class CommandBusSpec
    {
        [Fact]
        public async Task RoutesCommandsToHandlers()
        {
            var commandHandler = new Mock<ICommandHandler>();

            commandHandler
                .SetupGet(x => x.RequestType)
                .Returns(typeof(DummyCommand));

            commandHandler
                .Setup(x => x.HandleAsync(It.IsAny<object>()))
                .ReturnsAsync(new Mock<DummyCommandResult>().Object);
            
            var commandBus = new CommandBus(new[] { commandHandler.Object });
            await commandBus.HandleAsync<DummyCommandResult, DummyCommand>(new DummyCommand());
            
            commandHandler.Verify(x=>x.HandleAsync(It.IsAny<DummyCommand>()));
        }

        public class DummyCommand
        {
            
        }

        public class DummyCommandResult: CommandResult
        {
            
        }
    }
}