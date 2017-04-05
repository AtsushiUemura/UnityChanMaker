using UnityEngine;
using System.Collections;

public class GridLine : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GetComponent<LineRenderer>().sortingLayerName = "GridLine";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
