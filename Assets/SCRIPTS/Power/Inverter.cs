using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : MonoBehaviour, IPowerUP {

    [SerializeField]
    Transform inverterAnimado;

    Tubarao target;

    public void ExecutePowerUP(Tubarao target) {
        this.target = target;
        AtivarInverter();
    }

    //Funcao para inverter
    IEnumerator InverterCD(Transform inveterAnimadoClone) {
        yield return new WaitForSeconds(8.0f);
        target.Inverter = 1.0f;
        Destroy(inveterAnimadoClone.gameObject);
    }

    //Metodo chamado pelo tubarao inimigo para ativar o processo de inverter
    void AtivarInverter() {
        //Ativar o sprite do inverter animado
        target.Inverter = -1.0f;
        Transform inverterAnimadoClone = Instantiate(inverterAnimado, target.transform.position, 
            Quaternion.identity, target.transform);
        StartCoroutine(InverterCD(inverterAnimadoClone));
    }
}
