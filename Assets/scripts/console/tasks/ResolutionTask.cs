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
        cost = 30;
        description = "What pleases your eyes (or not).";
        requireParameter = true;
        example = "RESOLUTION X. X should be an integer between 1 and 100.";
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
        if (!int.TryParse(value, out intValue) || intValue < 0 || intValue > 100) {
            return "RESOLUTION parameter should be an integer between 0 and 100.";
        }

        var currentIntValue = int.Parse(currentValue);
        var mgr = GameManager.Instance;
        var currentTotal = mgr.PowerAvailable;
        var newConsumption = Mathf.CeilToInt((float)intValue * (float)cost / 100f);
        if (currentTotal - consumption + newConsumption > 0) {
            currentValue = value;
            mgr.SetRetroFactor(intValue);
            consumption = newConsumption;
            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public override int GetConsumption() {
        return consumption;
    }

}
