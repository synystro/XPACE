using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace XPACE {
    public class InputController : NetworkBehaviour {
        void Start() {
            if(!isLocalPlayer)
                return;
        }
        void Update() {
            if(!isLocalPlayer)
                return;
        }
        [ClientRpc]
        public void RpcSetNotificationPanel(bool state) {
            UiManager.instance.SetNotificationPanel(state);
        }
        [TargetRpc]
        public void RpcStartTurn() {
            UiManager.instance.SetRollButton(true);
            UiManager.instance.SetPointsParent(true);
        }
        [TargetRpc]
        public void RpcStartPlanningPhase() {
            UiManager.instance.SetRollButton(false);
            UiManager.instance.SetTradeButton(true);                        
        }
        [Command]
        public void EndTurn() {
            TurnManager.instance.EndTurn();
        }
        [TargetRpc]
        public void RpcEndTurn() {
            UiManager.instance.SetPointsParent(false);
            UiManager.instance.SetTradeButton(false); 
        }
        bool IsMyTurn() {
            if(TurnManager.instance.ActivePlayer != this.gameObject)
                return false;
            return true;
        }
        public void RollDice() {
            CmdRollDice();
        }
        [Command]
        private void CmdRollDice() {
            if(IsMyTurn() == false) {
                return;
            }
            TurnManager.instance.RollDice();
        }
        public void Trade() {
            CmdTrade();
        }
        [Command]
        private void CmdTrade() {
            if(IsMyTurn() == false) {
                return;
            }
            TurnManager.instance.EndTurn();
        }
        public void PlaceStructure(StructureType type, List<GameObject> adjacentTiles, float posX, float posY) {
            CmdPlaceStructure(type, adjacentTiles, posX, posY);
        }
        [Command]
        private void CmdPlaceStructure(StructureType type, List<GameObject> adjacentTiles, float posX, float posY) {
            if(IsMyTurn() == false) {
                return;
            }
            // spawn structure
            SpawnController.instance.SpawnStructure(type, posX, posY);
            // add player land ownership to them
            // set amount
            int amount = 0;
            switch(type) {
                case StructureType.MiningStation:
                    amount = 1;
                    break;
                case StructureType.Colony:
                    amount = 2;
                    break;
                case StructureType.Province:
                    amount = 3;
                    break;
                default:
                    Debug.LogError("Unkown structure type at spawn!");
                    break;
            }
            // set it on adjacent tiles
            foreach(GameObject tile in adjacentTiles) {
                Tile t = tile.GetComponent<Tile>();
                t.AddOwnerAndAmount(this.GetComponent<PlayerData>().Name, amount);
            }
        }
    }
}