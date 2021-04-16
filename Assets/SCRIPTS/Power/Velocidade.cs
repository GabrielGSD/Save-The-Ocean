using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocidade : MonoBehaviour, IPowerUP {

    Tubarao target;

    public void ExecutePowerUP(Tubarao target) {
        this.target = target;
        AtivarVelocidade();
    }

    void AtivarVelocidade() {
        StartCoroutine(AumentarVelocidade());
        target.PlayerRunSpeed = 15.0f;
    }

    //Metodo para aumentar a velocidade do tubarao
    IEnumerator AumentarVelocidade() {
        yield return new WaitForSeconds(8.0f);
        target.PlayerRunSpeed = 10.0f;
    }

   
}
