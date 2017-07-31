using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour {

    public AudioClip coinSound;
    protected AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = coinSound;
        _audioSource.volume = 1.0f;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player" && collision is CapsuleCollider2D) {
            _audioSource.Play();
            GameManager.Instance.score += 5;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }

}
