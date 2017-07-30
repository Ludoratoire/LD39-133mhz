using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	private int _score = 0;
	private Text _component;

	// Use this for initialization
	void Start () {
		this._component = gameObject.GetComponents<Text> ()[0];
	}
	
	// Update is called once per frame
	void Update () {

		int actualScore = GameManager.Instance.score;

		if (this._score != actualScore) {

			_score = actualScore;

			this._component.text = "Score : " + _score;
		}
	}
}
