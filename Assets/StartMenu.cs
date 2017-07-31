using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    public float delay = .1f;
    protected bool toKill = false;
    protected float nextKill = 0f;
    public OSButton button;

    public void Update() {

        if(Time.realtimeSinceStartup > nextKill && toKill)  {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);

            button.InActive();
            toKill = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            toKill = true;
            nextKill = Time.realtimeSinceStartup + delay;
        }
    }

}

