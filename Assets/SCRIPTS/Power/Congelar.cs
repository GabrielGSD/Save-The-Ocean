using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congelar : MonoBehaviour, IPowerUP {

    Tubarao target;
    [SerializeField]
    Transform congeladoSprite;

    Transform congeladoSpriteClone;

    public void ExecutePowerUP(Tubarao target) {
        this.target = target;
        AtivarCongelar();
    }

    //Metodo chamado pelo tubarao inimigo para ativar o congelamento
    void AtivarCongelar() {
        //Se ja estiver congelado,faz nada
        target.PlayerRunSpeed = 0.0f;
        congeladoSpriteClone = Instantiate(congeladoSprite, target.transform.position, 
                                        target.transform.rotation,target.transform);
        StartCoroutine(Descongelar());
    }

    //Funcao para descongelar
    IEnumerator Descongelar() {
        yield return new WaitForSeconds(6.0f);
        Destroy(congeladoSpriteClone.gameObject);
        target.PlayerRunSpeed = 10.0f;
        Destroy(gameObject);
    }
}
