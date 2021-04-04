using UnityEngine;

public enum StructureType {
    MiningStation,
    Colony,
    Province
}

public class StructureData : MonoBehaviour {
    float posX;
    float posY;
    StructureType type;
}
