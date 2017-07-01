using System;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test
{
    [TestFixture]
    public class ScratchpadTest : TestBase
    {
        [Test]
        public void TestSOMETHING()
        {
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                var value = random.NextDouble() * .1 + .5f;
                Console.WriteLine(value);
            }
        }
    }
}