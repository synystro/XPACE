using Mirror;
using UnityEngine;

namespace XPACE {
    public class PlayerData : NetworkBehaviour {
        [SyncVar(hook = nameof(SyncName))]
        private new string name = "No Color";
        private string color = "";
        public string Name => name;
        public string Color => color;

        private void Start() {
            if(!isLocalPlayer)
                return;
            UiManager.instance.SetOwnerPlayer(this.gameObject);    
        }

        private void SyncName(string oldName, string newName) {
            //name = newName;
            switch(newName) {
                case Constants.COLOR_BLUE:
                    name = "Blue";
                    break;
                case Constants.COLOR_RED:
                    name = "Red";
                    break;
                case Constants.COLOR_GREEN:
                    name = "Green";
                    break;
                case Constants.COLOR_YELLOW:
                    name = "Yellow";
                    break;
                default:               
                    break;
            }
            base.name = this.name;          
        }
        private void SyncColor(string oldColor, string newColor) {
            color = newColor;
        }
        public void SetName(string newName) {
            SyncName(name, newName);            
        }        
        public void SetColor(string newColor) {
            SyncColor(color, newColor);
        }
    }
}
