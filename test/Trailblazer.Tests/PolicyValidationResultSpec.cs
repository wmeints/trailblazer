using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Trailblazer.Tests
{
    public class PolicyValidationResultSpec
    {
        [Fact]
        public async Task CanRecordAsyncErrorsAsynchronously()
        {
            var result = new PolicyValidationResult();

            var field = "SomeField";
            var message = "SomeError";
            
            await result.AddDomainError(field, message, () => Task.FromResult(false));

            result.Isvalid.Should().BeFalse();
            result.Errors.Should().ContainSingle(x => x.Field == field && x.Message == message);
        }

        [Fact]
        public async Task CanRecordErrorsSynchonously()
        {
            var result = new PolicyValidationResult();

            var field = "SomeField";
            var message = "SomeError";
            
            await result.AddDomainError(field, message, () => false);

            result.Isvalid.Should().BeFalse();
            result.Errors.Should().ContainSingle(x => x.Field == field && x.Message == message);
        }
    }
}