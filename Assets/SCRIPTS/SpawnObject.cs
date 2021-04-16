using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnObject : MonoBehaviour
{

    public float spawnTime;
    public Transform[] spawnPoints;
    public GameObject[] objetos;
    // public float moveSpeed = 1.5f;
    public bool aux;

    private int i = 0;
    
    [SerializeField]
    private List<GameObject> lista = new List<GameObject>();

    [SerializeField]
    Dictionary<int, GameObject> dictShuffle = new Dictionary<int, GameObject>();

    [SerializeField]
    private List<int> posicoes = new List<int>();

    [SerializeField]
    private int indice = 0;
    [SerializeField]
    private int indice2 = 10;

    void Start()
    {
        //Spawnar novos lixos ao trocar a cor do tipo de lixo ou quando todos os lixos tiverem sido coletados
        spawnTime = 1f;

        //  InvokeRepeating(string methodName, float time, float repeatRate)
        //InvokeRepeating("Spawn", spawnTime, 3f);
        aux = true;

        shuffle();

        StartCoroutine(Spawn());

    }

    void FixedUpdate()
    {
        if (aux == true)
        {
            var vector = new Vector2(0, Time.deltaTime * Random.Range(2.0f, 7.0f));
            gameObject.transform.Translate(vector);
        }
        else
        {
            var vector = new Vector2(0, Time.deltaTime * Random.Range(2.0f, 5.0f)) * -1;
            gameObject.transform.Translate(vector);
        }
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            lista.Clear();
            posicoes.Clear();
            dictShuffle.Clear();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            shuffle();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Instantiate(dictShuffle[dictShuffle.Keys.ElementAt(i)]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Virar"))
        {
            aux = !aux;
            Flip();
        }

    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }

    private void shuffle() {
        
        int cont = 0;
        int j = Random.Range(0, objetos.Length); ;
        while (cont<20)
        {
            j = Random.Range(0, objetos.Length);

            if (!posicoes.Contains(j))
            {
                posicoes.Add(j);
                cont++;
            }
        }

        int i = 0;

        foreach (var lixos in posicoes)
        {
            dictShuffle.Add(i, objetos[lixos]);
            lista.Add(objetos[lixos]);
            i++;
        }
    }


    IEnumerator Spawn()
    {

        for (int i = 0; i < dictShuffle.Count; i++)
        {
            var pos = Random.Range(0,spawnPoints.Length);
            //print("AQUI PORRA");
            Instantiate(dictShuffle[dictShuffle.Keys.ElementAt(i)], spawnPoints[pos].position, spawnPoints[pos].rotation);
            yield return new WaitForSeconds(1.7f);
        }

        //print("acabou");

        lista.Clear();
        posicoes.Clear();
        dictShuffle.Clear();

        shuffle();
        StartCoroutine(Spawn());
    }

}