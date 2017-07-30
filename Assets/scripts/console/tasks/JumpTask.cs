using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class JumpTask : GameTask {

    protected CanJump _jumpScript;

    public JumpTask() {
        name = "JUMP";
        consumption = 20;
        description = "What gets you high.";
        currentValue = "";
        example = "JUMP\n";
        _jumpScript = GameManager.Instance.player.GetComponent<CanJump>();
    }

    public override string Disable() {
        _jumpScript.enabled = false;
        var msg = base.Disable();
        consumption = 0;
        return msg;
    }

    public override string Enable() {
        _jumpScript.enabled = true;
        var msg = base.Enable();
        consumption = 20;
        return msg;
    }

    public override string SetValue(string value) {
        return "JUMP filter expect no parameter.";
    }

}
