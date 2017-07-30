using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanJump : MonoBehaviour {

    // Inspector fields

    public float inputThreshold = 0.1f;
    public float jumpForce = 10f;
    public LayerMask resetJump;
    public Collider2D groundCollider;

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
            if (_rb2d.velocity.y > jumpForce)
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpForce);
            _requestJump = false;
            _touchingGround = false;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

        var isTouchingGround = false;
        foreach(var c in collision.contacts) {
            if(c.otherCollider == groundCollider) {
                isTouchingGround = true;
                break;
            }
        }

        if (isTouchingGround && resetJump == (resetJump| (1 << collision.gameObject.layer)))
            _touchingGround = true;

    }
}
