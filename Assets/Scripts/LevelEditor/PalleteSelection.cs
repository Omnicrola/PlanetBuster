using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class PalleteSelection
    {
        public PalleteSelection(int type, Sprite sprite)
        {
            Type = type;
            Sprite = sprite;
        }

        public int Type { get; private set; }
        public Sprite Sprite { get; private set; }
    }
}