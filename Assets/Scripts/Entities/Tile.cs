using Mirror;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace XPACE {

    public class Tile : NetworkBehaviour {
        [SerializeField] TextMeshProUGUI textMesh;
        [SerializeField] [SyncVar] int resourceId;
        [SerializeField] [SyncVar] int number;
        Dictionary<string, int> ownerToAmount;
        public int Resource => resourceId;
        public int Number => number;
        public Dictionary<string,int> OwnerToAmount => ownerToAmount;

        private MeshRenderer meshRenderer;

        private void Awake() {
            meshRenderer = this.GetComponentInChildren<MeshRenderer>();
            // start owners on this tile dictionary
            ownerToAmount = new Dictionary<string, int>();
        }

        private void Start() {
            if (isServer)
                return;
            
            RefreshMaterial();
        }

        void RefreshMaterial() {
            // set tile resource
            switch (resourceId) {
                case (int)ResourceType.Empty:
                    meshRenderer.material = Resources.Load("Materials/tile_empty", typeof(Material)) as Material;
                    break;
                case (int)ResourceType.Iron:
                    meshRenderer.material = Resources.Load("Materials/tile_iron", typeof(Material)) as Material;
                    break;
                case (int)ResourceType.Silicon:
                    meshRenderer.material = Resources.Load("Materials/tile_silicon", typeof(Material)) as Material;
                    break;
                case (int)ResourceType.Uranium:
                    meshRenderer.material = Resources.Load("Materials/tile_uranium", typeof(Material)) as Material;
                    break;
                case (int)ResourceType.Crops:
                    meshRenderer.material = Resources.Load("Materials/tile_crops", typeof(Material)) as Material;
                    break;
                case (int)ResourceType.Water:
                    meshRenderer.material = Resources.Load("Materials/tile_water", typeof(Material)) as Material;
                    break;                
                default:
                    Debug.LogError("Some tile had no type set! What happened?");
                    break;
            }
            // set tile number
            if (resourceId != (int)ResourceType.Empty && number > 0) {
                textMesh.text = number.ToString();
            }
        }

        public void Refresh() {
            RefreshMaterial();
        }

        [Server]
        public void SetResource(ResourceType type) {
            resourceId = (int)type;
        }
        [Server]
        public void SetNumber(int number_) {
            number = number_;
        }
        [Server]
        public void AddOwnerAndAmount(string playerName, int amount) {
            if(ownerToAmount.ContainsKey(playerName)) {
                // refresh amount of resource gain for the existing player on this tile
                ownerToAmount[playerName] += amount;
            } else {
                // add player resource gain to this tile
                ownerToAmount.Add(playerName, amount);
            }
        }
        [ContextMenu("AddOneMiningToPlayerOne")]
        public void AddOneMiningToPlayerOne() {
            AddOwnerAndAmount(GameController.instance.Players[0].name, 1);
        }
        [ContextMenu("AddOneMiningToPlayerTwo")]
        public void AddOneMiningToPlayerTwo() {
            AddOwnerAndAmount(GameController.instance.Players[1].name, 1);
        }
    }
}