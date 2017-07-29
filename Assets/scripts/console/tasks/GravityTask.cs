using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTask : GameTask {

    private Vector2 _defaultGravity;

    public GravityTask() {
        name = "GRAVITY";
        consumption = 0;
        description = "What keeps your feet on the ground.";
        _defaultGravity = Physics2D.gravity;
        base.Enable();
        base.SetValue("100");
    }

    public new string Disable() {
        return "Can't disable task " + name + ".";
    }

    public new string Enable() {
        return "Can't disable task " + name + ".";
    }

    public new string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue)) {
            return "GRAVITY parameter should be an integer between 0 and 100.";
        }

        if (intValue < 0 || intValue > 100)
            return "GRAVITY parameter should be between 0 and 100";

        var mgr = GameManager.Instance;
        var delta = Mathf.Abs(currentValue - intValue);
        if (intValue == currentValue)
            return "Task " + name + " value updated.";

        if (intValue < currentValue) {
            mgr.powerAvailable += delta;
            currentValue = intValue;
            Physics2D.gravity = new Vector2(0, (_defaultGravity.y * currentValue) / 100);

            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            currentValue = intValue;
            GameManager.Instance.powerAvailable -= delta;
            Physics2D.gravity = new Vector2(0, (_defaultGravity.y * currentValue) / 100);

            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public new int GetConsumption() {
        return base.currentValue;
    }

}
