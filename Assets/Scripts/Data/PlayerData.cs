using Mirror;

namespace XPACE {
    public class PlayerData : NetworkBehaviour {
        //public static readonly Vector3 movement = new Vector3(0f, 5f, 0f);
        [SyncVar(hook = nameof(SyncColor))]
        private string color = "No Color";
        public string Name => color;

        private void SyncColor(string oldColor, string newColor) {
            color = newColor;
            name = this.color;          
        }
        public void SetName(string newColor) {
            SyncColor(color, newColor);            
        }
    }
}
