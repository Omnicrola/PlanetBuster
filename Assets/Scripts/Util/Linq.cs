namespace Assets.Scripts.Util
{
    public static class Linq
    {
        public static bool IsNotNull<T>(T target)
        {
            return target != null;
        }
    }
}