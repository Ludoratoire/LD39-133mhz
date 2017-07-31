using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBar : MonoBehaviour {

    public OSButton[] buttons;

    public void Active(OSButtonID ID) {
        foreach(var b in buttons) {
            if (b.ID == ID)
                continue;

            b.InActive();
        }
    }
}
