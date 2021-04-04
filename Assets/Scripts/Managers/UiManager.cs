using UnityEngine;

namespace XPACE {
    public class UiManager : MonoBehaviour {
        public static UiManager instance;
        private void Awake() {
            if (instance == null)
                instance = this;
        }
        [SerializeField] private GameObject ownerPlayer;
        public GameObject GetOwnerPlayer() { return ownerPlayer; }

        private void Start() {
            //this.transform.parent = null;
        }
    }
}