using AlpacaSound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ResolutionTask : GameTask {

    public ResolutionTask() {
        name = "RESOLUTION";
        consumption = 30;
        description = "What pleases your eyes (or not).";
        requireParameter = true;
        example = "RESOLUTION X\nX should be an integer between 1 and 100.\n";
        base.Enable();
        currentValue = "100";
    }

    public override string Disable() {
        return "Can't disable task " + name + ".";
    }

    public override string Enable() {
        return "Can't disable task " + name + ".";
    }

    public override string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue)) {
            return "GRAVITY parameter should be an integer between 0 and 100.";
        }

        var mgr = GameManager.Instance;
        var currentIntValue = int.Parse(currentValue);
        var delta = Mathf.Abs(currentIntValue - intValue) * 30 / 100;
        if (intValue == currentIntValue)
            return "Task " + name + " value updated.";

        if (intValue < currentIntValue) {
            mgr.powerAvailable += delta;
            currentValue = value;
            mgr.SetRetroFactor(intValue);
            consumption -= delta;

            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            currentValue = value;
            GameManager.Instance.powerAvailable -= delta;
            consumption += delta;
            mgr.SetRetroFactor(intValue);

            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";

    }

    public override int GetConsumption() {
        return consumption;
    }

}
