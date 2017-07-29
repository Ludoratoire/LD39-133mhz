using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class GameTask {

    public int consumption = 0;
    public string description;
    public bool enabled = true;
    public int currentValue = 0;
    public string name;

    public string Disable() {
        enabled = false;
        GameManager.Instance.powerAvailable += consumption;
        return "Task " + name + " killed.";
    }

    public string Enable() {
        var mgr = GameManager.Instance;
        if (mgr.powerAvailable > consumption) {
            enabled = true;
            mgr.powerAvailable -= consumption;
            return "Task " + name + " enabled.";
        }
        else
            return "Not enough power to enabled task " + name + ".";
    }

    public string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue))
            return "Integer value expected";

        var mgr = GameManager.Instance;
        var delta = 100 - Mathf.Abs(currentValue - intValue);
        if (intValue == currentValue)
            return "Task " + name + " value updated.";

        if (intValue < currentValue) {
            mgr.powerAvailable += delta;
            currentValue = intValue;
            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            currentValue = intValue;
            GameManager.Instance.powerAvailable -= delta;
            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public int GetConsumption() {
        return consumption;
    }
}
