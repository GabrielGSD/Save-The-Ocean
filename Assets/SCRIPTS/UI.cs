using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    [SerializeField]
    Sprite[] lixoSprites;

    [SerializeField]
    GameObject trocaLixoVFX;

    [SerializeField]
    Tubarao[] tubarao;

    //Tags sprites
    public string tag_sprite = "sprite";

    Image Imagem;

    Text Cronometro;

    GameObject plastico;

    public AudioClip shootSound;
    private AudioSource audioObj;
    int indiceLixoAnterior;

    public float cronometro = 59f;
    public int aux = 3;
    public int fim = 0;

    public bool deletLixos = false;

    // Use this for initialization
    void Start () {
        Imagem = GetComponent<Image>();
        Cronometro = GameObject.FindWithTag("Cronometro").GetComponent<Text>();
        InvokeRepeating("SetImage", 0.0f, Random.Range(10f, 30f)); // tempo para mudar a sprite
        audioObj = GetComponent<AudioSource>();
        indiceLixoAnterior = Random.Range(0, lixoSprites.Length);
    }
	
	// Update is called once per frame
	void Update () {

        //Migue click
        if (Input.GetMouseButtonDown(0)){
            SetImage();
        }

        if (cronometro > 0f)
        {
            cronometro -= Time.deltaTime;
            Cronometro.text = aux + ":" + cronometro.ToString("F0");

        }
        else
        {
            aux--;
            cronometro = 59.0f;
            fim++;
        }

        if (aux == -1 && fim == 4)
        {
            Time.timeScale = 0;
            SalvaPlacar();
            SceneManager.LoadScene("TelaVitoria");
        }

        if (deletLixos)
        {
            Destruir();
        }
        /* --------Cronometro Crescente--------
         * 
        cronometro += Time.deltaTime;
        Cronometro.text = aux + ":" + cronometro.ToString("F0");

        if(cronometro >= 59.0f)
        {
            aux ++;
            cronometro *= 0.0f; ;
        }
       
        if(aux == 3     ) // Tem que colocar qual jogador ganhou
        {
            
        }*/

    }

    public void SetImage()
    {
        //Garantir que nao havera lixo repetido seguidamente
        int indiceLixoAtual;
        do {
            indiceLixoAtual = Random.Range(0, lixoSprites.Length);
        } while (indiceLixoAtual == indiceLixoAnterior);
        indiceLixoAnterior = indiceLixoAtual;

        Imagem.sprite = lixoSprites[indiceLixoAtual];
        transform.tag = lixoSprites[indiceLixoAtual].name;
        //Instanciar efeito de particula
        LixoEfeito();

        //Avisar ao tubarão que o lixo mudou e atualizar a tag
        foreach (Tubarao tuba in tubarao) {
            tuba.Lixo_Tag = transform.tag;
        }
    }

    public void Destruir() {

        Destroy(GameObject.FindGameObjectWithTag("Plastico"));
        Destroy(GameObject.FindGameObjectWithTag("Vidro"));
        Destroy(GameObject.FindGameObjectWithTag("Papel"));
        Destroy(GameObject.FindGameObjectWithTag("Metal"));
        deletLixos = false;

    }

    void DestroyAllObjects()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        Destroy(enemy);
    }

    //TODO VERIFICAR COR
    void LixoEfeito() {
        GameObject trocaLixoVFXCLone = Instantiate(trocaLixoVFX, transform);
        ParticleSystem.MainModule lixoEfeitoMain = trocaLixoVFXCLone.GetComponent<ParticleSystem>().main;
        //Coordenada do pixel retirada do SpriteEditor
        lixoEfeitoMain.startColor = Imagem.sprite.texture.GetPixelBilinear(0.60482782125473f, 0.702864944934845f);
        
        //audioObj.PlayOneShot(shootSound, 0);
        Destroy(trocaLixoVFXCLone, 3f);
    }

    void SalvaPlacar() {
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.placarTuba1 = tubarao[0].pontos;
        gameSession.placarTuba2 = tubarao[1].pontos;

        //Lixos Coletados pelo player 1
        gameSession.scorePlastico = tubarao[0].contPlastico;
        gameSession.scoreVidro = tubarao[0].contVidro;
        gameSession.scoreMetal = tubarao[0].contMetal;
        gameSession.scorePapel = tubarao[0].contPapel;

        //Lixos Coletados pelo player 2
        gameSession.scorePlastico_2 = tubarao[1].contPlastico;
        gameSession.scoreVidro_2 = tubarao[1].contVidro;
        gameSession.scoreMetal_2 = tubarao[1].contMetal;
        gameSession.scorePapel_2 = tubarao[1].contPapel;
        print("SALVAR");
    }
}
