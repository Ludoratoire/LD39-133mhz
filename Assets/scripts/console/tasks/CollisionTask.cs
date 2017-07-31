using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CollisionTask : GameTask {

    public CollisionTask() {
        name = "COLLISION";
        consumption = 10;
        cost = 10;
        description = "What stops you.";
        currentValue = "";
        base.Enable();
        example = "COLLISION";
    }

    public override string Disable() {
        if (!enabled)
            return "Task " + name + " already killed.";

        var msg = base.Disable();
        var collides = GameObject.FindObjectsOfType<Collider2D>();
        foreach(var c in collides) {
            c.isTrigger = true;
        }
        return msg;
    }

    public override string Enable() {
        if (enabled)
            return "Task " + name + " already started.";

        var msg = base.Enable();
        var collides = GameObject.FindObjectsOfType<Collider2D>();
        foreach (var c in collides) {
            c.isTrigger = false;
        }
        return msg;
    }

    public override string SetValue(string value) {
        return "COLLISION filter expect no parameter.";
    }

}
