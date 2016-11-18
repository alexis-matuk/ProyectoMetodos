using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResizeButton : MonoBehaviour {

    [SerializeField]
    int cols;
	// Use this for initialization
	void Start () {
        RectTransform rect = transform.GetComponent<RectTransform>();
        float width = rect.rect.width / (float)cols;
        GridLayoutGroup grid = transform.GetComponent<GridLayoutGroup>();
        float oldHeight = grid.cellSize.y;
        grid.cellSize = new Vector2(width, oldHeight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
