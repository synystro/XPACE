using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace XPACE {

    public class PointController : MonoBehaviour {
        private const string TILE_TAG = "Tile";
        private const string TILE_LAYER = "Tile";
        private const float SCAN_RANGE_X = 0.5f;
        private const float SCAN_RANGE_Y = 0.5f;

        [SerializeField] private bool isOccupied;
        [SerializeField] private List<GameObject> adjacentTiles;
        public bool IsOccupied => isOccupied;
        public List<GameObject> AdjacentTiles => adjacentTiles;

        Button button;

        private void Start() {
            // get button and add functionality to it
            button = this.GetComponent<Button>();
            button.onClick.AddListener(Select);
            // initialise adjacent tiles' list
            adjacentTiles = new List<GameObject>();
            GetAdjacentTiles();
        }

        private void GetAdjacentTiles() {
            Vector3 pos = this.transform.position;

            Vector3 offset1 = new Vector3(pos.x - SCAN_RANGE_X, pos.y, -10);
            Vector3 offset2 = new Vector3(pos.x + SCAN_RANGE_X, pos.y, -10);
            Vector3 offset3 = new Vector3(pos.x, pos.y - SCAN_RANGE_Y, -10);
            Vector3 offset4 = new Vector3(pos.x, pos.y + SCAN_RANGE_Y, -10);

            RaycastHit[] hit1 = Physics.RaycastAll(offset1, Vector3.forward * 20, Mathf.Infinity, 1 << LayerMask.NameToLayer(TILE_LAYER));
            RaycastHit[] hit2 = Physics.RaycastAll(offset2, Vector3.forward * 20, Mathf.Infinity, 1 << LayerMask.NameToLayer(TILE_LAYER));
            RaycastHit[] hit3 = Physics.RaycastAll(offset3, Vector3.forward * 20, Mathf.Infinity, 1 << LayerMask.NameToLayer(TILE_LAYER));
            RaycastHit[] hit4 = Physics.RaycastAll(offset4, Vector3.forward * 20, Mathf.Infinity, 1 << LayerMask.NameToLayer(TILE_LAYER));

            foreach (RaycastHit hit in hit1) {
                if(hit1.Length > 1)
                    break;
                if (hit.collider != null) {
                    if (hit.collider.tag == TILE_TAG) {
                        GameObject tileGO = hit.collider.transform.parent.gameObject;
                        if(!adjacentTiles.Contains(tileGO)) {
                            adjacentTiles.Add(tileGO);
                        }
                    }
                }
            } 
            foreach (RaycastHit hit in hit2) {
                if(hit2.Length > 1)
                    break;
                if (hit.collider != null) {
                    if (hit.collider.tag == TILE_TAG) {
                        GameObject tileGO = hit.collider.transform.parent.gameObject;
                        if(!adjacentTiles.Contains(tileGO)) {
                            adjacentTiles.Add(tileGO);
                        }
                    }
                }
            }
            foreach (RaycastHit hit in hit3) {
                if(hit3.Length > 1)
                    break;
                if (hit.collider != null) {
                    if (hit.collider.tag == TILE_TAG) {
                        GameObject tileGO = hit.collider.transform.parent.gameObject;
                        if(!adjacentTiles.Contains(tileGO)) {
                            adjacentTiles.Add(tileGO);
                        }
                    }
                }
            }
            foreach (RaycastHit hit in hit4) {
                if(hit4.Length > 1) {
                    break;
                }
                if (hit.collider != null) {
                    if (hit.collider.tag == TILE_TAG) {
                        GameObject tileGO = hit.collider.transform.parent.gameObject;
                        if(!adjacentTiles.Contains(tileGO)) {
                            adjacentTiles.Add(tileGO);
                        }
                    }
                }
            }   
        }

        void Select() {  
            if(isOccupied) { print("Point already occupied!"); return; }
            if(UiManager.instance.OwnerPlayer == null) { return; }    
            InputController ownerPlayer = UiManager.instance.OwnerPlayer.GetComponent<InputController>();
            //TODO: make it pass the adjacent tiles to add them for the player to collect
            ownerPlayer.PlaceStructure(StructureType.MiningStation, adjacentTiles, this.transform.position.x, this.transform.position.y);
            isOccupied = true; 
        } 

    }
}