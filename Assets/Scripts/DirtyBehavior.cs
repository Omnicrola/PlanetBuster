namespace Assets.Scripts
{
    public abstract class DirtyBehavior<T> : UnityBehavior
    {
        private bool _isDirty;
        private T _model;

        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                _isDirty = true;
            }
        }

        protected override void Start()
        {
            _isDirty = true;
        }

        protected override void Update()
        {
            if (_isDirty && isActiveAndEnabled)
            {
                DirtyUpdate();
                _isDirty = false;
            }
        }

        protected abstract void DirtyUpdate();
    }
}