using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class PlanetPalleteSelection : UnityBehavior
    {
        public GameObject Camera;
        public GameObject EditorManager;
        public Sprite Selection;
        public int Type;

        private Camera _camera;

        protected override void Start()
        {
            _camera = Camera.GetComponent<Camera>();
        }

        protected override void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var screenPointToRay = _camera.ScreenPointToRay(Input.mousePosition);
                var raycastHit2D = Physics2D.GetRayIntersection(screenPointToRay);
                if (raycastHit2D.transform != null && raycastHit2D.transform.gameObject == gameObject)
                {
                    Debug.Log("Selected : " + Selection.name + " (" + Type + ")");
                    EditorManager.GetComponent<LevelEditorManager>().CurrentPalletSelection = new PalleteSelection(Type, Selection);
                }
            }
        }
    }
}