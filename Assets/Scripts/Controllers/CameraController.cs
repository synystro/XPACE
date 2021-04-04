using UnityEngine;

namespace XPACE {
    public class CameraController : MonoBehaviour {      
        private float cameraMoveSpeed = 5;
        private Camera cam;

        private void Start() { 
            cam = this.GetComponent<Camera>();      
        }

        private void Update() {

            if(Input.GetKey(KeyCode.W))
                MoveCameraUp();
            if(Input.GetKey(KeyCode.A))
                MoveCameraLeft();
            if(Input.GetKey(KeyCode.S))
                MoveCameraDown();
            if(Input.GetKey(KeyCode.D))
                MoveCameraRight();
        }

        //[Command]
        //public void CmdPlaceStructure(StructureType type, float posX, float posY) {
        //    // register event
        //    EventSystem.instance.RegisterEvent(EventType.PlaceStructure);
        //    Vector3 structurePosition = new Vector3(posX, posY, 0);
        //    GameObject miningStation = Instantiate(MiningStationPrefab, structurePosition, Quaternion.identity);
        //    NetworkServer.Spawn(miningStation);
        //}

        private void MoveCameraUp() {
            this.transform.position += new Vector3(0, cameraMoveSpeed * Time.deltaTime, 0);
        }
        private void MoveCameraLeft()
        {
            this.transform.position += new Vector3(-cameraMoveSpeed * Time.deltaTime, 0, 0);
        }
        private void MoveCameraDown()
        {
            this.transform.position += new Vector3(0, -cameraMoveSpeed * Time.deltaTime, 0);
        }
         private void MoveCameraRight()
        {
            this.transform.position += new Vector3(cameraMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}