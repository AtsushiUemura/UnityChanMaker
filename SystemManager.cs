using UnityEngine;
using System.Collections;

public class SystemManager : SingletonMonoBehaviour<SystemManager> {


    void Awake() {
        if (Instance != this) {
            Destroy(this);
            return;
        }
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Quit();
    }

    private void Quit() {
        if (InputManager.Instance.Quit) {
            Application.Quit();
            return;
        }
    }
}
