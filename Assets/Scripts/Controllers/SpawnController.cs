using UnityEngine;
using Mirror;

namespace XPACE {
    public class SpawnController : NetworkBehaviour {
        [Header("Prefabs")]
        [SerializeField] private GameObject MiningStationPrefab;
        [SerializeField] private GameObject ColonyPrefab;
        [SerializeField] private GameObject ProvincePrefab;

        public static SpawnController instance;
        private void Awake() {
            if (instance == null)
                instance = this;
        }

        //TODO make it spawn with the player's color
        public void SpawnStructure(StructureType type, float posX, float posY) {
            // set position            
            Vector3 structurePosition = new Vector3(posX, posY, 0);            
            // define structure
            GameObject structurePrefab;
            switch(type) {
                case StructureType.MiningStation:
                    structurePrefab = MiningStationPrefab;
                    break;
                case StructureType.Colony:
                    structurePrefab = ColonyPrefab;
                    break;
                case StructureType.Province:
                    structurePrefab = ProvincePrefab;
                    break;
                default:
                    Debug.LogError("Structure type unkown or undefined.");
                    structurePrefab = null;
                    break;
            }
            if(structurePrefab == null) {
                Debug.LogError("Could not spawn structure. Check if prefab reference is undefined");
                return;
            }
            // spawn structure
            GameObject structure = Instantiate(structurePrefab, structurePosition, Quaternion.identity);
            NetworkServer.Spawn(structure);
            // register event
            EventSystem.instance.RegisterEvent(EventType.PlaceStructure);
            // print msg in notification panel
            PlayerData playerData = TurnManager.instance.ActivePlayer.GetComponent<PlayerData>();
            string msg = $"<color={playerData.Color}>{playerData.Name}</color> just placed a {type} on the map!";
            NetworkController.instance.SendNotification(msg);
        }
    }
}