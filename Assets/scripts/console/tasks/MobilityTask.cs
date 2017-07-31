using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MobilityTask : GameTask {

    public MobilityTask() {
        name = "MOBILITY";
        consumption = 50;
        cost = 50;
        description = "What makes you move.";
        currentValue = "100";
        requireParameter = true;
        base.Enable();
        example = "MOBILITY X. X should be an integer between 0 and 100";
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

        var currentIntValue = int.Parse(currentValue);
        var mgr = GameManager.Instance;
        var currentTotal = mgr.PowerAvailable;
        var newConsumption = Mathf.CeilToInt((float)intValue * (float)cost / 100f);
        if (currentTotal - consumption + newConsumption < mgr.maxPower) {
            currentValue = value;
            mgr.speedFactor = intValue;
            consumption = newConsumption;
            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

}
