using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player") {
            GameManager.Instance.score += 5;
            GameObject.Destroy(gameObject);
        }

    }

}
