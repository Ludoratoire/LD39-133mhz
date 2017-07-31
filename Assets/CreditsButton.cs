using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour {

    public GameObject credits;

    public void DisplayCredits() {
        credits.SetActive(true);
    }

}
