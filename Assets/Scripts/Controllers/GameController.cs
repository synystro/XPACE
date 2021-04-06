using UnityEngine;
using System.Collections.Generic;

namespace XPACE {      
    public class GameController : MonoBehaviour {
        public Stack<string> Colors;
        private List<GameObject> players;        
        public List<GameObject> Players => players;

        public static GameController instance;
        private void Awake() {
            if (instance == null)
                instance = this;
            players = new List<GameObject>();            
        }        
        public void SetupGame() {
            // define colors
            SetupNamesAndColors();
            // generate random map
            if (MapManager.instance == null) {
                Debug.LogError("MapManager instance not found!");
            } else {
                // start map generation
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
        private void SetupNamesAndColors() {   
            Colors = new Stack<string>();
            Colors.Push(Constants.COLOR_YELLOW);
            Colors.Push(Constants.COLOR_GREEN);
            Colors.Push(Constants.COLOR_RED);
            Colors.Push(Constants.COLOR_BLUE);
        }            
        [ContextMenu("Start the game")]
        private void StartGame() {
            // shuffle players list
            IListExtensions.Shuffle<GameObject>(players);
            //FIXME in the future, make this start all ui components necessary when game starts
            foreach(GameObject player in Players) {
                player.GetComponent<InputController>().RpcSetNotificationPanel(true);
            }  
            // initialise turn manager
            TurnManager.instance.Init();                       
        }        
        void StartMap() {
            MapManager.instance.GenerateMap();
        }   
    }
}