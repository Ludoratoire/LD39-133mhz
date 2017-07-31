using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnergy : MonoBehaviour {

	private int _energy = 0;
	private Text _component;

	// Use this for initialization
	void Start () {
		this._component = gameObject.GetComponents<Text> ()[0];
	}

	// Update is called once per frame
	void Update () {

		int actualEnergy = GameManager.Instance.PowerAvailable;

		if (this._energy != actualEnergy) {

			_energy = actualEnergy;

			this._component.text = "CPU : " + _energy;
		}
	}
}
