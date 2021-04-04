using UnityEngine;

namespace XPACE {

    public class HexData {

        public readonly int R;  // row
        public readonly int C;  // column
        public readonly int S;

        private const float RADIUS = 1f;
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
        private const float HEIGHT_OFFSET = 0.75f;

        // constructor
        public HexData(int c, int r) {
            this.C = c;
            this.R = r;
            this.S = -(c + r);
        }

        // properties
        public float Width() { return WIDTH_MULTIPLIER * Height(); }
        public float Height() { return RADIUS * 2; }
        public float WidthOffset() { return Width(); }
        public float HeightOffset() { return Height() * HEIGHT_OFFSET; }

        // world space position of this hex
        public Vector3 Position() {

            float horizOffset = Width();
            float vertiOffset = Height() * 0.75f;

            return new Vector3(
                WidthOffset() * (this.C + this.R / 2f),
                //0,
                HeightOffset() * this.R
                , 0
            );
        }

        public Vector3 PositionFromCamera(Vector3 cameraPos, int numRows, int numColumns) {
            float mapWidth = numRows * WidthOffset();
            float mapHeight = numColumns * HeightOffset();



            return new Vector3(0, 0, 0);

        }

    }

}
