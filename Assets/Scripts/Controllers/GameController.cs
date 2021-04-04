using UnityEngine;
using Mirror;

namespace XPACE {
    public class GameController : MonoBehaviour {
        public GameObject activePlayer;

        public static GameController instance;
        private void Awake() {
            if (instance == null)
                instance = this;
        }
        [Server]
        public void StartGame() {
            // generate random map
            if (MapManager.instance == null) {
                Debug.LogError("MapManager instance not found!");
            } else {
                StartMap();
            }
        }
        void StartMap() {
            MapManager.instance.GenerateMap();
        }
    }
}