using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class GameTask {

    public int consumption = 0;
    public int cost = 0;
    public string description;
    public bool enabled = true;
    public string currentValue = "0";
    public string name;
    public bool requireParameter = false;
    public string example;

    public virtual string Disable() {
        if (!enabled)
            return "Task " + name + " already killed.";

        enabled = false;
        consumption = cost;
        return "Task " + name + " killed.";
    }

    public virtual string Enable() {
        if (enabled)
            return "Task " + name + " already started.";

        var mgr = GameManager.Instance;
        if (mgr.PowerAvailable > consumption) {
            enabled = true;
            consumption = cost;
            return "Task " + name + " enabled.";
        }
        else
            return "Not enough power to enabled task " + name + ".";
    }

    public virtual string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue))
            return "Integer value expected";

        var currentIntValue = int.Parse(currentValue);
        var mgr = GameManager.Instance;
        var currentTotal = mgr.PowerAvailable;
        var newConsumption = Mathf.CeilToInt((float)intValue * (float)cost / 100f);
        if(currentTotal - consumption + newConsumption < mgr.maxPower) {
            currentValue = value;
            consumption = newConsumption;
            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public virtual int GetConsumption() {
        return consumption;
    }
}
