using UnityEngine;
using UnityEngine.UI;

namespace XPACE {
    public class ButtonController : MonoBehaviour {
        public void RollDice() {
            if(UiManager.instance.OwnerPlayer == null) {
                Debug.LogError("Owner player not set at UiManager!");
                return;
            }        
            InputController ownerPlayer = UiManager.instance.OwnerPlayer.GetComponent<InputController>();   
            ownerPlayer.RollDice();
        }        
        public void Trade() {
            if(UiManager.instance.OwnerPlayer == null) {
                Debug.LogError("Owner player not set at UiManager!");
                return;
            }        
            InputController ownerPlayer = UiManager.instance.OwnerPlayer.GetComponent<InputController>();   
            ownerPlayer.EndTurn();
        }
    }
}
