using Godot;
using System;

public partial class Jumping : State
{
    public override void Enter()
    {
        GD.Print("FSM Jumping");
    }

    public override void Update(float delta)
    {

    }
}
