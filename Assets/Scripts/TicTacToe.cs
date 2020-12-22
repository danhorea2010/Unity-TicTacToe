using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TicTacToe : MonoBehaviour
{
    private static TicTacToe _instance;
    public static TicTacToe Instance {get {return _instance; }}

    private bool won;
    private CellType currentTurn;
    private Grid grid;

    private Ai ai;

    private string scoreXKey = "ScoreXKey";
    private string scoreYKey = "ScoreYKey";

    private int scoreX;
    private int scoreY;

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;

        }
    }

    private void Start(){

        // Load score
        this.scoreX = PlayerPrefs.GetInt(scoreXKey,0);
        this.scoreY = PlayerPrefs.GetInt(scoreYKey,0);


        won = false;
        this.grid = GridManager.Instance.GetGrid();
        currentTurn = CellType.X;
        this.ai = new Ai();
    }

    private List<Cell> EmptyCells(Grid grid){
        List<Cell> emptyCells = new List<Cell>();
        Cell[] cells = grid.GetCells();

        for(int i =0;i<grid.GetCellSize();++i)
        {
            if(cells[i].GetToken() == CellType.EMPTY){
                emptyCells.Add(cells[i]);
            }
        }

        return emptyCells;
    }

    void AiTurn(CellType cellType){
        List<Cell> emptyCells = EmptyCells(this.grid);
        int depth = emptyCells.Count;

        // Or gameOver
        if(depth == 0)
            return;


        Cell[] cells = this.grid.GetCells();
        int x,y;
        Move move;
        if(depth == 9){

            x = UnityEngine.Random.Range(0,3);
            y = UnityEngine.Random.Range(0,3);

            while(cells[x+y*3].GetToken() != CellType.EMPTY){
                x = UnityEngine.Random.Range(0,3);
                y = UnityEngine.Random.Range(0,3);
           }

        }
        else{
            move = ai.Minimax(this.grid, depth, cellType);
            x = move.x;
            y = move.y;
        }

        cells[x+y*3].SetToken(cellType);

    }


    void Update(){

        // Ai is Y
        if(currentTurn == CellType.Y && !won){
            AiTurn(CellType.Y);
            currentTurn = CellType.X;
            GridUi.Instance.UpdateGrid();
            CheckWin();
        }
        //else if(!won){
        //    AiTurn(CellType.X);
        //    currentTurn = CellType.Y;
        //    GridUi.Instance.UpdateGrid();
        //    CheckWin();
        //}

    }

    public bool CheckWinPlayer(CellType cellType){

        Cell[] cells = this.grid.GetCells();
        Tuple<CellType,CellType,CellType>[] cellsCond = {

            (Tuple.Create(cells[0].GetToken(),cells[1].GetToken(),cells[2].GetToken())),
            (Tuple.Create(cells[3].GetToken(),cells[4].GetToken(),cells[5].GetToken())),
            (Tuple.Create(cells[6].GetToken(),cells[7].GetToken(),cells[8].GetToken())),
            (Tuple.Create(cells[0].GetToken(),cells[3].GetToken(),cells[6].GetToken())),
            (Tuple.Create(cells[1].GetToken(),cells[4].GetToken(),cells[7].GetToken())),
            (Tuple.Create(cells[2].GetToken(),cells[5].GetToken(),cells[8].GetToken())),
            (Tuple.Create(cells[0].GetToken(),cells[4].GetToken(),cells[8].GetToken())),
            (Tuple.Create(cells[2].GetToken(),cells[4].GetToken(),cells[6].GetToken()))
        };

        List<Tuple<CellType,CellType,CellType>> conditionList = new List<Tuple<CellType,CellType,CellType>>(
            cellsCond
        );

        var play = Tuple.Create(cellType, cellType, cellType);

        if(conditionList.Contains(play)){
            return true;
        }

        return false;
    }

   public void OnCellPress(int index){
       // Assume player is X at this point...

        Cell[] cells = this.grid.GetCells();
        if( cells[index].GetToken() == CellType.EMPTY )
            {
               // Should check if player turn or Ai turn

               if( currentTurn == CellType.X )
               {
                   currentTurn = CellType.Y;
                   cells[index].SetToken(CellType.X);
               }//else{
               //    currentTurn = CellType.X;
              // }
            }

        GridUi.Instance.UpdateGrid();
        if(!won){
            CheckWin();
        }
    }

   public void CheckWin()
   {
       TextMeshProUGUI victoryText = GridUi.Instance.GetText();


          // Check for X
          if ( TicTacToe.Instance.CheckWinPlayer(CellType.X) ){
              victoryText.SetText("X Won");
              won = true;
              StartCoroutine(GridUi.Instance.RestartGameCoroutine());
              GridUi.Instance.DisableButtons();

              this.scoreX +=1;

              PlayerPrefs.SetInt(scoreXKey, this.scoreX);
          }
          // Check for Y
          else if ( TicTacToe.Instance.CheckWinPlayer(CellType.Y) ){
              victoryText.SetText("Y Won");
              won = true;
              StartCoroutine(GridUi.Instance.RestartGameCoroutine());
              GridUi.Instance.DisableButtons();

              this.scoreY +=1;
              PlayerPrefs.SetInt(scoreYKey, this.scoreY);
          }

          Cell[] cells = this.grid.GetCells();
          bool anyEmpty = false;
          for(int i =0;i<this.grid.GetCellSize()&&!anyEmpty; ++i){
              if(cells[i].GetToken() == CellType.EMPTY)
                  anyEmpty = true;
          }

          if(!anyEmpty && !won){
              victoryText.SetText("Draw...");
              won = true;
              StartCoroutine(GridUi.Instance.RestartGameCoroutine());
              GridUi.Instance.DisableButtons();
          }

      }

   public bool GetWon(){
       return this.won;
   }

}
