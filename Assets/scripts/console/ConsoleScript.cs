using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScript : MonoBehaviour {

    public InputField consoleInput;
    public Text consoleOutput;

    protected Dictionary<string, MethodInfo> _methods;
    protected Dictionary<string, MethodInfo> _filters;

    private string _lastCommand;
    private string _lastFilter;
    private string _lastFilterParam;

    private List<string> taskSetFilters = new List<string> { "GRAVITY", "LUMINOSITY", "MONSTERS", "MOBILITY", "RESOLUTION", "SOUND" }; 

	void Start () {

        _methods = new Dictionary<string, MethodInfo>();
        _filters = new Dictionary<string, MethodInfo>();
        var methodInfos = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach(var info in methodInfos) {
            if(Attribute.IsDefined(info, typeof(ConsoleCommandAttribute))) {
                var attr = (ConsoleCommandAttribute)info.GetCustomAttributes(typeof(ConsoleCommandAttribute), false)[0];
                _methods.Add(attr.Binding, info);
            }
            else if(Attribute.IsDefined(info, typeof(ConsoleFilterAttribute))) {
                var attr = (ConsoleFilterAttribute)info.GetCustomAttributes(typeof(ConsoleFilterAttribute), false)[0];
                _filters.Add(attr.Binding, info);
            }
        }


	}
	
	public void ReceivedCommand(string input) {

        ParseInput(input);
        if(_methods.ContainsKey(_lastCommand)) {
            _methods[_lastCommand].Invoke(this, null);
        }
        else {
            consoleOutput.text += "Command not found\n";
        }
        consoleInput.text = "";
        ClearInput();

    }

    // Commands
    private void ParseInput(string input) {
        consoleOutput.text += "> " + input + "\n";
        var splits = input.Split(' ');
        _lastCommand = splits[0];

        if (splits.Length > 1)
            _lastFilter = splits[1];

        if (splits.Length > 2)
            _lastFilterParam = splits[2];
    }

    private void ClearInput() {
        _lastCommand = "";
        _lastFilter = "";
        _lastFilterParam = "";
    }

    private bool HasValidFilter() {
        return _filters.ContainsKey(_lastFilter);
    }

    public class ConsoleCommandAttribute : Attribute {

        public string Binding { get; private set; }

        public ConsoleCommandAttribute(string binding) {
            Binding = binding;
        }

    }

    public class ConsoleFilterAttribute : Attribute {

        public string Binding { get; private set; }

        public ConsoleFilterAttribute(string binding) {
            Binding = binding;
        }

    }

    [ConsoleCommand("tasklist")]
    void TaskList() {
        var mgr = GameManager.Instance;
        foreach(var task in mgr.taskList) {
            consoleOutput.text += task.name + " | " + task.GetConsumption() + "Mhz | " + task.description + "\n";
        }
    }

    [ConsoleCommand("taskkill")]
    void TaskKill() {

    }

    [ConsoleCommand("taskset")]
    void TaskSet() {
        if(_lastFilter.Length == 0) {
            consoleOutput.text += "Missing filter for command " + _lastCommand + "\n";
            return;
        }

        if(!HasValidFilter()) {
            consoleOutput.text += "Filter " + _lastFilter + " not found.\n";
            return;
        }

        if(!taskSetFilters.Contains(_lastFilter)) {
            consoleOutput.text += "Filter " + _lastFilter + " not available for command " + _lastCommand + "\n";
            return;
        }

        _filters[_lastFilter].Invoke(this, null);
        var mgr = GameManager.Instance;
        GameTask task = mgr.GetTask(_lastFilter);
        if(task == null)
            consoleOutput.text += "No task " + _lastFilter + "\n";
        else
            consoleOutput.text += task.SetValue(_lastFilterParam) + "\n";

        if (_lastFilterParam.Length == 0 && task.requireParameter) {
            consoleOutput.text += "Missing parameter for filter " + _lastFilter + ".\n";
            consoleOutput.text += _lastCommand + " " + task.example;
            return;
        }
    }

    [ConsoleCommand("help")]
    void Help() {
        if(_lastFilter.Length > 0) {
            consoleOutput.text += "Wrong syntax !\n";
            consoleOutput.text += "Command help accept not filters\n";
            return;
        }
    }

}
