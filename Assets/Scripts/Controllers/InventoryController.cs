using Mirror;

namespace XPACE {    
    public class InventoryController : NetworkBehaviour {
        [SyncVar] private int iron;
        [SyncVar] private int silicon;
        [SyncVar] private int uranium;
        [SyncVar] private int crops;
        [SyncVar] private int water;

        public int ResourceCount() { 
            return (iron + silicon + uranium + crops + water);
        }

        public void AddResource(ResourceType type, int amount) {
            PlayerData playerData = this.GetComponent<PlayerData>();
            string msg = $"<color={playerData.Color}>{playerData.Name}</color> just received {amount} {type.ToString()}!";
            NetworkController.instance.SendNotification(msg);
            switch(type) {
                case ResourceType.Iron:
                    iron += amount;
                    break;
                case ResourceType.Silicon:
                    silicon += amount;
                    break;
                case ResourceType.Uranium:
                    uranium += amount;
                    break;
                case ResourceType.Crops:
                    crops += amount;
                    break;
                case ResourceType.Water:
                    water += amount;
                    break;
                default:
                    break;
            }
        }

        public void RemoveResource(ResourceType type, int amount) {
            switch(type) {
                case ResourceType.Iron:
                    iron -= amount;
                    break;
                case ResourceType.Silicon:
                    silicon -= amount;
                    break;
                case ResourceType.Uranium:
                    uranium -= amount;
                    break;
                case ResourceType.Crops:
                    crops -= amount;
                    break;
                case ResourceType.Water:
                    water -= amount;
                    break;
                default:
                    break;
            }
        }
    }
}