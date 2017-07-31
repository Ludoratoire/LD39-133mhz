using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ScrollingTask : GameTask {

    public PlayerCam _playerCamScript;

    public ScrollingTask() {
        name = "SCROLLING";
        consumption = 20;
        cost = 20;
        description = "What lets you see.";
        currentValue = "";
        base.Enable();
        example = "SCROLLING";
        _playerCamScript = GameManager.Instance.gameCamera.GetComponent<PlayerCam>();
    }

    public override string Disable() {
        if (!enabled)
            return "Task " + name + " already killed.";

        _playerCamScript.enabled = false;
        var msg = base.Disable();
        consumption = 0;
        return msg;
    }

    public override string Enable() {
        if (enabled)
            return "Task " + name + " already started.";

        _playerCamScript.enabled = true;
        var msg = base.Enable();
        consumption = 20;
        return msg;
    }

    public override string SetValue(string value) {
        return "SCROLLING filter expect no parameter.";
    }

}
