using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDog : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject == GameManager.Instance.player && collision is CapsuleCollider2D) {
            GameManager.Instance.Victory();
        }
    }

}
