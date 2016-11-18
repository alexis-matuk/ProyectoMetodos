using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateTable : MonoBehaviour {

    public int rows = 4;
    public int columns = 4;
    private float buttonWidth;                                        //Change
    private float buttonHeight;                                        //Change
    public Button prefab;
    private Button button;
    void Start() {
        RectTransform myRect = GetComponent<RectTransform>();        //Change
        buttonHeight = myRect.rect.height / (float)rows;            //Change
        buttonWidth = myRect.rect.width / (float)columns;            //Change
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(buttonWidth, buttonHeight);

    }
}
