using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanKillEnnemy : MonoBehaviour
{
	public float bounceForce = 5f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		// Contact avec un ennemi
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemies"))
		{
			Debug.Log("CanKillEnnemy.OnCollisionEnter2D");
			RaycastHit2D[] hitsBottom = Physics2D.RaycastAll(transform.position, Vector2.down);

			foreach (RaycastHit2D raycastHit in hitsBottom)
			{
				// Un ennemi est touché par le bas
				if (raycastHit.rigidbody != null && raycastHit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Ennemies"))
				{
					ZombileBehaviour zombileBehaviour = raycastHit.rigidbody.gameObject.GetComponent<ZombileBehaviour>();
					if (zombileBehaviour != null)
					{
						zombileBehaviour.Kill();
						// Faire rebondir le joueur
						GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);					
					}
				}
			}
		}
	}
}
