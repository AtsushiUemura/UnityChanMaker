using UnityEngine;
using System.Collections.Generic;

public class GridManager : SingletonMonoBehaviour<GridManager> {

    public GameObject LinePrefab;
    public int Height;
    public int Width;
    public Vector3 VerticalEndPos;
    public Vector3 HorizontalEndPos;
    public Vector3 Offset;
    public float WidthDelta;
    public float LineWidth;

    public List<LineRenderer> verticalLineList = new List<LineRenderer>();
    public List<LineRenderer> horizontalLineList = new List<LineRenderer>();

    #region
    void Awake() {
        if (this != Instance) {
            Destroy(this);
            return;
        }
    }

    void Start() {
        DrawGrid(Height, Width);
    }
    #endregion

    /// <summary>
    /// ラインを描画
    /// </summary>
    /// <param name="height"></param>
    /// <param name="width"></param>
    private void DrawGrid(int height, int width) {

        DeleteGrid();

        float posX = width * WidthDelta + Offset.x;
        float posY = height * WidthDelta + Offset.y;
        VerticalEndPos = new Vector3(0, posY, 0);
        HorizontalEndPos = new Vector3(posX, 0, 0);

        Vector3 startPosX = Offset;
        Vector3 startPosY = Offset;
        Vector3 endPosX = VerticalEndPos;
        Vector3 endPosY = HorizontalEndPos;

        for (int i = 0; i < width + 1; i++) {
            startPosX.x = i * WidthDelta + Offset.x;
            endPosX.x = i * WidthDelta + Offset.x;
            LineRenderer line = CreateLine(LineWidth, LineWidth, startPosX, endPosX);
            verticalLineList.Add(line);
        }
        for (int i = 0; i < height + 1; i++) {
            startPosY.y = i * WidthDelta + Offset.y;
            endPosY.y = i * WidthDelta + Offset.y;
            LineRenderer line = CreateLine(LineWidth, LineWidth, startPosY, endPosY);
            horizontalLineList.Add(line);
        }
        //UIManager.Instance.TileControl();

    }
    /// <summary>
    /// ラインを生成
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="startColor"></param>
    /// <param name="endColor"></param>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    private LineRenderer CreateLine(float start, float end, Vector3 startPos, Vector3 endPos) {
        GameObject clone = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity) as GameObject;
        LineRenderer line = clone.GetComponent<LineRenderer>();
        line.sortingLayerName = "GridLine";
        line.SetWidth(start, end);
        line.SetVertexCount(2);
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
        return line;
    }
    /// <summary>
    /// グリッド削除
    /// </summary>
    private void DeleteGrid() {
        if (verticalLineList.Count != 0) {
            foreach (var item in verticalLineList) {
                Destroy(item.gameObject);
            }
            verticalLineList.Clear();
        }
        if (horizontalLineList.Count != 0) {
            foreach (var item in horizontalLineList) {
                Destroy(item.gameObject);
            }
            horizontalLineList.Clear();
        }
    }
    /// <summary>
    /// グリッド表示
    /// </summary>
    private void DisplayGrid() {
        if (verticalLineList.Count != 0) {
            foreach (var item in verticalLineList) {
                item.enabled = true;
            }
        }
        if (horizontalLineList.Count != 0) {
            foreach (var item in horizontalLineList) {
                item.enabled = true;
            }
        }
    }
    /// <summary>
    /// グリッド非表示
    /// </summary>
    private void HiddenGrid() {
        if (verticalLineList.Count != 0) {
            foreach (var item in verticalLineList) {
                item.enabled = false;
            }
        }
        if (horizontalLineList.Count != 0) {
            foreach (var item in horizontalLineList) {
                item.enabled = false;
            }
        }
    }

}
