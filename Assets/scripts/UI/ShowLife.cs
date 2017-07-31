using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLife : MonoBehaviour {

	public GameObject[] lifes;

	void Update () {

        var nbLifes = GameManager.Instance.life;
        for (var i = 0; i < 3; i++) {
            if(i < nbLifes)
                lifes[i].SetActive(true);
            else
                lifes[i].SetActive(false);
        }
    }	
}
