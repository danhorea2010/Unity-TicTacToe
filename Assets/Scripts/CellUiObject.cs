using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellUiObject
{
    private RectTransform rect;
    private Image image;
    private Button button;

    public CellUiObject(RectTransform rect, Image image, Button button){
        this.rect   = rect;
        this.image  = image;
        this.button = button;
    }

    public RectTransform GetRect(){
        return this.rect;
    }

    public Image GetImage(){
        return this.image;
    }

    public Button GetButton(){
        return this.button;
    }



}
