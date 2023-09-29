using Godot;
using System;
//using System.Security.Cryptography.X509Certificates;

public partial class Attack : State
{
    [Export] private CharacterBody2D _characterBody2D;
    private AnimatedSprite2D _animatedSprite2D;
    private CharacterBody2D player;
    private NavigationAgent2D _navigationAgent2D;
    private Timer _timerNavigationAgent2D;

    private Vector2 direcao = new Vector2();
    private float speed = 1000f;
    private float distancia;
    private Boolean atacando = false;

    public override void Enter()
    {
        GD.Print("Modo de ataque");
        player = GetNode<CharacterBody2D>("../../../Player");
        _animatedSprite2D = GetNode<AnimatedSprite2D>("../../AnimatedSprite2D");
        _navigationAgent2D = GetNode<NavigationAgent2D>("../../NavigationAgent2D");
        _timerNavigationAgent2D = GetNode<Timer>("../../NavigationAgent2D/TimerNavigationAgent2D");
        direcao = Vector2.Zero;
        criarCaminho();
        if (atacando) {
            _timerNavigationAgent2D.Start();
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        if (player != null) {
            atacando = true;
            //direcao = ToLocal(_navigationAgent2D.GetNextPathPosition()).Normalized();
            direcao = (_navigationAgent2D.GetNextPathPosition() - _characterBody2D.GlobalPosition).Normalized();


            GD.Print("Player position: " + player.Position +
                "\n_navigationAgent2D.TargetPosition" + _navigationAgent2D.TargetPosition +
                "\ndirecao: " + direcao);

            if (direcao.X < 0)
            {
                _animatedSprite2D.FlipH = true;
            }
            if (direcao.X > 0)
            {
                _animatedSprite2D.FlipH = false;
            }

            _characterBody2D.Velocity = direcao * delta * speed;
           
            _characterBody2D.MoveAndSlide();
        }
    }

    public void OnAggroBodyExited(Node2D body) {
        fsm.TransitionTO("Idle");
    }

    public void criarCaminho()
    {
        _navigationAgent2D.TargetPosition = player.GlobalPosition;

    }
    public void OnTimerNavigationAgent2dTimeout() {
        GD.Print("Localizando o player");
        criarCaminho();
    }

}
