using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanWalk : MonoBehaviour {

    public enum MoveDirection : int {
        Left = -1,
        Right = 1,
        None = 0
    }

    // Inspector fields

    public float unitsPerSecond = 1f;
    public float inputThreshold = 0.1f;

    // Components

    protected Rigidbody2D _rb2d;
    protected SpriteRenderer _spriteRenderer;

    // Private fields

    protected MoveDirection _direction;
    protected bool _facingRight = true;

    // Methods

	void Start () {

        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	void Update () {

        // Read inputs

        var xAxis = Input.GetAxis("Horizontal");
        if (xAxis > inputThreshold) {
            _direction = MoveDirection.Right;
            if(!_facingRight) {
                _facingRight = true;
                _spriteRenderer.flipX = false;
            }
        }
        else if (xAxis < -inputThreshold) {
            _direction = MoveDirection.Left;
            if (_facingRight) {
                _facingRight = false;
                _spriteRenderer.flipX = true;
            }
        }
        else
            _direction = MoveDirection.None;

	}

    void FixedUpdate() {

        var factor = (int)_direction * Time.fixedDeltaTime * unitsPerSecond;
        _rb2d.position += Vector2.right * factor;

    }

    
}
