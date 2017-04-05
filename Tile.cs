using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    protected AudioClip hitSE;

    #region
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    #endregion

    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            AudioManager.Instance.PlaySE(hitSE);
        }
    }
}
