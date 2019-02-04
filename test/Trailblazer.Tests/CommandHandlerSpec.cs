using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Trailblazer.Tests
{
    public class CommandHandlerSpec
    {
        [Fact]
        public async Task CanExecuteSampleCommand()
        {
            var commandHandlerPolicy = new Mock<SampleCommandPolicy>();

            commandHandlerPolicy
                .Setup(x => x.ValidateAsync(It.IsAny<SampleCommand>()))
                .ReturnsAsync(new PolicyValidationResult());
            
            var commandHandler = new SampleCommandHandler(commandHandlerPolicy.Object);

            var result = await commandHandler.HandleAsync(new SampleCommand("Willem"));

            result.Should().NotBeNull();
            result.Message.Should().BeEquivalentTo("Hello Willem");
        }

        [Fact]
        public async Task DoesNotProcessInvalidCommand()
        {
            var commandHandlerPolicy = new Mock<SampleCommandPolicy>();
            var validationResult = new PolicyValidationResult();

            await validationResult.AddDomainError("Argument", "Text is invalid", () => false);
            
            commandHandlerPolicy
                .Setup(x => x.ValidateAsync(It.IsAny<SampleCommand>()))
                .ReturnsAsync(validationResult);
            
            var commandHandler = new SampleCommandHandler(commandHandlerPolicy.Object);

            var result = await commandHandler.HandleAsync(new SampleCommand("Willem"));

            result.Should().NotBeNull();
            result.Message.Should().BeNull();
            result.Errors.Count().Should().Be(1);
        }

        public class SampleCommand
        {
            public SampleCommand(string argument)
            {
                Argument = argument;
            }

            public string Argument { get; set; }
        }

        public class SampleCommandResult: CommandResult
        {
            public string Message { get; set; }
        }
        
        public class SampleCommandPolicy: Policy<SampleCommand>
        {
            public override async Task<PolicyValidationResult> ValidateAsync(SampleCommand request)
            {
                var result = new PolicyValidationResult();
                return result;
            }
        }

        public class SampleCommandHandler : CommandHandler<SampleCommand, SampleCommandResult, SampleCommandPolicy>
        {
            public SampleCommandHandler(SampleCommandPolicy policy) : base(policy)
            {
            }

            protected override async Task<SampleCommandResult> ProcessAsync(SampleCommand request)
            {
                return new SampleCommandResult()
                {
                    Message = $"Hello {request.Argument}"
                };
            }
        }
    }
}