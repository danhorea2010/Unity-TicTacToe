using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class GridUi : MonoBehaviour
{
    private static GridUi _instance;
    public static GridUi Instance {get {return _instance; }}

    [SerializeField]
    private TextMeshProUGUI scoreXText;
    [SerializeField]
    private TextMeshProUGUI scoreYText;

    // TODO: Duplicate code from TicTacToe.cs
    private string scoreXKey = "ScoreXKey";
    private string scoreYKey = "ScoreYKey";

    private int scoreX;
    private int scoreY;

    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Transform cellUiElement;
    [SerializeField]
    private int gridSize = 150;
    private List<CellUiObject> cellUiElements;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private TextMeshProUGUI victoryText;


    private delegate void CheckWinDelegate(int index);
    private CheckWinDelegate checkWinDelegate;

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;


        }
    }

    private void MakeGrid(){
        Cell[] cells = this.grid.GetCells();

        for(int i =0; i<this.grid.GetCellSize();++i){
            RectTransform rect =
                Instantiate(cellUiElement, parent)
                as RectTransform;

            rect.gameObject.SetActive(true);

            rect.anchoredPosition =
                new Vector3(cells[i].X*gridSize, -(cells[i].Y*gridSize) ,0f);

            // Get sprite based on state
            Image imageRef = rect.GetComponent<Image>();
            imageRef.sprite = CellAssetManager.Instance.GetCellSprite(cells[i]);

            Button button = rect.GetComponent<Button>();

            // Set onClick function for button;
            int indexCopy = i;
            button.onClick.AddListener(delegate{
                    //TicTacToe.Instance.OnCellPress(indexCopy);
                    checkWinDelegate.Invoke(indexCopy);
                });

            CellUiObject cellObj =
                new CellUiObject(rect, imageRef,button);

            this.cellUiElements.Add(cellObj);
        }
         this.scoreXText.SetText("X: " + scoreX.ToString());
         this.scoreYText.SetText("Y: " + scoreY.ToString());
   }

    public TextMeshProUGUI GetText(){
        return this.victoryText;
    }


    public IEnumerator RestartGameCoroutine(){
        yield return new WaitForSeconds(.5f);
        // TODO: Persistant data?
        Application.LoadLevel(Application.loadedLevel);

    }

    public void DisableButtons(){
        foreach(var obj in cellUiElements){
            Destroy(obj.GetButton());
        }
    }


    public void UpdateGrid(){
        Cell[] cells = this.grid.GetCells();

        for(int i=0;i<this.grid.GetCellSize();++i){
            this.cellUiElements[i].GetImage().sprite
                = CellAssetManager.Instance.GetCellSprite(cells[i]);

        }


        // Update score stuff

        this.scoreXText.SetText("X: " + scoreX.ToString());
        this.scoreYText.SetText("Y: " + scoreY.ToString());

    }

    // Start is called before the first frame update
    void Start()
    {
        this.scoreX = PlayerPrefs.GetInt(scoreXKey,0);
        this.scoreY = PlayerPrefs.GetInt(scoreYKey,0);

        this.cellUiElements = new List<CellUiObject>();
        this.grid = GridManager.Instance.GetGrid();
        this.checkWinDelegate = new CheckWinDelegate(TicTacToe.Instance.OnCellPress);

        MakeGrid();

    }

}
