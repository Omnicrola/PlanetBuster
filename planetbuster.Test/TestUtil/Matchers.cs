using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace planetbuster.Test.TestUtil
{
    public class Matchers
    {
        public static List<T> EqualCollections<T>(List<T> expected)
        {
            return Arg.Is<List<T>>(actual => AssertEqualCollections(expected, actual));
        }

        private static bool AssertEqualCollections<T>(List<T> expected, List<T> actual)
        {
            Assert.AreEqual(expected, actual);
            return true;
        }
    }
}