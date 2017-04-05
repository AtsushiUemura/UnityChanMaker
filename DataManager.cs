using UnityEngine;
using System.Collections;
using System.IO;

public class DataManager : SingletonMonoBehaviour<DataManager> {

    public string SavePath;
    public bool append;

    #region
    void Awake() {
        if (this != Instance) {
            Destroy(this.gameObject);
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

    public void Save() {
        var sw = new StreamWriter(SavePath, append);
        sw.Write(GridManager.Instance.Height + "\n");
        sw.Write(GridManager.Instance.Width + "\n");
        foreach (var item in TileManager.Instance.Tiles) {

            sw.Write(item.Tile.name.Replace("(Clone)", "") + ":" + item.Tile.transform.localPosition);
        }
    }

}
