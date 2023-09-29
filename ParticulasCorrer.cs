using Godot;
using System;

public partial class ParticulasCorrer : AnimatedSprite2D
{
    public override void _Ready()
    {
		spawnarParticulas();
    }

    public void spawnarParticulas() {
		Play();
    }
	
	public void OnAnimationFinished() {
		QueueFree();
	}

}
