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

        var msg = base.Disable();
        if(!enabled)
            _jumpScript.enabled = false;
        return msg;
    }

    public override string Enable() {
        if (enabled)
            return "Task " + name + " already started.";

        var msg = base.Enable();
        if(enabled)
            _jumpScript.enabled = true;
        return msg;
    }

    public override string SetValue(string value) {
        return "JUMP filter expect no parameter.";
    }

}
