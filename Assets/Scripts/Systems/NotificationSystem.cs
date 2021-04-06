using Mirror;
using TMPro;
using UnityEngine;

namespace XPACE {
    public class NotificationSystem : MonoBehaviour {
        [SerializeField] private TMP_Text notificationText = null;
        private void Start() {
            if(!NetworkClient.active) { return; }
            NetworkClient.RegisterHandler<Notification>(OnNotification);
        }
        private void OnNotification(NetworkConnection conn, Notification msg) {
            notificationText.text += $"\n{msg.content}";            
        }
    }
}