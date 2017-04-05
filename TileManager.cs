using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : SingletonMonoBehaviour<TileManager> {

    [System.Serializable]
    public class TileInfo {
        public bool Editable;
        public Vector2 Id;
        public GameObject Tile;
        public TileInfo(Vector2 Id, GameObject Tile) {
            Editable = true;
            this.Id = Id;
            this.Tile = Tile;
        }
    }


    public GameObject EmptyTilePrefab;
    [SerializeField]
    public GameObject StockTile;
    [SerializeField]
    private GameObject SurfaceTile;
    public GameObject BaseTile;

    public GameObject TileCanvas;

    public List<TileInfo> Tiles = new List<TileInfo>();
    public List<GameObject> Enemys = new List<GameObject>();

    #region
    void Awake() {
        if (this != Instance) {
            Destroy(this);
            return;
        }
    }

    void Start() {

        FillTile(EmptyTilePrefab);
        ChangeTileLine(new Vector2(0, 0), new Vector2(1, GridManager.Instance.Width), SurfaceTile);
        
        ChangeTileLine(new Vector2(0, 0), new Vector2(1, 2), BaseTile);
        /*
        ChangeTileLine(new Vector2(0, 0), new Vector2(3, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(4, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(5, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(6, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(7, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(8, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(9, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(10, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(11, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(12, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(13, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(14, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(15, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(16, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(17, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(18, 2), BaseTile);
        ChangeTileLine(new Vector2(0, 0), new Vector2(19, 2), BaseTile);
    */
    }
    #endregion

    /// <summary>
    /// ボタン処理を変更
    /// </summary>
    private void Init() {
        InputManager.Instance.ButtonDelegateLeft += ChangeTileDelegate;
        InputManager.Instance.ButtonDownDelegateLeft += MoveItemButtonDownDelegate;
        InputManager.Instance.ButtonDelegateLeft += MoveItemButtonDelegate;
        InputManager.Instance.ButtonUpDelegateLeft += MoveItemButtonUpDelegate;
    }
    /// <summary>
    /// タイルで埋める
    /// </summary>
    /// <param name="tile"></param>
    private void FillTile(GameObject tile) {
        int height = GridManager.Instance.Height;
        int width = GridManager.Instance.Width;
        float delta = GridManager.Instance.WidthDelta;
        Vector2 pos = GridManager.Instance.Offset;

        for (int i = 0; i < height; i++) {
            if (i != 0) {
                pos.x = GridManager.Instance.Offset.x;
                pos.y += delta;
            }
            else {
                pos.y += delta / 2;
            }
            for (int j = 0; j < width; j++) {
                if (j != 0) {
                    pos.x += delta;
                }
                else {
                    pos.x += delta / 2;
                }
                Vector2 id = new Vector2(i, j);
                Tiles.Add(CreateTile(id, tile, pos));
            }
        }
    }
    /// <summary>
    /// タイル情報を生成
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private TileInfo CreateTile(Vector2 id, GameObject tile, Vector2 pos) {
        GameObject tileObj = Instantiate(tile, pos, Quaternion.identity) as GameObject;
        tileObj.transform.parent = TileCanvas.transform;
        TileInfo tileInfo = new TileInfo(id, tileObj);
        return tileInfo;
    }
    /// <summary>
    /// タイルをすべて削除
    /// </summary>
    private void DeleteAllTile() {
        if (Tiles.Count != 0) {
            foreach (var item in Tiles) {
                Destroy(item.Tile.gameObject);
            }
            Tiles.Clear();
        }
    }
    /// <summary>
    /// すべてのタイルを変更
    /// </summary>
    /// <param name="tile"></param>
    private void ChangeAllTile(GameObject tile) {
        if (tile == null) {
            return;
        }
        if (Tiles.Count != 0) {
            foreach (var item in Tiles) {
                Destroy(item.Tile.gameObject);
                item.Tile = ChangeTile(tile, item.Tile.transform.position);
            }
        }
    }
    /// <summary>
    /// 座標からタイル変更
    /// </summary>
    /// <param name="pos"></param>
    private void ChangePartTile(Vector2 pos) {
        Collider2D col2d = Physics2D.OverlapPoint(pos);
        if (!col2d) {
            return;
        }
        if (!col2d.CompareTag("Tile")) {
            return;
        }
        if (col2d.gameObject == StockTile) {
            return;
        }

        TileInfo tileInfo = null;
        foreach (var item in Tiles) {
            if (item.Tile == col2d.gameObject) {
                tileInfo = item;
                break;
            }
        }
        ChangePartTile(tileInfo.Id, StockTile);

    }
    /******************************************************************/
    /// <summary>
    /// 座標からタイル情報を返す
    /// </summary>
    /// <param name="pos"></param>
    private TileInfo GetTileInfoFromPos(Vector3 pos) {
        if (pos == null) {
            Debug.LogWarning("posがNULLです");
            return null;
        }
        Collider2D col2d = Physics2D.OverlapPoint(pos);
        if (!col2d) {
            Debug.LogWarning("col2dがNULLです");
            return null;
        }
        if (!col2d.CompareTag("Tile")) {
            Debug.LogWarning("TagがTileではありません");
            return null;
        }
        if (Tiles.Count == 0) {
            Debug.LogWarning("Tilesが空です");
            return null;
        }
        TileInfo tileInfo = null;
        foreach (var item in Tiles) {
            if (item.Tile.Equals(col2d.gameObject)) {
                tileInfo = item;
                break;
            }
        }
        Debug.Log("re:" + tileInfo.Tile);
        return tileInfo;
    }
    public TileInfo GetTileInfoFromPosUI(Vector3 pos) {
        TileInfo tileInfo = GetTileInfoFromPos(pos);
        return tileInfo;
    }
    /// <summary>
    /// 座標の位置にタイルを設定する
    /// </summary>
    private void SetTileFromPos(Vector3 pos, GameObject tile) {
        if (pos == null || tile == null) {
            Debug.LogWarning("posとtileがNULLです");
            return;
        }
        Collider2D col2d = Physics2D.OverlapPoint(pos);
        if (col2d == null) {
            Debug.LogWarning("col2dがNULLです");
            return;
        }
        GameObject clone = Instantiate(tile, Vector3.zero, Quaternion.identity) as GameObject;
        if (Tiles.Count == 0) {
            Debug.LogWarning("Tilesが空です");
            return;
        }
        for (int n = 0; n < Tiles.Count; n++) {
            if (Tiles[n].Tile.Equals(col2d.gameObject)) {
                clone.transform.position = Tiles[n].Tile.transform.position;
                Destroy(Tiles[n].Tile);
                Tiles[n].Tile = clone;
                break;
            }
        }

    }
    public void SetTileInfoFromPosUI(Vector3 pos, GameObject tile) {
        SetTileFromPos(pos, tile);
    }

    public List<GameObject> TilePrefabs = new List<GameObject>();
    public GameObject GetTilePrefabFromName(string tileName) {
        if (tileName == "") {
            return null;
        }
        string prefabName = tileName.Replace("(Clone)", "");
        foreach (var item in TilePrefabs) {
            if (item.name.Equals(prefabName)) {
                return item;
            }
        }
        return null;
    }

    private void SetAllTileFromTile(GameObject prefab) {
        if (prefab == null) {
            return;
        }
        foreach (var item in Tiles) {
            if (!item.Tile.name.Equals("EmptyTile(Clone)")) {
                item.Editable = true;
                Destroy(item.Tile);
                item.Tile = Instantiate(prefab, item.Tile.transform.localPosition, Quaternion.identity) as GameObject;
            }
        }
    }
    public void SetAllTileFromTileUI() {
        SetAllTileFromTile(EmptyTilePrefab);
    }


    /*******************************************************************/
    /// <summary>
    /// IDのタイルを変更
    /// </summary>
    private void ChangePartTile(Vector2 id, GameObject tile) {
        if (tile == null) {
            return;
        }
        if (Tiles.Count != 0) {
            foreach (var item in Tiles) {
                if (item.Id == id) {
                    Destroy(item.Tile.gameObject);
                    item.Tile = ChangeTile(tile, item.Tile.transform.position);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// IDがstartからendのタイルを変更
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    private void ChangeTileLine(Vector2 start, Vector2 end, GameObject tile) {
        for (int y = (int)start.y; y < end.y; y++) {
            for (int x = (int)start.x; x < end.x; x++) {
                ChangePartTile(new Vector2(x, y), tile);
            }
        }
    }
    /// <summary>
    /// 変更タイル生成
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    private GameObject ChangeTile(GameObject tile, Vector2 pos) {
        GameObject tileObj = Instantiate(tile, pos, Quaternion.identity) as GameObject;
        tileObj.transform.parent = TileCanvas.transform;
        return tileObj;
    }
    /// <summary>
    /// クリック位置タイル変更
    /// </summary>
    public void ChangeTileDelegate() {
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ChangePartTile(clickPos);

    }
    /// <summary>
    /// タイルの移動
    /// </summary>
    [SerializeField]
    private Collider2D col2d;
    [SerializeField]
    private GameObject arrow;
    public void MoveItemButtonDownDelegate() {

        if (col2d != null) {
            return;
        }
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] col2ds = Physics2D.OverlapPointAll(clickPos);
        foreach (var item in col2ds) {
            if (item.CompareTag("Player") || item.CompareTag("Goal")) {
                col2d = item;
                arrow = col2d.transform.FindChild("Arrow").gameObject;
                Debug.Log(col2d.name);
                break;
            }
        }
        //タイル変更をしないように
        if (col2d != null) {
            InputManager.Instance.ButtonDelegateLeft -= TileManager.Instance.ChangeTileDelegate;
            //UIManager.Instance.ClearAllButton();
            Debug.Log("remove");
        }
    }
    public void MoveItemButtonDelegate() {
        if (col2d == null) {
            return;
        }
        col2d.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        arrow.SetActive(true);
    }
    public void MoveItemButtonUpDelegate() {

        if (col2d != null) {
            arrow.SetActive(false);
            col2d = null;
            arrow = null;
        }
    }
}
