using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoder : MonoBehaviour {

    float spawnTime;
    public Transform[] spawnPoints;
    public GameObject[] poder;
    bool aux;

    [SerializeField]
    public float Speed;

    public float VelSpawn;

    // Use this for initialization
    void Start () {
        spawnTime = Random.Range(2f, 10f);
        InvokeRepeating("Spawn", spawnTime, VelSpawn);
        aux = true;
    }

    void FixedUpdate()
    {
        if (aux == true)
        {
            var vector = new Vector2(0, Time.deltaTime * Speed);
            this.gameObject.transform.Translate(vector);
        }
        else
        {
            var vector = new Vector2(0, Time.deltaTime * Speed) * -1;
            this.gameObject.transform.Translate(vector);
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


    void Spawn()
    {

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int obj = Random.Range(0, poder.Length);

        Instantiate(poder[obj], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
