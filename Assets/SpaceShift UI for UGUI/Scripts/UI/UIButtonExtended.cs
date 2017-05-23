using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.UI
{
	[AddComponentMenu("UI/Button Extended", 58)]
	public class UIButtonExtended : Button {
		
		public enum VisualState
		{
			Normal,
			Highlighted,
			Pressed,
			Disabled
		}
		
		[Serializable]
		public class StateChangeEvent : UnityEvent<VisualState, bool> { }
		
		/// <summary>
		/// The on state change event.
		/// </summary>
		public StateChangeEvent onStateChange = new StateChangeEvent();
		
		protected override void OnDisable()
		{
			base.OnDisable();
			
			// Invoke the state change event
			if (this.onStateChange != null)
				this.onStateChange.Invoke(VisualState.Disabled, true);
		}
		
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			
			// Invoke the state change event
			if (this.onStateChange != null)
				this.onStateChange.Invoke((VisualState)state, instant);
		}
	}
}