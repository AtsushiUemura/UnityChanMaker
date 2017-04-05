using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    /*
    private void OnCllisionEnter(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Hit");
            bc2d.enabled = false;
            rig2d.isKinematic = false;
            rig2d.velocity = new Vector2(transform.right.x * BackwordForce.x, transform.up.y * BackwordForce.y);
        }
    }
    */
}
