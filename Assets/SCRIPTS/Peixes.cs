using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peixes : MonoBehaviour
{

    public float moveSpeed = 1.5f;
    public bool aux;

    Rigidbody2D peixeRB;

    // Use this for initialization
    void Start()
    {
        peixeRB = GetComponent<Rigidbody2D>();
        aux = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (aux == true) {
            var vector = new Vector2(Time.deltaTime * moveSpeed, 0);
            this.gameObject.transform.Translate(vector);
        }
        else {
            var vector = new Vector2(Time.deltaTime * moveSpeed, 0) * -1;
            this.gameObject.transform.Translate(vector);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flip"))
        {
            aux = !aux;
            Flip();
        }

    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
 
}