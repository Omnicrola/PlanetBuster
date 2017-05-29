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

        public static void CallPrivateMethod(this object target, string methodName, params object[] arguments)
        {
            var targetType = target.GetType();
            var methodInfo = targetType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new AssertFailedException(string.Format("Type {0} has no method named {1}", targetType.Name, methodName));
            }
            methodInfo.Invoke(target, arguments);

        }
    }
}