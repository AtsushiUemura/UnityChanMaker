using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : SingletonMonoBehaviour<UIManager> {

    [System.Serializable]
    public class StockInfo {
        public Sprite StockImage;
        public GameObject TilePrefab;
    }
    public List<StockInfo> StockInfos = new List<StockInfo>();

    void Start() {
        Init();
    }

    private void Init() {
        InputManager.Instance.ButtonDelegateLeft += TilePen;
        InputManager.Instance.ButtonDelegateLeft += TileErase;
        InputManager.Instance.ButtonDelegateLeft += TileLock;
        InputManager.Instance.ButtonDelegateRight += TileUnLock;

        InputManager.Instance.ButtonDownDelegateLeft += TileMoveEnter;
        InputManager.Instance.ButtonDelegateLeft += TileMoveStay;
        InputManager.Instance.ButtonUpDelegateLeft += TileMoveExit;

        InputManager.Instance.ButtonDownDelegateLeft += TileScaleUp;
        InputManager.Instance.ButtonDownDelegateRight += TileScaleDown;

        CameraSlider.onValueChanged.AddListener(delegate { ChangeCameraPos(); });
    }

    /*******************************************************************************************/

    public float TakeTime;
    public AnimationCurve AnimationCurve;
    public Sprite[] ArrowSprites;

    public RectTransform RightUIRect;
    public Image RightImage;
    private bool RightButtonHandlerFlag;
    public void RightButtonHandler() {
        if (!RightButtonHandlerFlag) {
            RightButtonHandlerFlag = true;
            RightImage.sprite = ArrowSprites[0];
            StartCoroutine(MoveToRight(new Vector3(0, 1, 1), TakeTime));
        }
        else {
            RightButtonHandlerFlag = false;
            RightImage.sprite = ArrowSprites[1];
            StartCoroutine(MoveToRight(Vector3.one, TakeTime));
        }
    }
    private bool MoveToRightIsRunning;
    private IEnumerator MoveToRight(Vector3 destination, float takeTime) {
        if (MoveToRightIsRunning) {
            yield break;
        }
        MoveToRightIsRunning = true;
        Vector3 pivot = RightUIRect.localScale;
        float elapsedTime = 0;
        bool flag = false;
        while (!flag) {
            if (Vector3.Distance(RightUIRect.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = AnimationCurve.Evaluate(elapsedTime);
            RightUIRect.localScale = Vector3.Lerp(pivot, destination, progress);
            yield return null;
        }
        RightUIRect.localScale = destination;
        MoveToRightIsRunning = false;
    }

    public RectTransform LeftUIRect;
    public Image LeftImage;
    private bool LeftButtonHandlerFlag;
    public void LeftButtonHandler() {
        if (!LeftButtonHandlerFlag) {
            LeftButtonHandlerFlag = true;
            LeftImage.sprite = ArrowSprites[1];
            StartCoroutine(MoveToLeft(new Vector3(0, 1, 1), TakeTime));
        }
        else {
            LeftButtonHandlerFlag = false;
            LeftImage.sprite = ArrowSprites[0];
            StartCoroutine(MoveToLeft(Vector3.one, TakeTime));
        }
    }
    private bool MoveToLeftIsRunning;
    private IEnumerator MoveToLeft(Vector3 destination, float takeTime) {
        if (MoveToLeftIsRunning) {
            yield break;
        }
        MoveToLeftIsRunning = true;
        Vector3 pivot = LeftUIRect.localScale;
        float elapsedTime = 0;
        bool flag = false;
        while (!flag) {
            if (Vector3.Distance(LeftUIRect.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = AnimationCurve.Evaluate(elapsedTime);
            LeftUIRect.localScale = Vector3.Lerp(pivot, destination, progress);
            yield return null;
        }
        LeftUIRect.localScale = destination;
        MoveToLeftIsRunning = false;
    }

    public RectTransform UpUIRect;
    private bool UpButtonHandlerFlag;
    public void UpButtonHandler() {
        if (!UpButtonHandlerFlag) {
            UpButtonHandlerFlag = true;
            StartCoroutine(MoveToUp(new Vector3(1, 0, 1), TakeTime));
        }
        else {
            UpButtonHandlerFlag = false;
            StartCoroutine(MoveToUp(Vector3.one, TakeTime));
        }
    }
    private bool MoveToUpIsRunning;
    private IEnumerator MoveToUp(Vector3 destination, float takeTime) {
        if (MoveToUpIsRunning) {
            yield break;
        }
        MoveToUpIsRunning = true;
        Vector3 pivot = UpUIRect.localScale;
        float elapsedTime = 0;
        bool flag = false;
        while (!flag) {
            if (Vector3.Distance(UpUIRect.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = AnimationCurve.Evaluate(elapsedTime);
            UpUIRect.localScale = Vector3.Lerp(pivot, destination, progress);
            yield return null;
        }
        UpUIRect.localScale = destination;
        MoveToUpIsRunning = false;

    }

    public RectTransform DownUIRect;
    private bool DownButtonHandlerFlag;
    public void DownButtonHandler() {
        if (!DownButtonHandlerFlag) {
            DownButtonHandlerFlag = true;
            StartCoroutine(MoveToDown(new Vector3(1, 0, 1), TakeTime));
        }
        else {
            DownButtonHandlerFlag = false;
            StartCoroutine(MoveToDown(Vector3.one, TakeTime));
        }
    }
    private bool MoveToDownIsRunning;
    private IEnumerator MoveToDown(Vector3 destination, float takeTime) {
        if (MoveToDownIsRunning) {
            yield break;
        }
        MoveToDownIsRunning = true;
        Vector3 pivot = DownUIRect.localScale;
        float elapsedTime = 0;
        bool flag = false;
        while (!flag) {
            if (Vector3.Distance(DownUIRect.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = AnimationCurve.Evaluate(elapsedTime);
            DownUIRect.localScale = Vector3.Lerp(pivot, destination, progress);
            yield return null;
        }
        DownUIRect.localScale = destination;
        MoveToDownIsRunning = false;
    }

    public RectTransform PanelUIRect;
    public Sprite[] PullDownSprites;
    public Image PullDownImage;
    private bool PanelButtonHandlerFlag;
    public void PanelButtonHandler() {
        if (!PanelButtonHandlerFlag) {
            PanelButtonHandlerFlag = true;
            PullDownImage.sprite = PullDownSprites[0];
            StartCoroutine(MoveToPanel(Vector3.one, TakeTime));
            //ほかのイベントを非選択にする
            PenButtonHandlerFlag = true;
            PenButtonHandler();
            EraserButtonHandlerFlag = true;
            EraserButtonHandler();
            LockButtonHandlerFlag = true;
            LockButtonHandler();
        }
        else {
            PanelButtonHandlerFlag = false;
            PullDownImage.sprite = PullDownSprites[1];
            StartCoroutine(MoveToPanel(Vector3.zero, TakeTime));
        }
    }
    private bool MoveToPanelIsRunning;
    private IEnumerator MoveToPanel(Vector3 destination, float takeTime) {
        if (MoveToPanelIsRunning) {
            yield break;
        }
        MoveToPanelIsRunning = true;
        Vector3 pivot = PanelUIRect.localScale;
        float elapsedTime = 0;
        bool flag = false;
        while (!flag) {
            if (Vector3.Distance(PanelUIRect.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = AnimationCurve.Evaluate(elapsedTime);
            PanelUIRect.localScale = Vector3.Lerp(pivot, destination, progress);
            yield return null;
        }
        PanelUIRect.localScale = destination;
        MoveToPanelIsRunning = false;
    }

    public RectTransform AlertUIRect;
    private bool AlertButtonHandlerFlag;
    public void AlertButtonHandler() {
        if (!AlertButtonHandlerFlag) {
            AlertButtonHandlerFlag = true;
            StartCoroutine(MoveToAlert(Vector3.one, TakeTime));
        }
        else {
            AlertButtonHandlerFlag = false;
            StartCoroutine(MoveToAlert(Vector3.zero, TakeTime));
        }
    }
    private bool MoveToAlertIsRunning;
    private IEnumerator MoveToAlert(Vector3 destination, float takeTime) {
        if (MoveToAlertIsRunning) {
            yield break;
        }
        MoveToAlertIsRunning = true;
        Vector3 pivot = AlertUIRect.localScale;
        float elapsedTime = 0;
        bool flag = false;
        while (!flag) {
            if (Vector3.Distance(AlertUIRect.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = AnimationCurve.Evaluate(elapsedTime);
            AlertUIRect.localScale = Vector3.Lerp(pivot, destination, progress);
            yield return null;
        }
        AlertUIRect.localScale = destination;
        MoveToAlertIsRunning = false;
    }

    public Image PlayButtonImage;
    public Sprite[] PlaySprites;
    public RectTransform[] SideButtoRects;
    public void PlayButtonHandler() {
        PlayButtonImage.sprite = PlaySprites[0];
        SideButtoRects[0].localScale = Vector3.zero;
        SideButtoRects[1].localScale = Vector3.zero;

        UpButtonHandlerFlag = false;
        UpButtonHandler();
        DownButtonHandlerFlag = false;
        DownButtonHandler();
        RightButtonHandlerFlag = false;
        RightButtonHandler();
        LeftButtonHandlerFlag = false;
        LeftButtonHandler();

        PanelButtonHandlerFlag = true;
        PanelButtonHandler();
        AlertButtonHandlerFlag = true;
        AlertButtonHandler();

        StartCoroutine(FadeOut(fadeOutInterval));
    }
    public void Reset() {
        PlayButtonImage.sprite = PlaySprites[1];
        SideButtoRects[0].localScale = Vector3.one;
        SideButtoRects[1].localScale = Vector3.one;

        UpButtonHandlerFlag = true;
        UpButtonHandler();
        DownButtonHandlerFlag = true;
        DownButtonHandler();
        RightButtonHandlerFlag = true;
        RightButtonHandler();
        LeftButtonHandlerFlag = true;
        LeftButtonHandler();

        PanelButtonHandlerFlag = true;
        PanelButtonHandler();
        AlertButtonHandlerFlag = true;
        AlertButtonHandler();

        fadeImage.color = Color.clear;
    }

    /******************************************************************************************************/

    public Image fadeImage;
    //フェードにかかる時間
    public float fadeOutInterval;
    private bool fadeOutIsRunning;
    /// <summary>
    /// フェードアウト
    /// </summary>
    private IEnumerator FadeOut(float interval) {
        if (fadeOutIsRunning) {
            yield break;
        }
        fadeOutIsRunning = true;
        float fadeAlpha = 0;
        float time = 0;
        while (time <= interval) {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeOutIsRunning = false;

        Reset(); //Test
    }

    /*****************************/
    public Image StockImage;
    private void TileStock(int tileID) {
        StockImage.sprite = StockInfos[tileID].StockImage;
        TileManager.Instance.StockTile = StockInfos[tileID].TilePrefab;
    }
    public void TileStockHandler(int tileID) {
        TileStock(tileID);
    }

    /******************************************************************************************************/
    public Image LockImage;
    private bool LockButtonHandlerFlag;
    public void LockButtonHandler() {
        if (!LockButtonHandlerFlag) {
            LockButtonHandlerFlag = true;
            LockImage.color = new Color(0.5f, 0.5f, 0.5f, 1);

            PenButtonHandlerFlag = true;
            PenButtonHandler();
            EraserButtonHandlerFlag = true;
            EraserButtonHandler();
            MoveButtonFlag = true;
            MoveButtonHandler();
            ScaleButtonFlag = true;
            ScaleButtonHandler();

            PanelButtonHandlerFlag = true;
            PanelButtonHandler();
            AlertButtonHandlerFlag = true;
            AlertButtonHandler();
        }
        else {
            LockButtonHandlerFlag = false;
            LockImage.color = new Color(1, 1, 1, 1);
        }
    }
    /// <summary>
    /// 指定したタイルを編集不可にする
    /// </summary>
    private void TileLock() {
        if (!LockButtonHandlerFlag) {
            return;
        }
        Debug.Log("Lock");
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            Debug.LogWarning("タイル情報が空です");
            return;
        }
        if (tileInfo.Tile.name.Equals("EmptyTile(Clone)")) {
            Debug.LogWarning("タイルがありません");
            return;
        }
        if (!tileInfo.Editable) {
            Debug.LogWarning("そのタイルは編集できません");
            return;
        }
        tileInfo.Editable = false;
        tileInfo.Tile.transform.FindChild("LockImage").GetComponent<SpriteRenderer>().enabled = true;
    }
    private void TileUnLock() {
        if (!LockButtonHandlerFlag) {
            return;
        }
        Debug.Log("UnLock");
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            return;
        }
        if (tileInfo.Tile.name.Equals("EmptyTile(Clone)")) {
            return;
        }
        if (tileInfo.Editable) {
            return;
        }
        tileInfo.Editable = true;
        tileInfo.Tile.transform.FindChild("LockImage").GetComponent<SpriteRenderer>().enabled = false;
    }

    public Image EraserImage;
    private bool EraserButtonHandlerFlag;
    public void EraserButtonHandler() {
        if (!EraserButtonHandlerFlag) {
            EraserButtonHandlerFlag = true;
            EraserImage.color = new Color(0.5f, 0.5f, 0.5f, 1);

            PenButtonHandlerFlag = true;
            PenButtonHandler();
            LockButtonHandlerFlag = true;
            LockButtonHandler();
            MoveButtonFlag = true;
            MoveButtonHandler();
            ScaleButtonFlag = true;
            ScaleButtonHandler();

            PanelButtonHandlerFlag = true;
            PanelButtonHandler();
            AlertButtonHandlerFlag = true;
            AlertButtonHandler();
        }
        else {
            EraserButtonHandlerFlag = false;
            EraserImage.color = new Color(1, 1, 1, 1);

        }
    }
    /// <summary>
    /// 指定したタイルを削除(EmptyTileに変更)する
    /// </summary>
    private void TileErase() {
        if (!EraserButtonHandlerFlag) {
            return;
        }
        Debug.Log("Eraser");
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            Debug.LogWarning("タイル情報が空です");
            return;
        }
        if (tileInfo.Tile.name.Equals("EmptyTile(Clone)")) {
            Debug.LogWarning("タイルがありません");
            return;
        }
        if (!tileInfo.Editable) {
            Debug.LogWarning("そのタイルは編集できません");
            return;
        }

        TileManager.Instance.SetTileInfoFromPosUI(pos, EmptyTile);

    }

    public Image PenImage;
    private bool PenButtonHandlerFlag;
    public void PenButtonHandler() {
        if (!PenButtonHandlerFlag) {
            PenButtonHandlerFlag = true;
            PenImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            //非選択状態にする
            EraserButtonHandlerFlag = true;
            EraserButtonHandler();
            LockButtonHandlerFlag = true;
            LockButtonHandler();
            MoveButtonFlag = true;
            MoveButtonHandler();
            ScaleButtonFlag = true;
            ScaleButtonHandler();

            PanelButtonHandlerFlag = true;
            PanelButtonHandler();
            AlertButtonHandlerFlag = true;
            AlertButtonHandler();
        }
        else {
            PenButtonHandlerFlag = false;
            PenImage.color = new Color(1, 1, 1, 1);

        }
    }
    /// <summary>
    /// 指定したタイルを変更する
    /// </summary>
    private void TilePen() {
        if (!PenButtonHandlerFlag) {
            return;
        }
        Debug.Log("Pen");
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            Debug.LogWarning("タイル情報が空です");
            return;
        }
        if (tileInfo.Tile.name.Equals(TileManager.Instance.StockTile.name + "(Clone)")) {
            Debug.LogWarning("そのタイルはストックと同じです");
            return;
        }
        if (!tileInfo.Editable) {
            Debug.LogWarning("そのタイルは編集できません");
            return;
        }
        TileManager.Instance.SetTileInfoFromPosUI(pos, TileManager.Instance.StockTile);

    }

    public Image MoveImage;
    public GameObject EmptyTile;
    private bool MoveButtonFlag;
    public void MoveButtonHandler() {
        if (!MoveButtonFlag) {
            MoveButtonFlag = true;
            MoveImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            //非選択状態にする
            PenButtonHandlerFlag = true;
            PenButtonHandler();
            EraserButtonHandlerFlag = true;
            EraserButtonHandler();
            LockButtonHandlerFlag = true;
            LockButtonHandler();
            ScaleButtonFlag = true;
            ScaleButtonHandler();

            PanelButtonHandlerFlag = true;
            PanelButtonHandler();
            AlertButtonHandlerFlag = true;
            AlertButtonHandler();
        }
        else {
            MoveButtonFlag = false;
            MoveImage.color = new Color(1, 1, 1, 1);

        }
    }
    public GameObject HoldTile;
    private Vector2 prePos;
    /// <summary>
    /// 指定したタイルを移動する
    /// </summary>
    private void TileMoveEnter() {
        if (!MoveButtonFlag) {
            return;
        }
        Debug.Log("MoveEnter");
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            Debug.LogWarning("タイル情報が空です");
            return;
        }
        if (tileInfo.Tile.name.Equals("EmptyTile(Clone)")) {
            Debug.LogWarning("タイルが空です");
            return;
        }
        if (!tileInfo.Editable) {
            Debug.LogWarning("そのタイルは編集できません");
            return;
        }
        prePos = tileInfo.Tile.transform.localPosition;
        GameObject prefab = TileManager.Instance.GetTilePrefabFromName(tileInfo.Tile.name);
        HoldTile = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        HoldTile.GetComponent<Collider2D>().enabled = false;
        TileManager.Instance.SetTileInfoFromPosUI(pos, EmptyTile);
    }
    private void TileMoveStay() {
        if (!MoveButtonFlag) {
            return;
        }
        Debug.Log("MoveStay");
        if (HoldTile == null) {
            return;
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HoldTile.transform.position = pos;
        Debug.Log(HoldTile);
    }
    private void TileMoveExit() {

        if (!MoveButtonFlag) {
            return;
        }
        Debug.Log("MoveExit");
        if (HoldTile == null) {
            return;
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject prefab = TileManager.Instance.GetTilePrefabFromName(HoldTile.name);
        Destroy(HoldTile);
        HoldTile = null;
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo.Editable) {
            TileManager.Instance.SetTileInfoFromPosUI(pos, prefab);
        }
        else {
            TileManager.Instance.SetTileInfoFromPosUI(prePos, prefab);
        }

    }

    /// <summary>
    /// 終了
    /// </summary>
    private void Quit() {
        Application.Quit();
    }
    public void QuitButton() {
        Quit();
    }
    /* ここまで完了 */
    /*************************************/

    public Image ScaleImage;
    private bool ScaleButtonFlag;
    public void ScaleButtonHandler() {
        if (!ScaleButtonFlag) {
            ScaleButtonFlag = true;
            ScaleImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            //非選択状態にする
            PenButtonHandlerFlag = true;
            PenButtonHandler();
            EraserButtonHandlerFlag = true;
            EraserButtonHandler();
            LockButtonHandlerFlag = true;
            LockButtonHandler();
            MoveButtonFlag = true;
            MoveButtonHandler();

            PanelButtonHandlerFlag = true;
            PanelButtonHandler();
            AlertButtonHandlerFlag = true;
            AlertButtonHandler();
        }
        else {
            ScaleButtonFlag = false;
            ScaleImage.color = new Color(1, 1, 1, 1);

        }
    }
    /// <summary>
    /// 指定したタイルのサイズを変更する
    /// </summary>
    private void TileScaleUp() {
        if (!ScaleButtonFlag) {
            return;
        }
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            return;
        }
        if (!tileInfo.Editable) {
            return;
        }
        /* ここでTileManagerから削除処理をする(TileInfoを渡して処理させる) */
        Debug.Log("Scale!");
        tileInfo.Tile.transform.localScale *= 2;

    }
    private void TileScaleDown() {
        if (!ScaleButtonFlag) {
            return;
        }
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TileManager.TileInfo tileInfo = TileManager.Instance.GetTileInfoFromPosUI(pos);
        if (tileInfo == null) {
            return;
        }
        if (!tileInfo.Editable) {
            return;
        }
        /* ここでTileManagerから削除処理をする(TileInfoを渡して処理させる) */
        Debug.Log("Scale!");
        tileInfo.Tile.transform.localScale *= 0.5f;

    }
    /********************************************************************************************************************/

    public Slider CameraSlider;
    public GameObject Player;
    private void ChangeCameraPos() {
        Player.transform.localPosition = new Vector3(-5 + 19 * CameraSlider.value, 0, 0);
    }
}
