using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Move{
        public int x;
        public int y;
        public int score;

        public Move(int _x, int _y, int score){
            this.x = _x;
            this.y = _y;
            this.score = score;
        }
    }



public class Ai
{

    private const int HUMAN = -1;
    private const int COMP  = +1;


    public Move Minimax(Grid grid, int depth, CellType player ){
        // Assume AI is Y...
        Move bestMove;
        Move mScore;
        int fakeInfinity = 9999999;

        if( player == CellType.Y )
        {
            bestMove = new Move(-1,-1, -fakeInfinity);
        }
        else
        {
            bestMove = new Move(-1,-1, +fakeInfinity);
        }

        if ( depth == 0 || GameOver(grid) ){
            int score = Evaluate(grid);
            return new Move(-1,-1, score);
        }


        List<Cell> emptyCells = EmptyCells(grid);
        Cell[] cells = grid.GetCells();

        foreach(var cell in emptyCells){
            int x = cell.X;
            int y = cell.Y;


            cells[x+y*3].SetToken(player);

            if(player == CellType.Y)
            {
                mScore = Minimax(grid, depth-1, CellType.X);
            }
            else{
                mScore = Minimax(grid, depth-1, CellType.Y);
            }

            cells[x+y*3].SetToken(CellType.EMPTY);
            mScore.x = x;
            mScore.y = y;

            if(player == CellType.Y){
                if(mScore.score > bestMove.score){
                    bestMove = mScore;
                }
            }
            else{
                if(mScore.score < bestMove.score){
                    bestMove = mScore;
                }
            }

        }


        return bestMove;
    }

    private bool Wins(Grid grid, CellType player){
        return TicTacToe.Instance.CheckWinPlayer(player);
    }

    private int NoEmptyCells(Grid grid){
        int eCells = 0;
        Cell[] cells = grid.GetCells();

        for(int i=0;i<grid.GetCellSize();++i){
            if(cells[i].GetToken() == CellType.EMPTY)
            {
                ++eCells;
            }
        }

        return eCells;
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


    private int Evaluate(Grid grid) {
        int score = 0;

        if(Wins(grid, CellType.Y)){
            score = +1;
        }
        else if (Wins(grid, CellType.X)){
            score = -1;
        }

        return score;

    }

    private bool GameOver(Grid grid) {
        return Wins(grid, CellType.X)
            || Wins(grid, CellType.Y);
    }


}
