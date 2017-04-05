using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float endPosX;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CameraControl();
    }

    private void CameraControl() {
        transform.position = new Vector3(player.transform.position.x, 0, -10);

        if (transform.position.x < 0) {
            transform.position = new Vector3(0, 0, -10);
        }

        if (transform.position.x >= endPosX) {
            transform.position = new Vector3(endPosX, 0, -10);
        }

    }

}
