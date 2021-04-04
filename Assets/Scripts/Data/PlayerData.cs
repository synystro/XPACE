using Mirror;

namespace XPACE {
    public class PlayerData : NetworkBehaviour {
        [SyncVar(hook = nameof(SyncColor))]
        private string color = "No Color";
        public string Name => color;

        private void Start() {
            if(!isLocalPlayer)
                return;
            UiManager.instance.SetOwnerPlayer(this.gameObject);    
        }

        private void SyncColor(string oldColor, string newColor) {
            color = newColor;
            name = this.color;          
        }
        public void SetName(string newColor) {
            SyncColor(color, newColor);            
        }        
    }
}
