using System.Reflection;
using NSubstitute;
using NUnit.Framework;

namespace planetbuster.Test.TestUtil
{
    public abstract class TestBase
    {

        [TearDown]
        public void BaseTeardown()
        {
        }


        private static void SetStaticField<T>(string fieldName, object valueToSet)
        {
            var type = typeof(T);
            var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            if (fieldInfo == null)
            {
                throw new AssertionException("Type " + type.Name + " does not have a field named " + fieldName);
            }
            fieldInfo.SetValue(null, valueToSet);
        }
    }
}