using Assets.Scripts.Util;

namespace Assets.Scripts
{
    public class GameManager : UnityBehavior, IGameManager
    {
        private static GameManager _instance = null;

        public static IGameManager Instance
        {
            get { return _instance; }
        }


        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }


        public GameManager()
        {
        }

        protected override void Update()
        {
        }

        protected override void OnDestroy()
        {
        }
    }
}