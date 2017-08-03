using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    public OSButton button;

    private bool hover = false;

    public void OnMouseEnter() {
        hover = true;
    }

    public void OnMouseExit() {
        hover = false;
    }

    public void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if(!hover) {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);

                button.InActive();
            }
        }
    }

}

