using Assets.Scripts.Models;

namespace Assets.Scripts.LevelEditor
{
    public interface IGridEditorSettings
    {
        LevelSummary GetExportData();
        void SetLevelData(LevelSummary levelSummary);
    }
}