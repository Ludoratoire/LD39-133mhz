using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLife : MonoBehaviour {

	private int _nbrHP = 3;

	public GameObject prefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		Image[] images = gameObject.GetComponentsInChildren<Image> ();
		int life = GameManager.Instance.life;

		if (this._nbrHP != life) {
			
			if (this._nbrHP > life && this._nbrHP > 1) {

				GameObject.Destroy (images [_nbrHP].gameObject);
				this._nbrHP = life;

			} else if (this._nbrHP < 3) {
				
				GameObject.Instantiate (prefab, this.transform);
				this._nbrHP = life;
			}

		}
	}	
}
