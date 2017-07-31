﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehaviour : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag == "Player" && collision.GetType() == typeof(CapsuleCollider2D)) {
            var mgr = GameManager.Instance;
            mgr.life--;
            if (mgr.life <= 0) {
                mgr.score = 0;
                mgr.ResetPlayerPos();
            }
            foreach (var c in collision.gameObject.GetComponents<Collider2D>()) {
                c.enabled = false;
            }
        }

    }
}
