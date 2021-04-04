using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace XPACE {

    public class PointController : MonoBehaviour {

        [SerializeField] InputController ownerPlayer;
        Button button;

        private void Start() {
            button = this.GetComponent<Button>();
            button.onClick.AddListener(Select);
            GameObject ownerPlayerGO = UiManager.instance.GetOwnerPlayer();
            ownerPlayer = ownerPlayerGO.GetComponent<InputController>();
        }

        void Select() {                  
            print("point " + this.gameObject.name + " selected");
            ownerPlayer.PlaceStructure(StructureType.MiningStation, this.transform.position.x, this.transform.position.y); 
        } 

    }
}