using UnityEngine;
using System.Collections;

public class AudioManager : SingletonMonoBehaviour<AudioManager> {

    [SerializeField]
    private AudioSource SEAudioSource;
    [SerializeField]
    private AudioSource BGMAudioSource;

    #region
    void Awake() {
        if (this != Instance) {
            Destroy(this);
            return;
        }

    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    #endregion

    public void PlaySE(AudioClip se) {
        Debug.Log("Play:" + se.name);
        SEAudioSource.PlayOneShot(se);
    }

    public void PlayBGM(AudioClip bgm) {

        BGMAudioSource.clip = bgm;
        BGMAudioSource.Play();
    }

    public string GetCurrentBGMName() {
        if (BGMAudioSource.clip == null) {
            return "";
        }
        string bgmName = BGMAudioSource.clip.name;
        return bgmName;
    }
}
