using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    public static bool Pausado;
    private int cont = 0;

    //GO do Menu pause
    public GameObject GO;

    KeyCode Kstart1, Kstart2;

    bool start1, start2;

    // Use this for initialization
    void Start() {

        GO.SetActive(false);
        Kstart1 = KeyCode.Joystick1Button7;
        Kstart2 = KeyCode.Joystick2Button7;
    }

    // Update is called once per frame
    void Update() {

        start1 = Input.GetKey(Kstart1);
        start2 = Input.GetKey(Kstart2);

        if (start1 || start2)
        {
            Pausar();
            
        }


    }

    public void Pausar()
    {
        GO.SetActive(true);
        Time.timeScale = 0;
        Pausado = true;
    }

    public void Resumir()
    {
        Time.timeScale = 1;
        Pausado = false;
        GO.SetActive(false);
    }

     public void TelaIncial(){
        Time.timeScale = 1;
        Pausado = false;
        SceneManager.LoadScene("TesteUI");
     }

    public void Reiniciar(){
        Time.timeScale = 1;
        Pausado = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    
}
