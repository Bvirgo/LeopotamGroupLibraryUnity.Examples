using LeopotamGroup.Common;
using LeopotamGroup.Events;
using LeopotamGroup.SystemUi.Actions;
using UnityEngine;

namespace LeopotamGroup.Examples.SystemUi.ActionsTest {
    public class ActionsTest : MonoBehaviour {
        [SerializeField]
        string _onClickFilteredGroup;

        int _onClickFilteredGroupId;

        void Awake () {
            // save groupId hash for fast filtering, we dont need to calculate hash each time for performance reason.
            _onClickFilteredGroupId = _onClickFilteredGroup.GetHashCode ();
        }

        void OnEnable () {
            // Subscribe to scene events pipeline.
            Singleton.Get<UnityEventBus> ().Subscribe<UiClickActionData> (OnClick);
            Singleton.Get<UnityEventBus> ().Subscribe<UiDragActionData> (OnDrag);
            Singleton.Get<UnityEventBus> ().Subscribe<UiPressActionData> (OnPress);
            Singleton.Get<UnityEventBus> ().Subscribe<UiReleaseActionData> (OnRelease);
            Singleton.Get<UnityEventBus> ().Subscribe<UiEnterActionData> (OnEnter);
            Singleton.Get<UnityEventBus> ().Subscribe<UiExitActionData> (OnExit);
            Singleton.Get<UnityEventBus> ().Subscribe<UiSelectActionData> (OnSelect);
            Singleton.Get<UnityEventBus> ().Subscribe<UiDeselectActionData> (OnDeselect);
            Singleton.Get<UnityEventBus> ().Subscribe<UiScrollActionData> (OnScroll);
        }

        void OnDisable () {
            // Unsubscribe to scene events pipeline. We should check first - is pipeline still exists or already killed?
            // If you want to change scene - you can ignore unsibscription, UnityEventBus will try to do it automatically.
            // But better to cleanup it correctly in right way and at right time.
            if (Singleton.IsTypeRegistered<UnityEventBus> ()) {
                // You can set second parameter to true if you want to decrease memory allocation
                // for same events at current scene.
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiClickActionData> (OnClick, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiDragActionData> (OnDrag, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiPressActionData> (OnPress, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiReleaseActionData> (OnRelease, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiEnterActionData> (OnEnter, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiExitActionData> (OnExit, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiSelectActionData> (OnSelect, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiDeselectActionData> (OnDeselect, true);
                Singleton.Get<UnityEventBus> ().Unsubscribe<UiScrollActionData> (OnScroll, true);
            }
        }

        bool OnClick (UiClickActionData data) {
            // We can filter messages based on logical group id.
            if (data.GroupId == _onClickFilteredGroupId) {
                Debug.Log ("OnClick: " + data.Receiver.name);
            } else {
                Debug.Log ("OnClick: " + data.Receiver.name + ", but we will skip it.");
            }
            // We should return result - is event processed and should not be passed to other listeners.
            // Always return false, or check order of listeners subscription for proper interrupt processing of event.
            return false;
        }

        bool OnDrag (UiDragActionData data) {
            Debug.Log ("OnDrag: " + data.EventData.delta);
            var pos = data.Receiver.transform.position;
            pos += new Vector3 (data.EventData.delta.x, data.EventData.delta.y, 0f);
            data.Receiver.transform.position = pos;
            return false;
        }

        bool OnRelease (UiReleaseActionData data) {
            Debug.Log ("OnRelease: " + data.Receiver.name);
            return false;
        }

        bool OnPress (UiPressActionData data) {
            Debug.Log ("OnPress: " + data.Receiver.name);
            return false;
        }

        bool OnExit (UiExitActionData data) {
            Debug.Log ("OnExit: " + data.Receiver.name);
            return false;
        }

        bool OnEnter (UiEnterActionData data) {
            Debug.Log ("OnEnter: " + data.Receiver.name);
            return false;
        }

        bool OnDeselect (UiDeselectActionData data) {
            Debug.Log ("OnDeselect: " + data.Receiver.name);
            return false;
        }

        bool OnSelect (UiSelectActionData data) {
            Debug.Log ("OnSelect: " + data.Receiver.name);
            return false;
        }

        bool OnScroll (UiScrollActionData data) {
            Debug.Log ("OnScroll: " + data.Receiver.name);
            return false;
        }
    }
}