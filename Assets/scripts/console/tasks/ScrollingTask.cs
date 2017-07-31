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

        var msg = base.Disable();
        if(!enabled)
            _playerCamScript.enabled = false;
        return msg;
    }

    public override string Enable() {
        if (enabled)
            return "Task " + name + " already started.";

        var msg = base.Enable();
        if(enabled)
            _playerCamScript.enabled = true;
        return msg;
    }

    public override string SetValue(string value) {
        return "SCROLLING filter expect no parameter.";
    }

}
