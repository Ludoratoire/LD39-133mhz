using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShutdownButton : MonoBehaviour {

    public void ExitGame() {
        Application.Quit();
        Debug.Log("Exit");
    }

}
