using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolhas : MonoBehaviour {

    public static bool flag;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 bolhaScale = new Vector3(1,1, 1);
        bolhaScale.x = transform.parent.localScale.x;

        transform.localScale = bolhaScale;

        /*if (flag)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }else
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            */
    }

}
