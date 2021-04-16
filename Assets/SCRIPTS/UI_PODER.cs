using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PODER : MonoBehaviour
{

    Dictionary<string, int> spritePoder;

    [SerializeField]
    Sprite[] spritePoderes;

    Image Imagem;

    // Use this for initialization
    void Start(){
        Imagem = GetComponent<Image>();
        PreencherSpritePoderDic();
        Imagem.sprite = spritePoderes[spritePoder["Default"]];
    }

    public void MudarSpritePoder(string spriteName){
        Imagem.sprite = spritePoderes[spritePoder[spriteName]];
    }

    //Inner helper methods
    void PreencherSpritePoderDic() {
        spritePoder = new Dictionary<string, int> {
            { "Default", 0 },
            { "Congelar", 1 },
            { "Inverter", 2 },
            { "Nitro", 3 },
            { "x2", 4 }
        };
    }
}
