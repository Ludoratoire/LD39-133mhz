using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MobilityTask : GameTask {

    public MobilityTask() {
        name = "MOBILITY";
        consumption = 50;
        description = "What makes you move.";
        currentValue = "100";
        requireParameter = true;
        base.Enable();
        example = "MOBILITY X\nX should be an integer between 0 and 100\n";
    }

    public override string Disable() {
        return "Can't disable task " + name + ".";
    }

    public override string Enable() {
        return "Can't enable task " + name + ".";
    }

    public override string SetValue(string value) {

        int intValue;
        if (!int.TryParse(value, out intValue) || intValue < 0 || intValue > 100) {
            return "MOBILITY parameter should be an integer between 0 and 100.";
        }

        var mgr = GameManager.Instance;
        var currentIntValue = int.Parse(currentValue);
        var delta = Mathf.Abs(currentIntValue - intValue) / 2;
        if (intValue == currentIntValue)
            return "Task " + name + " value updated.";

        if (intValue < currentIntValue) {
            mgr.powerAvailable += delta;
            currentValue = value;
            mgr.speedFactor = intValue;
            consumption -= delta;

            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            currentValue = value;
            mgr.speedFactor = intValue;
            GameManager.Instance.powerAvailable -= delta;
            consumption += delta;

            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";

    }

}
