using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tubarao : MonoBehaviour {

    //constantes
    const string TAG_POWER_DOWN = "PowerDown";
    const string TAG_POWER_UP = "PowerUP";

    [SerializeField]
    [Range(10, 30)]
    float playerRunSpeed;

    [SerializeField]
    Tubarao tubaraoInimigo;

    //Referencia para o UI que tem as imagens dos lixos
    GameObject _ui;
    //Referencia para o UI_PODER. UI que tem as imagens dos poderes
    [SerializeField]
    UI_PODER _ui_Poder;

    //Referencia para a tag do lixo que deve ser pego
    public string Lixo_Tag { get; set; }

    //Referencia para o sprite inverter
    [SerializeField]
    GameObject inverterAnimado;

    //Referencia para o sprite do tubarao com a boca aberta
    [SerializeField]
    Sprite[] tubaraoBocaSprite;

    Text tubaraoText;

    SpriteRenderer mySpriteRenderer;

    public Vector2 speed = new Vector2(10, 10);

    public int pontos { get; set; }

    public float vrau;

    //Variaveis para contar a quantidade de lixos coletados de cada tipo
    public int contPlastico;
    public int contPapel;
    public int contVidro;
    public int contMetal;

    //Referencia para o corpo rigido
    Rigidbody2D PlayerRB;
    //Referencia para o animator
    Animator playerAnimator;
    //Referencia para o collider
    CapsuleCollider2D playerCapCollider;
    //Referencia para o sprite renderer
    SpriteRenderer playerSpriteRenderer;
    //Powerup pego
    IPowerUP powerUP;

    //Definicao de controle
    KeyCode keyCodeA, keyCodeX;

    //variaveis de controle 
    float playerXDir;
    float playerYDir;
    bool BotaoX;
    bool A,BotaoA;
    bool auxiliarCongelado = false;
    bool temPowerUP = false, temPowerDown = false, temPower = false;
    public bool auxiliarInverter = false;
    bool auxiliarVel = false;
    bool auxiliarMult = false;
    bool Mult_Pts = false;

    //Propriedades
    public float PlayerRunSpeed { get; set; }
    public float Inverter { get; set; }

    //Diferenciar controles tubarao1/tubarao2
    string analogicoHor = "", analogicoVer = "";

    // Use this for initialization
    void Start() {
        PlayerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapCollider = GetComponent<CapsuleCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        pontos = 0;
        contPlastico = 0;
        contPapel = 0;
        contVidro = 0;
        contMetal = 0;

    PlayerRunSpeed = 10.0f;
        Inverter = 1.0f;
        _ui = GameObject.Find("Image_Lixo");

        if (transform.CompareTag("Tubarao1")) {
            analogicoHor = "Left Analog Horizontal";
            analogicoVer = "Left Analog Vertical";
            tubaraoText = GameObject.FindWithTag("Score1").GetComponent<Text>();
            keyCodeA = KeyCode.Joystick1Button0;
            keyCodeX = KeyCode.Joystick1Button2; 
        } else {
            analogicoHor = "Left Analog Horizontal 2";
            analogicoVer = "Left Analog Vertical 2";
            tubaraoText = GameObject.FindWithTag("Score2").GetComponent<Text>();
            keyCodeA = KeyCode.Joystick2Button0;
            keyCodeX = KeyCode.Joystick2Button2;
        }
    }

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {

        BotaoA = Input.GetKey(keyCodeA);
        BotaoX = Input.GetKeyDown(keyCodeX);
        
        //Left Analog Vertical
        playerXDir = Input.GetAxis(analogicoHor) * Time.deltaTime * PlayerRunSpeed * Inverter;
        playerYDir = Input.GetAxis(analogicoVer) * Time.deltaTime * PlayerRunSpeed * Inverter;
        Mover();

        //trecho para abrir a boca do tubarao
        if (BotaoA)//Boca aberta
        {
            playerSpriteRenderer.sprite = tubaraoBocaSprite[1];
        }
        else//Boca fechada
        {
            playerSpriteRenderer.sprite = tubaraoBocaSprite[0];
        }

        //fim do trecho para abrir a boca
        if (temPower && BotaoX){
            if (temPowerUP) {
                powerUP.ExecutePowerUP(this);
                temPowerUP = false;
            } else if (temPowerDown) {
                powerUP.ExecutePowerUP(tubaraoInimigo);
                temPowerDown = false;
            }
            temPower = false;
            
            _ui_Poder.MudarSpritePoder("Default");
        }

    }

    //Metodo para mover o tubarao
    private void Mover()
    {
        float xMin = -48.0f;
        float xMax = 37.0f;
        float yMin = -17.50f;
        float yMax = 21.0f;
        playerXDir = Mathf.Clamp(playerXDir, xMin, xMax);
        transform.Translate(playerXDir, playerYDir, 0);
        Vector3 tempPosition = transform.position;
        tempPosition.x = Mathf.Clamp(transform.position.x, xMin, xMax);
        tempPosition.y = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = tempPosition;

        Vector3 tubaScale = new Vector3(1, 1, 1);
        if (Input.GetAxis(analogicoHor) !=0) {
            tubaScale.x = Mathf.Sign(Input.GetAxis(analogicoHor)) *-1;
            transform.localScale = tubaScale;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (BotaoA) {
            //Tratar colisao com os lixos
            if (collision.tag.Equals(Lixo_Tag)) {
                pontos += collision.GetComponent<Lixo>().Ponto;
                TipoDePontuacao(collision.tag, collision);
            }
            else if (collision.CompareTag(TAG_POWER_UP) || collision.CompareTag(TAG_POWER_DOWN)) {
                if (temPower)
                    Destroy(collision.gameObject);
                else {
                    if (collision.CompareTag("PowerUP")) {
                        temPowerUP = true;
                        temPowerDown = false;
                        SetarPoder(collision);
                    } else if (collision.CompareTag("PowerDown")) {
                        temPowerDown = true;
                        temPowerUP = false;
                        SetarPoder(collision);
                    }
                }
            } 
            else {
                if (pontos == 0) {
                    Destroy(collision.gameObject);
                }
                if (pontos > 0) {
                    pontos--;
                    AtualizaPlacar(collision, 0);
                }
            }
        }
        
    }

    

    public void AtivarPontoDuplo() {
        StartCoroutine(Multiplicar());
        Mult_Pts = true;
    }

    //Multiplicar()
    IEnumerator Multiplicar()
    {
        //yield return new WaitForSecondsRealtime(8.0f);
        yield return new WaitForSeconds(8.0f);
        Mult_Pts = false;
    }

    //Inner helper
    void SetarPoder(Collider2D collision) {
        temPower = true;
        _ui_Poder.MudarSpritePoder(collision.gameObject.name.Replace("(Clone)", ""));
        collision.GetComponent<SpriteRenderer>().enabled = false;
        collision.GetComponent<Collider2D>().enabled = false;
        powerUP = collision.gameObject.GetComponent<IPowerUP>();
    }

    void TipoDePontuacao(string nomeLixo, Collider2D collision) {
		if (nomeLixo.Equals("Plastico"))
		{
			contPlastico++;
			AtualizaPlacar(collision, 4);
		}
		else if (nomeLixo.Equals("Papel"))
		{
			contPapel++;
			AtualizaPlacar(collision, 1);
		}
		else if (nomeLixo.Equals("Metal"))
		{
			contMetal++;
			AtualizaPlacar(collision, 2);
		}
		else if (nomeLixo.Equals("Vidro")) {
			contVidro++;
			AtualizaPlacar(collision, 3);
		}    
    }

    void AtualizaPlacar(Collider2D collision, int anim) {
        tubaraoText.text = pontos.ToString();
        collision.GetComponentInChildren<Lixo>().ExecutaAnimacao(anim);
        Destroy(collision.gameObject);
    }
}
