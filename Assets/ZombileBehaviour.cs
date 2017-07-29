using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombileBehaviour : MonoBehaviour
{
	private Transform transform;
	private EnnemyWalk ennemyWalk;
	
	public enum EnnemyType
	{
		Zombile
	}

	public EnnemyType currentType;
	private GameObject player;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag("Player");
		ennemyWalk = GetComponent<EnnemyWalk>();
	}

	// Update is called once per frame
	void Update () {
		// Adapter la direction du Zombie en fonction de celle du joueur
		// Joueur sur la gauche du zombie ?
		if (player.transform.position.x < transform.position.x)
		{
			ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Left);
		}
		// Joueur sur la droite du zombie ?
		if (player.transform.position.x > transform.position.x)
		{
			ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Right);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if (collider2D.gameObject.layer == LayerMask.NameToLayer("EnnemyWalls"))
		{
			Debug.Log("ZombileBehaviour.OnTriggerEnter2D : " + collider2D.gameObject.name);
			ennemyWalk.SwitchDirection();
		}
	}
}
