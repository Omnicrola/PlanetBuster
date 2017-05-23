using Assets.SpaceShift_UI_for_UGUI.Scripts.Databases;
using Assets.SpaceShift_UI_for_UGUI.Scripts.UI.Icon_Slots_System;
using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Test_Scripts
{
    public class Test_UISpellSlot_Assign : MonoBehaviour {

        public UISpellSlot slot;
        public UISpellDatabase spellDatabase;
        public int assignSpell;
	
        void Awake()
        {
            if (this.slot == null)
                this.slot = this.GetComponent<UISpellSlot>();
        }
	
        void Start()
        {
            if (this.slot == null || this.spellDatabase == null)
            {
                this.Destruct();
                return;
            }
		
            this.slot.Assign(this.spellDatabase.GetByID(this.assignSpell));
            this.Destruct();
        }
	
        private void Destruct()
        {
            DestroyImmediate(this);
        }
    }
}
