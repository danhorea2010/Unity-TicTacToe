using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellAssetManager : MonoBehaviour
{
    private static CellAssetManager _instance;

    public static CellAssetManager Instance {get {return _instance; }}

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;


        }
    }

    // Sprites based on Cell state

    [SerializeField] private CellAssetObjects assetObject;

    public Sprite GetCellSprite(Cell cell){

        CellType cellType = cell.GetToken();

        switch(cellType){
            case CellType.X:
                return assetObject.XCellSprite;
            case CellType.Y:
                return assetObject.YCellSprite;
            case CellType.EMPTY:
                return assetObject.emptyCellSprite;

        }

        return null;
    }
}
