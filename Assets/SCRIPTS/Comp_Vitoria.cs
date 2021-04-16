using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // chamar tela inicial

public class Comp_Vitoria : MonoBehaviour {

    GameSession gameSession;

    //Referencia para o sprite do tubarao com a boca aberta
    [SerializeField]
    Sprite[] tubaraoBocaSprite;

    private SpriteRenderer mySpriteRenderer;
    private Text ScoreVit;

    private Text ScorePlastico;
    private Text ScoreMetal;
    private Text ScoreVidro;
    private Text ScorePapel;

    private Text ScorePlastico_2;
    private Text ScoreMetal_2;
    private Text ScoreVidro_2;
    private Text ScorePapel_2;


    //Referencia para o sprite renderer
    SpriteRenderer playerSpriteRenderer;

    //bool a;

    // Use this for initialization
    void Start () {

        //img = GetComponent<Image>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        GameSession gameSession = FindObjectOfType<GameSession>();
        ScoreVit = GameObject.FindWithTag("ScoreVitoria").GetComponent<Text>();

        ScorePlastico = GameObject.FindWithTag("ScorePlastico").GetComponent<Text>();
        ScoreVidro = GameObject.FindWithTag("ScoreVidro").GetComponent<Text>();
        ScoreMetal = GameObject.FindWithTag("ScoreMetal").GetComponent<Text>();
        ScorePapel = GameObject.FindWithTag("ScorePapel").GetComponent<Text>();

        ScorePlastico_2 = GameObject.FindWithTag("ScorePlastico2").GetComponent<Text>();
        ScoreVidro_2 = GameObject.FindWithTag("ScoreVidro2").GetComponent<Text>();
        ScoreMetal_2 = GameObject.FindWithTag("ScoreMetal2").GetComponent<Text>();
        ScorePapel_2 = GameObject.FindWithTag("ScorePapel2").GetComponent<Text>();

        print("Tuba1 " + gameSession.placarTuba1 + "  Tuba2 " + gameSession.placarTuba2);

        ScorePlastico.text = gameSession.scoreMetal.ToString();
        ScoreMetal.text = gameSession.scoreMetal.ToString();
        ScoreVidro.text = gameSession.scoreVidro.ToString();
        ScorePapel.text = gameSession.scorePapel.ToString();

        ScorePlastico_2.text = gameSession.scoreMetal_2.ToString();
        ScoreMetal_2.text = gameSession.scoreMetal_2.ToString();
        ScoreVidro_2.text = gameSession.scoreVidro_2.ToString();
        ScorePapel_2.text = gameSession.scorePapel_2.ToString();

        if (gameSession.placarTuba1 > gameSession.placarTuba2)
        {
            playerSpriteRenderer.sprite = tubaraoBocaSprite[0];

            ScoreVit.text = gameSession.placarTuba1.ToString();

        }
        else if(gameSession.placarTuba1 < gameSession.placarTuba2)
        {
            playerSpriteRenderer.sprite = tubaraoBocaSprite[1];

            ScoreVit.text = gameSession.placarTuba2.ToString();
        }
        else
        {
            ScoreVit.text = "  ";
        }

    }

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
     
    }

    // Update is called once per frame
    void Update () {



    }

}
