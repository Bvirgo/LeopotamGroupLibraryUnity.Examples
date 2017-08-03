using LeopotamGroup.Common;
using UnityEngine;

namespace LeopotamGroup.Examples.Common.SingletonTest {
    public class SingletonTest : MonoBehaviour {
        void Start () {
            Services.Get<MySingletonManager> (true).Test ();
            Debug.Log ("MySingletonManager.GetStringParameter: " + Services.Get<MySingletonManager> ().GetStringParameter ());
        }

        void OnDestroy () {
            // Dont forget to check Services.IsTypeRegistered<T> () at any OnDestroy method (it can be
            // already killed before, execution order not defined), otherwise new instance of singleton class
            // will be created and unity throw exception about it.
            if (Services.IsTypeRegistered<MySingletonManager> ()) {
                Debug.Log ("MySingletonManager still alive!");
            } else {
                Debug.Log ("MySingletonManager already killed!");
            }
        }
    }
}