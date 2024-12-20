﻿using FluentAssertions;
using PingX.Helpers;
using PingX.Interfaces;


namespace PingX.Tests.Helpers
{
    public class InputValidatorTests
    {
        [Theory]
        [InlineData(new string[] { "192.168.1.1", "10.0.0.1" }, new string[] { "192.168.1.1", "10.0.0.1" })]
        [InlineData(new string[] { "192.168.1.1", "192.168.1.1" }, new string[] { "192.168.1.1" })]
        [InlineData(new string[] { "invalid-ip", "192.168.1.1" }, new string[] { "192.168.1.1" })]
        [InlineData(new string[] { "invalid-ip", "", "192.168.1.1", "192.168.88.ip" }, new string[] { "192.168.1.1" })]
        [InlineData(new string[] { "127.0.0.1", "invalid-ip", "also-invalid" }, null)]
        [InlineData(new string[] { }, null)]
        [InlineData(null, null)]
        public void ValidateIPAddresses_ShouldReturnExpectedResults(string[] input, string[] expectedOutput)
        {
            // Arrange
            IInputValidator inputValidator = new InputValidator();

            // Act
            var result = inputValidator.ValidateIPAddresses(input);

            // Assert
            if (expectedOutput == null)
            {
                result.Should().BeNull();
            }
            else
            {
                result.Should().NotBeNull();
                result.Should().HaveCount(expectedOutput.Length);
                result.Should().BeEquivalentTo(expectedOutput);
            }
        }
    }
}