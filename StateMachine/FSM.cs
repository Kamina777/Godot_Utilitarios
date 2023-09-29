using Godot;
//using Godot.NativeInterop;
using System;
using System.Collections.Generic;

public partial class FSM : Node2D
{
    //Get list of states
    //store the current state
    //execute this state's logic
    //handle the transitions

    [Export] public NodePath initialState;

	private Dictionary<string, State> _states = new Dictionary<string, State>();

    private State _currentState;

	public override void _Ready()
	{

        foreach (Node2D node2D in GetChildren())
		{
            if (node2D is State s) 
			{
                _states[node2D.Name] = s;
				s.fsm = this;
				s.Ready();
				s.Exit();
            }
		}
		_currentState = GetNode<State>(initialState);
		_currentState.Enter();
	}

	public override void _Process(double delta)
	{
		_currentState.Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate((float)delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		_currentState._UnhandledInput(@event);
	}

	public void TransitionTO(string key) 
	{
		if (!_states.ContainsKey(key) || _currentState == _states[key])
			return;

		_currentState.Exit();
		_currentState = _states[key];
		_currentState.Enter();
	}
}
