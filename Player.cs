using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private AnimationPlayer animationPlayer;
    private Sprite2D sprite2D;
    private AnimationTree animationTree;
    private PackedScene particulas;
    private Timer timerPodeAtacar;

	private float Speed = 6000.0f;
    private Boolean podeAtacar = true;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        animationTree = GetNode<AnimationTree>("AnimationTree");
        timerPodeAtacar = GetNode<Timer>("TimerPodeAtacar");

        animationTree.Active = true;
        particulas = (PackedScene) GD.Load("res://scenes/Particulas/ParticulasCorrer.tscn");
    }

    public override void _PhysicsProcess(double delta)
	{
        Vector2 direcao = Input.GetVector("Left", "Right", "Up", "Down").Normalized();

        movimentacao(direcao, (float) delta);
        animacao(direcao);
	}

	public void movimentacao(Vector2 direcao,float delta) {
        Vector2 velocity = Velocity;

        if (direcao != Vector2.Zero)
        {
            velocity = direcao * Speed * delta;
        }
        else 
        {
            velocity = Vector2.Zero;
        }
        Velocity = velocity;
        MoveAndSlide();
    }

    public void animacao(Vector2 direcao) {


        if (direcao != Vector2.Zero)
        {
            animationTree.Set("parameters/Idle/blend_position", direcao);
            animationTree.Set("parameters/Run/blend_position", direcao);
            animationTree.Set("parameters/Attack/blend_position", direcao);

            animationTree.Set("parameters/conditions/idle", false);
            animationTree.Set("parameters/conditions/is_moving", true);
            animationTree.Set("parameters/conditions/swing", false);

        }
        else 
        {
            animationTree.Set("parameters/conditions/idle", true);
            animationTree.Set("parameters/conditions/is_moving", false);
            animationTree.Set("parameters/conditions/swing", false);


        }
        ataque();
    }

    public void ataque() 
    {
        if (Input.IsActionJustPressed("Ataque") && podeAtacar)
        {
            animationTree.Set("parameters/conditions/swing", true);
            animationTree.Set("parameters/conditions/idle", false);
            animationTree.Set("parameters/conditions/is_moving", false);
            podeAtacar = false;
            timerPodeAtacar.Start();
        }
        else 
        {
            animationTree.Set("parameters/conditions/swing", false);
        }    
    }

    public void intanciarParticulas() {
        Node2D particula = particulas.Instantiate<Node2D>();
        particula.GlobalPosition = GlobalPosition + new Vector2(0, -3);
        particula.Call("spawnarParticulas");
        GetTree().Root.AddChild(particula);

    }
    public void TimerPodeAtacarTimeout()
    {
        podeAtacar = true;
    }

}
