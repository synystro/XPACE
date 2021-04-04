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
            if(TurnManager.instance.activePlayer != this.gameObject)
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
        public void PlaceStructure(StructureType type, float posX, float posY) {
            CmdPlaceStructure(type, posX, posY);
        }
        [Command]
        private void CmdPlaceStructure(StructureType type, float posX, float posY) {
            if(IsMyTurn() == false) {
                return;
            }
            // register event
            EventSystem.instance.RegisterEvent(EventType.PlaceStructure);
            Vector3 structurePosition = new Vector3(posX, posY, 0);            
            print(this.gameObject.name + " placed a structure on x " + structurePosition.x + ", y " + structurePosition.y);
            // place structure
            //GameObject miningStation = Instantiate(MiningStationPrefab, structurePosition, Quaternion.identity);
            //NetworkServer.Spawn(miningStation);
        }
    }
}