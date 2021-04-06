using Mirror;
using UnityEngine;

namespace XPACE {
    public class NetworkController : NetworkManager {
        public static NetworkController instance;
        private new void Awake() {
            if(instance == null) {
                instance = this;
            }                        
        }              
        public override void OnStartServer() {
            base.OnStartServer();
            if (GameController.instance == null) {
                    Debug.LogError("GameController instance not found!");
            } else {
                    GameController.instance.SetupGame();
            }
            NetworkServer.RegisterHandler<Notification>(OnCreatePlayer);            
        }
        public override void OnStartHost() {
            base.OnStartHost();
        }
        public override void OnStopServer() {
            base.OnStopServer();
        }
        public override void OnClientConnect(NetworkConnection conn) {
            base.OnClientConnect(conn);
            Notification notification = new Notification();
            notification.content = $"Player {conn.address} just connected to the server!";
            conn.Send(notification);                  
        }
        public override void OnServerConnect(NetworkConnection conn) {
            base.OnServerConnect(conn);            
        }
        public override void OnServerAddPlayer(NetworkConnection conn) {
            base.OnServerAddPlayer(conn);   
        }
        public override void OnClientDisconnect(NetworkConnection conn) {
            base.OnClientDisconnect(conn);
        }
        public void SendNotification(string msg) {
            NetworkServer.SendToAll(new Notification { content = msg } );
        }
        void OnCreatePlayer(NetworkConnection conn, Notification msg) {
            GameObject player = Instantiate(playerPrefab);
            PlayerData playerData = player.GetComponent<PlayerData>();
            string playerColor = GameController.instance.Colors.Pop();
            playerData.SetColor(playerColor);
            playerData.SetName(playerColor);
            Debug.Log(msg.content);
            NetworkServer.AddPlayerForConnection(conn, player);
            // add player to list of players
            GameController.instance.AddPlayer(player);
        }
    }
}
