namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Tweening
{
	internal interface ITweenValue
	{
		void TweenValue(float floatPercentage);
		bool ignoreTimeScale { get; }
		float duration { get; }
		TweenEasing easing { get; }
		bool ValidTarget();
		void Finished();
	}
}