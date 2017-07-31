using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player" && collision.GetType() == typeof(CapsuleCollider2D)) {
            collision.gameObject.GetComponent<PlayerBehavior>().ReceiveDamage();
        }

    }
}
