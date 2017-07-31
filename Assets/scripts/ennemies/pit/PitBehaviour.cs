using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehaviour : MonoBehaviour {

    private AudioSource _source;

    private void Start() {
        _source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag == "Player" && collision.GetType() == typeof(CapsuleCollider2D)) {
            foreach (var c in collision.gameObject.GetComponents<Collider2D>()) {
                c.enabled = false;
            }
            _source.Play();
        }

        if (collision.gameObject.tag == "zombile") {
            foreach (var c in collision.gameObject.GetComponents<Collider2D>()) {
                c.enabled = false;
            }
            _source.Play();
        }

    }
}
