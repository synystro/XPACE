using Mirror;
using UnityEngine;

namespace XPACE {

    public class HexMapController : NetworkBehaviour {

        public GameObject hexTilePrefab;

        public int tilesAmount;
        public int columnSize;
        public int rowSize;

        private Transform hexesParent;
      
        void Start() {
            if(!isServer)
                return;
            hexesParent = this.transform;
            GenerateMap();
            print("hexmapcontroller called why");
        }

        public void GenerateMap() {
            for (int column = 0; column < columnSize; column++) {
                for (int row = 0; row < rowSize; row++) {

                    // create hex data
                    HexData hexData = new HexData(column, row);

                    // instatiate hex
                    GameObject hexTile = Instantiate(
                        hexTilePrefab,
                        hexData.Position(),
                        Quaternion.identity,
                        hexesParent
                    ) as GameObject;
                    hexTile.name = "tile_" + row + "_" + column;
                    NetworkServer.Spawn(hexTile);
                }
            }
        }
    }
}