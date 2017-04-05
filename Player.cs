using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Status))]
public class Player : MonoBehaviour {

    public LayerMask WhatIsGround;
    public Vector2 BackwordForce;
    public float LeftMovableLimit;
    public float RightMovableLimit;
    private Status status { get { return GetComponent<Status>(); } }
    private Rigidbody2D rig2d { get { return GetComponent<Rigidbody2D>(); } }
    private Animator animator { get { return GetComponent<Animator>(); } }

    public BoxCollider2D bc2d { get { return GetComponent<BoxCollider2D>(); } }
    public CircleCollider2D cc2d { get { return GetComponent<CircleCollider2D>(); } }

    public enum State {
        NORMAL,
        DAMAGED,
        INVINCIBLE,
    }
    public State state;
    #region
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate() {

        MoveLimit();
        ChangeState();

    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other) {
        if (state != State.NORMAL) {
            return;
        }
        switch (other.tag) {
            case "Enemy":
                Debug.Log("Enemy");
                state = State.DAMAGED;
                StartCoroutine(INTERNAL_OnDamage());
                break;
            case "Item":
                Debug.Log("Item");
                UseItem(0);
                break;
            default:

                break;
        }

    }

    private IEnumerator INTERNAL_OnDamage() {
        animator.Play(IsGround() ? "Damage" : "AirDamage");
        animator.Play("Idle");
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        rig2d.velocity = new Vector2(transform.right.x * BackwordForce.x, transform.up.y * BackwordForce.y);
        yield return new WaitForSeconds(0.2f);
        while (!IsGround()) {
            yield return new WaitForFixedUpdate();
        }
        animator.SetTrigger("Invincible Mode");
        state = State.INVINCIBLE;
    }
    void OnFinishedInvincibleMode() {
        state = State.NORMAL;
    }

    private void ChangeState() {
        switch (state) {
            case State.NORMAL:
                Move();
                SetGround();

                break;
            case State.INVINCIBLE:
                Move();
                SetGround();
                break;
            case State.DAMAGED:
                SetGround();
                break;
            default:

                break;
        }
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    /// <returns></returns>
    private bool IsGround() {
        var pos = transform.localPosition;
        var isGround = Physics2D.Linecast(pos, pos - transform.up * 1.2f, WhatIsGround);
        return isGround;
    }
    private void MoveLimit() {
        if (transform.localPosition.x < LeftMovableLimit) {
            transform.localPosition = new Vector2(LeftMovableLimit, transform.localPosition.y);
        }
        if (transform.localPosition.x > RightMovableLimit) {
            transform.localPosition = new Vector2(RightMovableLimit, transform.localPosition.y);
        }
    }
    private void SetGround() {
        animator.SetBool("isGround", IsGround());
    }
    /// <summary>
    /// 移動
    /// </summary>
    private void Move() {
        if (Mathf.Abs(InputManager.Instance.Horizontal) > 0) {
            Quaternion rot = transform.localRotation;
            transform.localRotation = Quaternion.Euler(rot.x, Mathf.Sign(InputManager.Instance.Horizontal) == 1 ? 0 : 180, rot.z);
        }

        rig2d.velocity = new Vector2(InputManager.Instance.Horizontal * status.MoveSpeed, rig2d.velocity.y);

        animator.SetFloat("Horizontal", InputManager.Instance.Horizontal);
        animator.SetFloat("Vertical", rig2d.velocity.y);
        animator.SetBool("isGround", IsGround());

        if (InputManager.Instance.Jump && IsGround()) {
            rig2d.velocity = new Vector2(rig2d.velocity.x, 0);
            rig2d.AddForce(Vector2.up * status.JumpPower);
            animator.SetTrigger("Jump");
            SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void UseItem(int id) {
        switch (id) {
            case 0:
                status.HP++;
                break;
        }
    }
}
