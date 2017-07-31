using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LuminosityTask : GameTask {

    private List<Light> _lights;

    public LuminosityTask() {
        name = "LUMINOSITY";
        consumption = 10;
        cost = 10;
        description = "What illuminates your path.";
        currentValue = "100";
        base.Enable();
        requireParameter = true;
        example = "LUMINOSITY X. X should be an integer between 0 and 100.";
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
            return "LUMINOSITY parameter should be an integer between 0 and 100.";

        var currentIntValue = int.Parse(currentValue);
        var mgr = GameManager.Instance;
        var currentTotal = mgr.PowerAvailable;
        var newConsumption = Mathf.CeilToInt((float)intValue / 10f);
        if (currentTotal + consumption - newConsumption >= 0) {
            currentValue = value;
            foreach (var l in _lights) {
                l.intensity = l.intensity * intValue / currentIntValue;
            }
            consumption = newConsumption;
            return "Task " + name + " value updated.";
        }
        else
            return "Not enough power to update task " + name + " value.";

    }

    public override int GetConsumption() {
        return (100 - int.Parse(base.currentValue)) / 10;
    }

}