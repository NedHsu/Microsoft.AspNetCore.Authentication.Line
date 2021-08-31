using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Line;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Text.Encodings.Web;
using Xunit;

namespace Microsoft.AspNetCore.Authentication.LineTests
{
    public class LineHandlerTests
    {
        private IOptionsMonitor<LineOptions> subOptionsMonitor;
        private ILoggerFactory subLoggerFactory;
        private UrlEncoder subUrlEncoder;
        private ISystemClock subSystemClock;

        public LineHandlerTests()
        {
            this.subOptionsMonitor = Substitute.For<IOptionsMonitor<LineOptions>>();
            this.subLoggerFactory = Substitute.For<ILoggerFactory>();
            this.subUrlEncoder = Substitute.For<UrlEncoder>();
            this.subSystemClock = Substitute.For<ISystemClock>();
        }

        private LineHandler CreateLineHandler()
        {
            return new LineHandler(
                this.subOptionsMonitor,
                this.subLoggerFactory,
                this.subUrlEncoder,
                this.subSystemClock);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var lineHandler = this.CreateLineHandler();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
