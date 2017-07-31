using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleScript : MonoBehaviour {

    public InputField consoleInput;
    public GameObject consoleOutput;
    public GameObject consoleLinePrefab;
    public ScrollRect consoleScroll;

    protected Dictionary<string, MethodInfo> _methods;
    protected Dictionary<string, MethodInfo> _filters;

    private string _lastCommand = "";
    private string _lastFilter = "";
    private string _lastFilterParam = "";

    private List<string> taskSetFilters = new List<string> { "GRAVITY", "LUMINOSITY", "MONSTERS", "MOBILITY", "RESOLUTION", "SOUND" }; 
    private List<string> taskKillFilters = new List<string> { "GRAVITY", "LUMINOSITY", "MONSTERS", "MOBILITY", "RESOLUTION", "SOUND", "PLATEFORM", "IA", "SCROLLING", "JUMP", "COLLISION", "SPRITE", "ANIMATION", "CLOTHE" }; 

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

        consoleInput.onEndEdit.AddListener((value) => ReceivedCommand(value));
	}

	public void ReceivedCommand(string input) {

        if(Input.GetKeyDown(KeyCode.Return)) {
            ParseInput(input);
            if(_methods.ContainsKey(_lastCommand)) {
                _methods[_lastCommand].Invoke(this, null);
            }
            else {
                PrintLog("Command not found\n");
            }
            consoleInput.text = "";
            ClearInput();
        }
        else {
            consoleInput.ActivateInputField();
        }

    }

    // Commands
    private void ParseInput(string input) {
        
        PrintLog("> " + input + "\n");
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

    public void PrintLog(string text) {

        var line = GameObject.Instantiate(consoleLinePrefab, consoleOutput.transform);
        line.GetComponent<Text>().text = text;
        consoleInput.transform.SetAsLastSibling();
        Canvas.ForceUpdateCanvases();
        consoleScroll.verticalNormalizedPosition = 0;
        Canvas.ForceUpdateCanvases();

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
            PrintLog(task.name + " | " + task.GetConsumption() + "Mhz | " + task.description + "\n");
        }
    }

    [ConsoleCommand("taskkill")]
    void TaskKill() {
        if (_lastFilter.Length <= 0) {
            PrintLog("Missing filter for command " + _lastCommand + "\n");
            return;
        }

        if(_lastFilterParam.Length > 0) {
            PrintLog("Wrong syntax !\n taskkill " + _lastFilter + " expects no parameter.\n");
            return;
        }

        if (!taskKillFilters.Contains(_lastFilter)) {
            PrintLog("Filter " + _lastFilter + " not available for command " + _lastCommand + "\n");
            return;
        }

        var mgr = GameManager.Instance;
        GameTask task = mgr.GetTask(_lastFilter);
        if (task == null)
            PrintLog("No task " + _lastFilter + "\n");
        else
            PrintLog(task.Disable() + "\n");
    }

    [ConsoleCommand("taskstart")]
    void TaskStart() {
        if (_lastFilter.Length <= 0) {
            PrintLog("Missing filter for command " + _lastCommand + "\n");
            return;
        }

        if (_lastFilterParam.Length > 0) {
            PrintLog("Wrong syntax !\n taskstart " + _lastFilter + " expects no parameter.\n");
            return;
        }

        if (!taskKillFilters.Contains(_lastFilter)) {
            PrintLog("Filter " + _lastFilter + " not available for command " + _lastCommand + "\n");
            return;
        }

        var mgr = GameManager.Instance;
        GameTask task = mgr.GetTask(_lastFilter);
        if (task == null)
            PrintLog("No task " + _lastFilter + "\n");
        else
            PrintLog(task.Enable() + "\n");
    }

    [ConsoleCommand("taskset")]
    void TaskSet() {
        if(_lastFilter.Length == 0) {
            PrintLog("Missing filter for command " + _lastCommand + "\n");
            return;
        }

        if(!taskSetFilters.Contains(_lastFilter)) {
            PrintLog("Filter " + _lastFilter + " not available for command " + _lastCommand + "\n");
            return;
        }

        var mgr = GameManager.Instance;
        GameTask task = mgr.GetTask(_lastFilter);

        if (_lastFilterParam.Length == 0 && task.requireParameter) {
            PrintLog("Missing parameter for filter " + _lastFilter + ".\n");
            PrintLog(_lastCommand + " " + task.example);
            return;
        }

        if(task == null)
            PrintLog("No task " + _lastFilter + "\n");
        else
            PrintLog(task.SetValue(_lastFilterParam) + "\n");
    }

    [ConsoleCommand("help")]
    void Help() {
        if(_lastFilter.Length > 0) {
            PrintLog("Wrong syntax !\n");
            PrintLog("Command help accept no filter\n");
            return;
        }

        PrintLog("tasklist : list main tasks.\n");
        PrintLog("taskset : update the value of a task.\n");
        PrintLog("taskkill : kill a task.\n");
        PrintLog("taskstart : kill a task.\n");
        PrintLog("help : display this text.\n");
    }

}
