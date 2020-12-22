using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;

    public static GridManager Instance {get {return _instance; }}

    [SerializeField]
    private Grid grid;

    public void SetGrid(Grid grid){
        this.grid = grid;
    }

    public Grid GetGrid(){
        return this.grid;
    }

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            this.grid = new Grid();
            _instance = this;


        }
    }

}
