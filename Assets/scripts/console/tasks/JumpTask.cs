using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class JumpTask : GameTask {

    protected CanJump _jumpScript;

    public JumpTask() {
        name = "JUMP";
        consumption = 0;
        cost = 20;
        description = "What gets you high.";
        currentValue = "";
        example = "JUMP";
        _jumpScript = GameManager.Instance.player.GetComponent<CanJump>();
        _jumpScript.enabled = false;
        enabled = false;
    }

    public override string Disable() {
        if (!enabled)
            return "Task " + name + " already killed.";

        _jumpScript.enabled = false;
        var msg = base.Disable();
        return msg;
    }

    public override string Enable() {
        if (enabled)
            return "Task " + name + " already started.";

        _jumpScript.enabled = true;
        var msg = base.Enable();
        return msg;
    }

    public override string SetValue(string value) {
        return "JUMP filter expect no parameter.";
    }

}
