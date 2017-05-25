using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace planetbuster.Test.TestUtil
{
    public static class TestUtilExtensions
    {
        public static void SetPrivateProperty(this object target, string propertyName, object value)
        {
            var type = target.GetType();
            var propertyInfo = type.GetProperty(propertyName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (propertyInfo == null)
            {
                throw new AssertFailedException($"Type {type.Name} does not have a property named {propertyName}");
            }
            propertyInfo.SetValue(target, value);
        }
    }
}