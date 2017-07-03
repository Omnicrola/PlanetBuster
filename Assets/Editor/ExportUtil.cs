using Assets.Scripts.Models;

namespace Assets.Editor
{
    public static class ExportUtil
    {
        public static string ConstructFilename(int ordinalNumber)
        {
            return "level-" + ordinalNumber.ToString("0000") + ".bin";
        }
    }
}