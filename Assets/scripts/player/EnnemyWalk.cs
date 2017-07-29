using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnnemyWalk : MonoBehaviour {

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
	
	// Use this for initialization
	void Start () {
		_rb2d = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_direction = Random.Range(0, 2) == 1 ? MoveDirection.Right : MoveDirection.Left;
		_spriteRenderer.flipX = _direction == MoveDirection.Right;
	}
	
	// Update is called once per frame
	void Update () {
		var factor = (int)_direction * Time.fixedDeltaTime * unitsPerSecond;
		_rb2d.position += Vector2.right * factor;
	}
	
	public void SwitchDirection()
	{
		_direction = _direction == MoveDirection.Right ? MoveDirection.Left : MoveDirection.Right;
		_spriteRenderer.flipX = !_spriteRenderer.flipX;
	}
}
