using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager> {

    public delegate void InputDelegate();
    public InputDelegate ButtonDownDelegateLeft;
    public InputDelegate ButtonDownDelegateRight;
    public InputDelegate ButtonDelegateLeft;
    public InputDelegate ButtonDelegateRight;
    public InputDelegate ButtonUpDelegateLeft;

    public float Vertical;
    public float Horizontal;
    public bool Jump;
    public bool Quit;

    #region
    void Awake() {
        if (this != Instance) {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        InitInput();
    }

    void Start() {

    }

    void Update() {
        Inputting();
    }
    #endregion

    /// <summary>
    /// 初期化
    /// </summary>
    private void InitInput() {
        ButtonDownDelegateLeft = () => { };
        ButtonDownDelegateRight = () => { };
        ButtonDelegateLeft = () => { };
        ButtonDelegateRight = () => { };
        ButtonUpDelegateLeft = () => { };
    }
    /// <summary>
    /// 入力に合わせて処理
    /// </summary>
    private void Inputting() {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetButtonDown("Jump");
        Quit = Input.GetKeyDown(KeyCode.Escape);

        if (ButtonDownDelegateLeft != null && Input.GetMouseButtonDown(0)) {
            ButtonDownDelegateLeft();
        }
        else if (ButtonDownDelegateRight != null && Input.GetMouseButtonDown(1)) {
            ButtonDownDelegateRight();
        }
        else if (ButtonDelegateLeft != null && Input.GetMouseButton(0)) {
            ButtonDelegateLeft();
        }
        else if (ButtonDelegateRight != null && Input.GetMouseButton(1)) {
            ButtonDelegateRight();
        }
        else if (ButtonUpDelegateLeft != null && Input.GetMouseButtonUp(0)) {
            ButtonUpDelegateLeft();
        }
        else {
            return;
        }
    }

}
