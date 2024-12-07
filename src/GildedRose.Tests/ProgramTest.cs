using GildedRoseKata;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

namespace GildedRoseTests
{
    /// <summary>
    /// Runs the GildedRose program and ensures that the output matches the golden master
    /// stored in the project directory in the format:
    /// <code>{CLASS_NAME}.{TEST_NAME}.verified.txt</code>
    /// 
    /// (See https://github.com/VerifyTests/Verify)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ProgramTest
    {
        private readonly StringBuilder _capturedOutput;

        public ProgramTest()
        {
            _capturedOutput = new StringBuilder();
            Console.SetOut(new StringWriter(_capturedOutput));
        }

        [Fact]
        public Task GivenAnArgumentOf30_WhenCallingMain_ThenOutputMatchesGoldenMaster()
        {
            // Given
            const string argument = "30";

            // When
            Program.Main([argument]);

            // Then
            return Verifier.Verify(_capturedOutput.ToString());
        }
        
        [Fact]
        public Task GivenAnArgumentOf20_WhenCallingMain_ThenOutputMatchesGoldenMaster()
        {
            // Given
            const string argument = "20";

            // When
            Program.Main([argument]);

            // Then
            return Verifier.Verify(_capturedOutput.ToString());
        }
    }
}
