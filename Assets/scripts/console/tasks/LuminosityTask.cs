using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LuminosityTask : GameTask {

    private List<Light> _lights;

    public LuminosityTask() {
        name = "LUMINOSITY";
        consumption = 0;
        description = "What illuminates your path.";
        currentValue = "100";
        base.Enable();
        requireParameter = true;
        example = "LUMINOSITY X\n X should be an integer between 0 and 100.\n";
        _lights = new List<Light>(Light.GetLights(LightType.Directional, 0));
        _lights.AddRange(Light.GetLights(LightType.Point, 0));
        _lights.AddRange(Light.GetLights(LightType.Spot, 0));
    }

    public override string Disable() {
        return "Can't disable task " + name + ".";
    }

    public override string Enable() {
        return "Can't enable task " + name + ".";
    }

    public override string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue) || intValue < 0 || intValue > 100)
            return "LUMINOSITY parameter should be an integer between 0 and 100.\n";

        var mgr = GameManager.Instance;
        var currentIntValue = int.Parse(currentValue);
        var delta = (int)(100 - Mathf.Abs(currentIntValue - intValue)) / 10;
        if (intValue == currentIntValue)
            return "Task " + name + " value updated.";

        if (intValue < currentIntValue) {
            mgr.powerAvailable += delta;
            foreach(var l in _lights) {
                l.intensity = l.intensity * intValue / currentIntValue;
            }
            currentValue = value;

            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            GameManager.Instance.powerAvailable -= delta;
            foreach (var l in _lights) {
                l.intensity = l.intensity * intValue / currentIntValue;
            }
            currentValue = value;

            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

    public override int GetConsumption() {
        return (100 - int.Parse(base.currentValue)) / 10;
    }

}