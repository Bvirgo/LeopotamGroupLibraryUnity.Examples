using LeopotamGroup.Common;
using LeopotamGroup.Fx;
using UnityEngine;

namespace LeopotamGroup.Examples.EditorHelpers.FadeManagerTest {
    public class FadeManagerTest : MonoBehaviour {
        Color _color1 = Color.black;
        Color _color2 = Color.clear;

        bool _isLocked;

        void Start () {
            Services.Get<FadeManager> (true);
        }

        void OnGUI () {
            if (!_isLocked) {
                if (GUILayout.Button ("Fade in/ Fade out")) {
                    _isLocked = true;
                    Services.Get<FadeManager> ().Process (_color1, _color2, 1f, () => {
                        var t = _color1;
                        _color1 = _color2;
                        _color2 = t;
                        _isLocked = false;
                    });
                }
            }
        }
    }
}