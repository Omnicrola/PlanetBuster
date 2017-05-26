using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class UnityBehavior : MonoBehaviour
    {
        #region Helper methods

        public void WhenTargetIsActive(MonoBehaviour behavior, Action doWork)
        {
            StartCoroutine(WaitForAction(behavior, doWork));
        }

        public void WhenTargetIsActive(GameObject targetObject, Action doWork)
        {
            StartCoroutine(WaitForAction(targetObject, doWork));
        }

        private IEnumerator WaitForAction(MonoBehaviour monoBehaviour, Action doWork)
        {
            yield return new WaitUntil(() => monoBehaviour.isActiveAndEnabled);
            doWork();
        }

        private IEnumerator WaitForAction(GameObject targetObject, Action doWork)
        {
            yield return new WaitUntil(() => targetObject.activeInHierarchy);
            doWork();
        }

        #endregion

        #region Lifecycle Behaviors
        #region editor
        /// <summary>
        /// Reset is called to initialize the script’s properties when it is first attached to the object and also when the Reset command is used.
        /// </summary>
        protected virtual void Reset() { }
        #endregion

        #region First Scene Load
        /// <summary>
        /// This function is always called before any Start functions and also just after a prefab is instantiated. (If a GameObject is inactive during start up Awake is not called until it is made active.)
        /// </summary>
        protected virtual void Awake()
        {
        }

        /// <summary>
        ///  (only called if the Object is active): This function is called just after the object is enabled. This happens when a MonoBehaviour instance is created, such as when a level is loaded or a GameObject with the script component is instantiated.
        /// </summary>
        protected virtual void OnEnable() { }

        #endregion

        #region Before First Frame
        /// <summary>
        /// Start is called before the first frame update only if the script instance is enabled.
        /// </summary>
        protected virtual void Start()
        {
        }
        #endregion

        #region In Between Frames
        /// <summary>
        /// This is called at the end of the frame where the pause is detected, effectively between the normal frame updates. One extra frame will be issued after OnApplicationPause is called to allow the game to show graphics that indicate the paused state.
        /// </summary>
        protected virtual void OnApplicationPause() { }
        #endregion

        #region Update Order
        /// <summary>
        /// FixedUpdate is often called more frequently than Update. It can be called multiple times per frame, if the frame rate is low and it may not be called between frames at all if the frame rate is high. All physics calculations and updates occur immediately after FixedUpdate. When applying movement calculations inside FixedUpdate, you do not need to multiply your values by Time.deltaTime. This is because FixedUpdate is called on a reliable timer, independent of the frame rate.
        /// </summary>
        protected virtual void FixedUpdate() { }
        /// <summary>
        /// Update is called once per frame. It is the main workhorse function for frame updates.
        /// </summary>
        protected virtual void Update() { }
        /// <summary>
        ///  LateUpdate is called once per frame, after Update has finished. Any calculations that are performed in Update will have completed when LateUpdate begins. A common use for LateUpdate would be a following third-person camera. If you make your character move and turn inside Update, you can perform all camera movement and rotation calculations in LateUpdate. This will ensure that the character has moved completely before the camera tracks its position.
        /// </summary>
        protected virtual void LateUpdate() { }
        #endregion

        #region Rendering
        /// <summary>
        /// Called before the camera culls the scene. Culling determines which objects are visible to the camera. OnPreCull is called just before culling takes place.
        /// </summary>
        protected virtual void OnPreCull() { }

        /// <summary>
        /// Called when an object becomes visible/invisible to any camera.
        /// </summary>
        protected virtual void OnBecameVisible() { }

        /// <summary>
        /// Called when an object becomes visible/invisible to any camera.
        /// </summary>
        protected virtual void OnBecameInvisible() { }

        /// <summary>
        /// Called once for each camera if the object is visible.
        /// </summary>
        protected virtual void OnWillRenderObject() { }

        /// <summary>
        /// Called before the camera starts rendering the scene.
        /// </summary>
        protected virtual void OnPreRender() { }

        /// <summary>
        /// Called after all regular scene rendering is done. You can use GL class or Graphics.DrawMeshNow to draw custom geometry at this point.
        /// </summary>
        protected virtual void OnRenderObject() { }

        /// <summary>
        /// Called after a camera finishes rendering the scene.
        /// </summary>
        protected virtual void OnPostRender() { }

        /// <summary>
        /// Called multiple times per frame in response to GUI events. The Layout and Repaint events are processed first, followed by a Layout and keyboard/mouse event for each input event.
        /// </summary>
        protected virtual void OnGUI() { }

        /// <summary>
        /// Used for drawing Gizmos in the scene view for visualisation purposes.
        /// </summary>
        protected virtual void OnDrawGizmos() { }
        #endregion

        #region When Object is destroyed
        /// <summary>
        /// This function is called after all frame updates for the last frame of the object’s existence (the object might be destroyed in response to Object.Destroy or at the closure of a scene).
        /// </summary>
        protected virtual void OnDestroy() { }
        #endregion

        #region Application Shutdown
        /// <summary>
        /// This function is called on all game objects before the application is quit. In the editor it is called when the user stops playmode.
        /// </summary>
        protected virtual void OnApplicationQuit() { }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected virtual void OnDisable() { }

        #endregion
        #endregion
    }
}