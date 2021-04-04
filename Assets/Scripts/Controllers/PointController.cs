using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace XPACE {

    public class PointController : MonoBehaviour {

        Button button;

        private void Start() {
            button = this.GetComponent<Button>();
            button.onClick.AddListener(Select);
        }

        void Select() {  
            if(UiManager.instance.OwnerPlayer == null) {
                return;
            }        
            InputController ownerPlayer = UiManager.instance.OwnerPlayer.GetComponent<InputController>();
            ownerPlayer.PlaceStructure(StructureType.MiningStation, this.transform.position.x, this.transform.position.y); 
        } 

    }
}