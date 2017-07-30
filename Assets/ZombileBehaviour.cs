using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombileBehaviour : MonoBehaviour
{
	private Transform _transform;
	private EnnemyWalk _ennemyWalk;
	
	public enum EnnemyType
	{
		Zombile
	}

	public EnnemyType currentType;
	private GameObject _player;

	// Use this for initialization
	void Start ()
	{
		_transform = GetComponent<Transform>();
		_player = GameObject.FindGameObjectWithTag("Player");
		_ennemyWalk = GetComponent<EnnemyWalk>();
	}

	void FixedUpdate()
	{
		FollowPlayer();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	// Adapter la direction du Zombie en fonction de celle du joueur
	private void FollowPlayer()
	{
		// Joueur sur la gauche du zombie ?
		if (_player.transform.position.x < _transform.position.x)
		{
			_ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Left);
		}
		// Joueur sur la droite du zombie ?
		if (_player.transform.position.x > _transform.position.x)
		{
			_ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Right);
		}
	}

	void OnTriggerEnter2D(Collider2D collider2D) {
		if (collider2D.gameObject.layer == LayerMask.NameToLayer("EnnemyWalls"))
		{
			Debug.Log("ZombileBehaviour.OnTriggerEnter2D : " + collider2D.gameObject.name);
			_ennemyWalk.SwitchDirection();
		}
	}

	public void Kill()
	{
		DestroyObject(gameObject);
	}
}
