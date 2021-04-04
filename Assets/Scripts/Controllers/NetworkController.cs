using Mirror;
using UnityEngine;
using System.Collections.Generic;

namespace XPACE {
    public class NetworkController : NetworkManager {
        private Stack<string> colors;
        public override void OnStartServer() {
            base.OnStartServer();
            if (GameController.instance == null) {
                    Debug.LogError("GameController instance not found!");
            } else {
                    GameController.instance.SetupGame();
                    colors = new Stack<string>();
                    colors.Push("Yellow");
                    colors.Push("Green");
                    colors.Push("Red");
                    colors.Push("Blue");
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
            notification.content = "Player (" + conn.ToString() + ") connected to the server!";
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
        void OnCreatePlayer(NetworkConnection conn, Notification msg) {
            GameObject player = Instantiate(playerPrefab);
            PlayerData playerData = player.GetComponent<PlayerData>();
            playerData.SetName(colors.Pop());
            Debug.Log(msg.content);
            NetworkServer.AddPlayerForConnection(conn, player);
            // add player to list of players
            GameController.instance.AddPlayer(player);
        }
    }
}
