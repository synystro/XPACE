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
            if(Input.GetKey(KeyCode.X)) {
                ChangeColor();
            }
        }
        public void PlaceStructure(StructureType type, float posX, float posY) {
            CmdPlaceStructure(type, posX, posY);
        }
        [Command]
        private void ChangeColor() {
            this.GetComponent<PlayerData>().SetName("Black");
        }
        [Command]
        private void CmdPlaceStructure(StructureType type, float posX, float posY) {
            if(GameController.instance.activePlayer != this.gameObject) {
                print("not " + this.gameObject.name + "'s turn.");
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