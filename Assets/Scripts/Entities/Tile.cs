using Mirror;
using UnityEngine;
using TMPro;

namespace XPACE {

    public class Tile : NetworkBehaviour {
        [SerializeField] TextMeshProUGUI textMesh;
        [SerializeField] [SyncVar] string resource;
        [SerializeField] [SyncVar] int number;
        public string Resource { get; private set; }
        public int Number { get; private set; }

        private MeshRenderer meshRenderer;

        private void Awake() {
            meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        }

        private void Start() {
            if (isServer)
                return;
            RefreshMaterial();
        }

        void RefreshMaterial() {
            if (resource == "") {
                Debug.LogError("No resource set to be refreshed?");
                return;
            }
            // set tile resource
            switch (resource) {
                case "empty":
                    meshRenderer.material = Resources.Load("Materials/tile_empty", typeof(Material)) as Material;
                    break;
                case "water":
                    meshRenderer.material = Resources.Load("Materials/tile_water", typeof(Material)) as Material;
                    break;
                case "crops":
                    meshRenderer.material = Resources.Load("Materials/tile_crops", typeof(Material)) as Material;
                    break;
                case "iron":
                    meshRenderer.material = Resources.Load("Materials/tile_iron", typeof(Material)) as Material;
                    break;
                case "silicon":
                    meshRenderer.material = Resources.Load("Materials/tile_silicon", typeof(Material)) as Material;
                    break;
                case "uranium":
                    meshRenderer.material = Resources.Load("Materials/tile_uranium", typeof(Material)) as Material;
                    break;
                default:
                    Debug.LogError("Some tile had no resource/emptiness set! What happened?");
                    break;
            }
            // set tile number
            if (!string.IsNullOrEmpty(resource) && number > 0)
                textMesh.text = number.ToString();
        }

        public void Refresh() {
            RefreshMaterial();
        }

        [Server]
        public void SetResource(string resource_) {
            resource = resource_;
            Resource = resource;
        }
        [Server]
        public void SetNumber(int number_) {
            number = number_;
            Number = number;
        }
    }

}