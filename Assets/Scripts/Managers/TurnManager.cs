using UnityEngine;
using System.Collections.Generic;
using Mirror;

namespace XPACE {
    public class TurnManager : MonoBehaviour {
        private GameObject activePlayer;
        public GameObject ActivePlayer => activePlayer;
        private PlayerData playerData => activePlayer.GetComponent<PlayerData>();

        public enum TurnState { Roll, Resources, Plan, Trade, Build, Move, Attack, Bounty };
        [SerializeField] private TurnState curTurnState;
        private Queue<GameObject> turnOrder;

        public static TurnManager instance;
        private void Awake() {
            if (instance == null)
                instance = this;
            turnOrder = new Queue<GameObject>();           
        }
        private void NextTurn() {
            activePlayer = turnOrder.Dequeue();   
            activePlayer.GetComponent<InputController>().RpcStartTurn();
            string msg = $"It's now <color={playerData.Color}>{playerData.Name}</color>'s turn.";
            NetworkController.instance.SendNotification(msg);
        }
        private void RollPlayersTurnOrder() {
            
            foreach(GameObject player in GameController.instance.Players) {
                turnOrder.Enqueue(player);
            }
            activePlayer = turnOrder.Dequeue();            
            activePlayer.GetComponent<InputController>().RpcStartTurn();
        }
        
        public void Init() {               
            // set initial turn state
            curTurnState = TurnState.Roll;
            RollPlayersTurnOrder();
            // first player message
            string msg = $"Dice rolled <color={playerData.Color}>{playerData.Name}</color> as the first player!";
            NetworkController.instance.SendNotification(msg);        
        }

        public void RollDice() {
            int result = Random.Range(2, 13);
            string msg = $"<color={playerData.Color}>{playerData.Name}</color> rolled for a total {result}!";
            NetworkController.instance.SendNotification(msg);
            DistributeResources(result);
        }    
        
        public void EndTurn() {
            string msg = $"<color={playerData.Color}>{playerData.Name}</color> just ended their turn.";
            NetworkController.instance.SendNotification(msg);
            activePlayer.GetComponent<InputController>().RpcEndTurn();
            turnOrder.Enqueue(activePlayer);
            NextTurn();                     
        }        
        public void DistributeResources(int tileNumber) {
            curTurnState = TurnState.Resources;
            // distribute and return if anyone got a resource
            bool anyoneGotAnything = ResourceController.instance.DistributeResourcesForTiles(tileNumber);
            if(anyoneGotAnything == false) {
                string msg = $"Nothing given on this turn.";
                NetworkController.instance.SendNotification(msg);
            }
            curTurnState = TurnState.Plan;
            activePlayer.GetComponent<InputController>().RpcStartPlanningPhase();           
        }    
    }
}