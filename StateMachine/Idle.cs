using Godot;
using System;
using System.Reflection;

public partial class Idle : State
{
    [Export] private CharacterBody2D _characterBody2D;
    [Export] private Timer _timer;
    [Export] private Area2D _area2D;
    [Export] private AnimatedSprite2D _animatedSprite2D;
    [Export] private Timer timerGrandao;
    [Export] private CollisionShape2D collisionArea2D;
    private CharacterBody2D player;

    private float speed = 1000f;
    private Vector2 direcao = Vector2.Zero;
    private Random rnd = new Random();
    private int numX;
    private int numY;
    private Boolean grandao = false;

    public override void Enter()
    {
        GD.Print("Modo Idle");
    }

    public override void Ready()
    {
        player = GetNode<CharacterBody2D>("../../../Player");
    }

    public override void PhysicsUpdate(float delta)
    {
        if (direcao.X < 0)
        {
            _animatedSprite2D.FlipH = true;
        }
        if(direcao.X > 0)
        {
            _animatedSprite2D.FlipH = false;
        }
        _characterBody2D.Velocity = direcao * speed * delta;
        _characterBody2D.MoveAndSlide();
    }

    public override void Exit()
    {

    }

    public void OnTimerTimeout() {
        numX = rnd.Next(-1, 2);
        numY = rnd.Next(-1, 2);
        if (direcao == Vector2.Zero)
        {
            direcao = new Vector2(numX, numY).Normalized();
        }
        else 
        {
            direcao = Vector2.Zero;
        }        
    }

    public void OnArea2dBodyEntered(Node2D body) {
        if (!grandao) {
            timerGrandao.Start();
        }
        fsm.TransitionTO("Attack");
    }


    public void OnTimerGrandaoTimeout()
    {

        if (_characterBody2D.Scale < new Vector2(4, 4))
        {
            _characterBody2D.Scale += new Vector2(0.1f, 0.1f);
        }
        else
        {
            timerGrandao.QueueFree();
            grandao = true;
        }
    }
}
