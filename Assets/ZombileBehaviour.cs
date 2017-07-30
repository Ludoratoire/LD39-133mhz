using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombileBehaviour : MonoBehaviour
{
	public enum EnnemyType
	{
		Zombile
	}

	public EnnemyType currentType;
	
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if (collider2D.gameObject.layer == LayerMask.NameToLayer("EnnemyWalls"))
		{
			GetComponent<EnnemyWalk>().SwitchDirection();
		}
	}
}
