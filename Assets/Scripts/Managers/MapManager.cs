using UnityEngine;

namespace XPACE {

    public class MapManager : MonoBehaviour {
        public ResourceController resourceController;
        public GameObject mapGO;
        public GameObject pointsParent;

        public static MapManager instance;
        private void Awake() {
            if (instance == null)
                instance = this;
        }
        public void GenerateMap() {
            // generate resources
            resourceController.Init();
        }
    }
}