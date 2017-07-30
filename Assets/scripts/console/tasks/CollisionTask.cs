using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CollisionTask : GameTask {

    public CollisionTask() {
        name = "COLLISION";
        consumption = 10;
        description = "What stops you.";
        currentValue = "";
        base.Enable();
        example = "COLLISION\n";
    }

    public override string Disable() {
        var msg = base.Disable();
        consumption = 0;
        var collides = GameObject.FindObjectsOfType<Collider2D>();
        foreach(var c in collides) {
            c.isTrigger = true;
        }
        return msg;
    }

    public override string Enable() {
        var msg = base.Enable();
        var collides = GameObject.FindObjectsOfType<Collider2D>();
        foreach (var c in collides) {
            c.isTrigger = false;
        }
        consumption = 10;
        return msg;
    }

    public override string SetValue(string value) {
        return "COLLISION filter expect no parameter.";
    }

}
