using UnityEngine;
using System.Collections.Generic;

namespace XPACE {
    public class GameController : MonoBehaviour {
        private List<GameObject> players;
        public List<GameObject> Players => players;

        public static GameController instance;
        private void Awake() {
            if (instance == null)
                instance = this;
            players = new List<GameObject>();            
        }        
        public void SetupGame() {
            // generate random map
            if (MapManager.instance == null) {
                Debug.LogError("MapManager instance not found!");
            } else {
                StartMap();
            }
        }
        public void AddPlayer(GameObject player) {
            if(players.Contains(player) == false) {
                players.Add(player);
            } else {
                Debug.LogWarning("Player is already inside list of players!");
            }
        }            
        [ContextMenu("Start the game")]
        private void StartGame() {
            // shuffle players list
            IListExtensions.Shuffle<GameObject>(players);
            print($"first player rolled to {players[0]}");
            // initialise turn manager
            TurnManager.instance.Init();            
        }        
        void StartMap() {
            MapManager.instance.GenerateMap();
        }   
    }
}