namespace DotNet.Testcontainers.Tests.Unit.Containers
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using DotNet.Testcontainers.Containers.WaitStrategies;
  using Xunit;

  public class WaitUntilOperationIsSucceeded
  {
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    public async Task UntilMaxRepeats(int maxCallCount, int expectedCallsCount)
    {
      // Given
      var callCounter = 0;

      // When
      await Assert.ThrowsAsync<TimeoutException>(async () =>
      {
        var wait = Wait.ForUnixContainer().UntilOperationIsSucceeded(() =>
        {
          callCounter += 1;
          return false;
        }, maxCallCount);

        await WaitStrategy.WaitUntil(() => wait.Build().Skip(1).First().Until(null, string.Empty));
      });

      // Then
      Assert.Equal(expectedCallsCount, callCounter);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(10, 1)]
    [InlineData(10, 2)]
    [InlineData(10, 5)]
    [InlineData(10, 7)]
    public async Task UntilSomeRepeats(int maxCallCount, int expectedCallsCount)
    {
      // Given
      var callCounter = 0;

      // When
      var wait = Wait.ForUnixContainer().UntilOperationIsSucceeded(() => ++callCounter >= expectedCallsCount, maxCallCount);
      await WaitStrategy.WaitUntil(() => wait.Build().Skip(1).First().Until(null, string.Empty));

      // Then
      Assert.Equal(expectedCallsCount, callCounter);
    }
  }
}
