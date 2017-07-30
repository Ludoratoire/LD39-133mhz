using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class GameTask {

    public int consumption = 0;
    public string description;
    public bool enabled = true;
    public string currentValue = "0";
    public string name;
    public bool requireParameter = false;
    public string example;

    public virtual string Disable() {
        enabled = false;
        GameManager.Instance.powerAvailable += consumption;
        return "Task " + name + " killed.";
    }

    public virtual string Enable() {
        var mgr = GameManager.Instance;
        if (mgr.powerAvailable > consumption) {
            enabled = true;
            mgr.powerAvailable -= consumption;
            return "Task " + name + " enabled.";
        }
        else
            return "Not enough power to enabled task " + name + ".";
    }

    public virtual string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue))
            return "Integer value expected";

        var mgr = GameManager.Instance;
        var currentIntValue = int.Parse(currentValue);
        var delta = 100 - Mathf.Abs(currentIntValue - intValue);
        if (intValue == currentIntValue)
            return "Task " + name + " value updated.";

        if (intValue < currentIntValue) {
            mgr.powerAvailable += delta;
            currentValue = value;
            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            currentValue = value;
            GameManager.Instance.powerAvailable -= delta;
            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public virtual int GetConsumption() {
        return consumption;
    }
}
