using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    
    public int placarTuba1;
  
    public int placarTuba2;

    //Tubarao1
    public int scorePlastico;
    public int scoreVidro;
    public int scoreMetal;
    public int scorePapel;

    //Tubarao 2
    public int scorePlastico_2;
    public int scoreVidro_2;
    public int scoreMetal_2;
    public int scorePapel_2;

    //Implementacao do singleton para o GameSession
    void Awake() {


        if (FindObjectsOfType<GameSession>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

}
