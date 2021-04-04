using UnityEngine;
using System.Collections.Generic;

namespace XPACE {
    public class TurnManager : MonoBehaviour {
        public GameObject activePlayer;

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
        }
        private void RollPlayersTurnOrder() {
            
            foreach(GameObject player in GameController.instance.Players) {
                turnOrder.Enqueue(player);
            }
            activePlayer = turnOrder.Dequeue();
            print($"first player ? {activePlayer}");
            activePlayer.GetComponent<InputController>().RpcStartTurn();
        }
        
        public void Init() {
            // set initial turn state
            curTurnState = TurnState.Roll;
            RollPlayersTurnOrder();            
        }

        public void RollDice() {
            int result = Random.Range(1, 13);
            print($"dice rolled for a {result}!");
            DistributeResources(result);
        }    
        
        public void EndTurn() {
            print($"{activePlayer} just ended their turn!");
            activePlayer.GetComponent<InputController>().RpcEndTurn();
            turnOrder.Enqueue(activePlayer);
            NextTurn();                     
        }        
        public void DistributeResources(int tileNumber) {
            curTurnState = TurnState.Resources;
            print($"giving resources to tiles with the number {tileNumber}!");
            //TODO: give resources accordingly
            curTurnState = TurnState.Plan;
            activePlayer.GetComponent<InputController>().RpcStartPlanningPhase();           
        }    
    }
}