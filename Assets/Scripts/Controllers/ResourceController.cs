using Mirror;
using UnityEngine;
using System.Collections.Generic;

namespace XPACE {
    public enum ResourceType { Empty, Iron, Silicon, Uranium, Crops, Water }

    public class ResourceController : NetworkBehaviour {

        public GameObject tilesParent;
        [SerializeField] public Tile[] Tiles { get; private set; }

        public bool IsGenerated { get; private set; }

        public delegate void OnTilesChanged();
        public OnTilesChanged onTilesChanged;

        private List<Tile> availableTiles = new List<Tile>();
        private int water;
        private int crops;
        private int iron;
        private int silicon;
        private int uranium;

        private int[] numbers = new int[18] { 2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12 };

        private const int WTR_QTY = 4;
        private const int CRO_QTY = 4;
        private const int IRO_QTY = 4;
        private const int SIL_QTY = 3;
        private const int URA_QTY = 3;

        private int resAmount;
        private int emptyTilesAmount;

        public static ResourceController instance;
        private void Awake() {
            if (instance == null)
                instance = this;          
        }

        [TargetRpc]
        public void TargetRefreshAllTiles(NetworkConnection conn) {
            onTilesChanged?.Invoke();
        }
        [Server]
        public void Init() {
            if(tilesParent == null) {
                Debug.LogError("TilesParent gameobject was not found!");
            } else {
                Tiles = tilesParent.GetComponentsInChildren<Tile>(true);
            }

            if(Tiles.Length == 0) {
                print(tilesParent.name);
                Debug.LogError("Not tiles found to be set!");
                return;
            }

            foreach (Tile t in Tiles) {
                onTilesChanged += t.Refresh;
                availableTiles.Add(t);
            }

            resAmount = WTR_QTY + CRO_QTY + IRO_QTY + SIL_QTY + URA_QTY;

            water = WTR_QTY;
            crops = CRO_QTY;
            iron = IRO_QTY;
            silicon = SIL_QTY;
            uranium = URA_QTY;

            emptyTilesAmount = Tiles.Length - resAmount;

            SetEmptyTiles();
            ShuffleResourcesOnTiles();

            IsGenerated = true;
        }
        [Server]
        void SetEmptyTiles() {
            if (emptyTilesAmount > availableTiles.Count) {
                Debug.LogError("Cannot set empty tiles. Number of tiles is smaller than empty tiles");
                return;
            }
            int emptyTilesLeft = emptyTilesAmount;
            while (emptyTilesLeft > 0) {
                int randomTileN = UnityEngine.Random.Range(0, availableTiles.Count);
                Tile randomTile = availableTiles[randomTileN];
                if (randomTile.Resource == (int)ResourceType.Empty) {
                    randomTile.SetResource(ResourceType.Empty);
                    availableTiles.Remove(randomTile);
                    emptyTilesLeft--;
                }
            }
        }
        [Server]
        void ShuffleResourcesOnTiles() {
            List<int> numbersLeft = new List<int>();
            for (int i = 0; i < numbers.Length; i++)
                numbersLeft.Add(numbers[i]);

            while (availableTiles.Count > 0) {
                int randomTileN = UnityEngine.Random.Range(0, availableTiles.Count);
                Tile randomTile = availableTiles[randomTileN];

                if (water > 0) {
                    randomTile.SetResource(ResourceType.Water);
                    water--;
                } else if (crops > 0) {
                    randomTile.SetResource(ResourceType.Crops);
                    crops--;
                } else if (iron > 0) {
                    randomTile.SetResource(ResourceType.Iron);
                    iron--;
                } else if (silicon > 0) {
                    randomTile.SetResource(ResourceType.Silicon);
                    silicon--;
                } else if (uranium > 0) {
                    randomTile.SetResource(ResourceType.Uranium);
                    uranium--;
                }

                int randomN = UnityEngine.Random.Range(0, numbersLeft.Count);
                int randomNumber = numbersLeft[randomN];

                randomTile.SetNumber(randomNumber);

                numbersLeft.Remove(randomNumber);
                availableTiles.Remove(randomTile);
            }
            onTilesChanged?.Invoke();
        }
        public bool DistributeResourcesForTiles(int tileNumber) {
            bool wasGiven = false;
            // loop through all tiles to find the ones with the desired number
            foreach (Tile t in Tiles) {
                if (t.Number == tileNumber) {
                    int resourceToGive = t.Resource;
                    if (t.OwnerToAmount.Count > 0) {
                        foreach (KeyValuePair<string, int> entry in t.OwnerToAmount) {
                            foreach (GameObject player in GameController.instance.Players) {
                                if (player.name == entry.Key.ToString()) {
                                    player.GetComponent<InventoryController>().AddResource((ResourceType)resourceToGive, entry.Value);
                                    wasGiven = true;
                                }
                            }
                        }
                    }
                }
            }
            return wasGiven;
        }
    }

}
