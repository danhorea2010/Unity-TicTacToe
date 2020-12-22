using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    X,
    Y,
    EMPTY
}


public class Cell
{
    private int _x;
    private int _y;

    public int X {get {return this._x;} set { this._x = value;}}
    public int Y {get {return this._y;} set { this._y = value;}}

    // X or O or empty
    CellType token;

    public Cell(int x, int y){
        this._x = x;
        this._y = y;
        this.token = CellType.EMPTY;

    }

    public Cell(int x, int y, CellType token){
        this._x = x;
        this._y = y;
        this.token = token;

    }


    public void SetToken(CellType cellType){
        this.token = cellType;
    }

    public CellType GetToken(){
        return this.token;
    }


}
