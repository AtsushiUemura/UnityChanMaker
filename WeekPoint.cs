using UnityEngine;
using System.Collections;

public class WeekPoint : MonoBehaviour {

    public Rigidbody2D rig2d { get { return GetComponentInParent<Rigidbody2D>(); } }
    public BoxCollider2D bc2d { get { return GetComponentInParent<BoxCollider2D>(); } }
    public Vector2 BackwordForce;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        SetActive();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Hit");
            bc2d.enabled = false;
            rig2d.isKinematic = false;
            rig2d.velocity = new Vector2(transform.right.x * BackwordForce.x, transform.up.y * BackwordForce.y);
        }
    }
    
    private void SetActive() {
        if (transform.localPosition.y < -6) {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
