using UnityEngine;

namespace XPACE {
    public class UiManager : MonoBehaviour {
        public static UiManager instance;
        private void Awake() {
            if (instance == null)
                instance = this;
        }
        [SerializeField] private GameObject ownerPlayer;
        [SerializeField] private GameObject pointsParent;
        [SerializeField] private GameObject rollButton;
        [SerializeField] private GameObject tradeButton;

        public GameObject OwnerPlayer => ownerPlayer;

        public void SetOwnerPlayer(GameObject player) {
            ownerPlayer = player;          
        }
        public void SetPointsParent(bool state) {
            pointsParent.SetActive(state);
        }
        public void SetRollButton(bool state) {
            rollButton.SetActive(state);
        }
        public void SetTradeButton(bool state) {
            tradeButton.SetActive(state);
        }
    }
}