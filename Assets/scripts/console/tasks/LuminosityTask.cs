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
        base.Enable();
        base.SetValue("100");
        _lights = new List<Light>(Light.GetLights(LightType.Directional, 0));
        _lights.AddRange(Light.GetLights(LightType.Point, 0));
        _lights.AddRange(Light.GetLights(LightType.Spot, 0));
    }

    public new string SetValue(string value) {
        int intValue;
        if (!int.TryParse(value, out intValue))
            return "Integer value expected";

        var mgr = GameManager.Instance;
        var delta = (int)(100 - Mathf.Abs(currentValue - intValue)) / 10;
        if (intValue == currentValue)
            return "Task " + name + " value updated.";

        if (intValue < currentValue) {
            mgr.powerAvailable += delta;
            foreach(var l in _lights) {
                l.intensity = l.intensity * intValue / currentValue;
            }
            currentValue = intValue;

            return "Task " + name + " value updated.";
        }
        else if (mgr.powerAvailable > delta) {
            GameManager.Instance.powerAvailable -= delta;
            foreach (var l in _lights) {
                l.intensity = l.intensity * intValue / currentValue;
            }
            currentValue = intValue;

            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";
    }

}