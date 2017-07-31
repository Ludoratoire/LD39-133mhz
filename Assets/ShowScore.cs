using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    private Text _component;

	void Start () {
		this._component = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _component.text = "Score : " + GameManager.Instance.score;
	}
}
