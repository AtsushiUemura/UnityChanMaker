using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
public class TapOverComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Transform tf { get { return transform; } }
    public Transform[] cTf = new Transform[2];

    private bool bigRollIsRunning;
    private bool smallRollIsRunning;
    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData) {
        tf.localScale = Vector3.one * 1.2f;
        StartCoroutine(BigRoll());
        StartCoroutine(SmallRoll());
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    // 
    public void OnPointerExit(PointerEventData eventData) {
        tf.localScale = Vector3.one;
        bigRollIsRunning = false;
        smallRollIsRunning = false;
        cTf[0].eulerAngles = Vector3.zero;
        cTf[1].eulerAngles = Vector3.zero;
    }

    private IEnumerator BigRoll() {
        if (bigRollIsRunning) {
            yield break;
        }
        bigRollIsRunning = true;
        while (bigRollIsRunning) {
            cTf[0].Rotate(new Vector3(0, 0, -1));
            yield return null;
        }
    }
    private IEnumerator SmallRoll() {
        if (smallRollIsRunning) {
            yield break;
        }
        smallRollIsRunning = true;
        while (smallRollIsRunning) {
            cTf[1].Rotate(new Vector3(0, 0, 1));
            yield return null;
        }
    }
}