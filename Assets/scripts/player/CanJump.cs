using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanJump : MonoBehaviour {

    // Inspector fields

    public float inputThreshold = 0.1f;
    public float jumpForce = 10f;

    // Components

    protected Rigidbody2D _rb2d;

    // Private fields

    protected bool _requestJump = false;
    protected bool _touchingGround = false;

	void Start () {

        _rb2d = GetComponent<Rigidbody2D>();

	}
	
	void Update () {

        var yAxis = Input.GetAxis("Vertical");
        if (_touchingGround && yAxis > inputThreshold)
            _requestJump = true;

	}

    void FixedUpdate() {

        if(_requestJump) {
            _rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _requestJump = false;
            _touchingGround = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {

        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            _touchingGround = true;
        }

    }
}
