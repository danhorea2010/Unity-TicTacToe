using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private Cell[] cells;
    private int cellSize = 9;
    private int rowWidth = 3;

    public Grid(){
        this.cells = new Cell[cellSize];

        for(int i =0;i<cellSize;++i){
            this.cells[i] = new Cell(i%rowWidth, i/rowWidth);
           // this.cells[i].X = i % cellSize;
           // this.cells[i].Y = i / cellSize;
           // this.cells[i].SetToken(CellType.EMPTY);
        }
    }

    public Cell[] GetCells(){
        return this.cells;
    }

    public int GetCellSize(){
        return this.cellSize;
    }

    public void SetAt(int x, int y, CellType cellType){
        this.cells[x+y*cellSize].SetToken(cellType);
    }

    public CellType GetAt(int x, int y){
        return this.cells[x+y*cellSize].GetToken();
    }


}
