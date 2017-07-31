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
        requireParameter = true;
        example = "GRAVITY X\nX should be an integer between 10 and 100.\n";
        currentValue = "100";
        base.Enable();
    }

    public override string Disable() {
        return "Can't disable task " + name + ".";
    }

    public override string Enable() {
        return "Can't enable task " + name + ".";
    }

    public override string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue)) {
            return "GRAVITY parameter should be an integer between 0 and 100.";
        }

        if (intValue < 0 || intValue > 100)
            return "GRAVITY parameter should be between 0 and 100";

        var mgr = GameManager.Instance;
        var currentIntValue = int.Parse(currentValue);
        var delta = Mathf.Abs(currentIntValue - intValue);
        if (intValue == currentIntValue)
            return "Task " + name + " value updated.";

        if (intValue > currentIntValue) {
            mgr.powerAvailable += delta;
            currentValue = value;
            Physics2D.gravity = new Vector2(0, (_defaultGravity.y * intValue) / 100);

            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            currentValue = value;
            GameManager.Instance.powerAvailable -= delta;
            Physics2D.gravity = new Vector2(0, (_defaultGravity.y * intValue) / 100);

            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public override int GetConsumption() {
        return 100 - int.Parse(base.currentValue);
    }

}
