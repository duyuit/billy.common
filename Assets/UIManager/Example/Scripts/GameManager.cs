using UnityEngine;

namespace BlitzyUI.UIExample
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }


        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Start() {
            // Push the example menu screen immediately.
            //UIManager.Instance.QueuePush(ScreenId_ExampleMenu, null, "ExampleMenuScreen", null);
        }
    }
}
