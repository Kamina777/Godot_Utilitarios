using Godot;
using System;

public partial class State : Node2D
{
	public FSM fsm;

	public virtual void Enter() { }
	public virtual void Exit() { }

	public virtual void Ready() { } //_Ready
    public virtual void Update(float delta) { } //_process
    public virtual void PhysicsUpdate(float delta) { } //_PhysicsProcess
    public virtual void HandleInput(InputEvent @event) { }

}
