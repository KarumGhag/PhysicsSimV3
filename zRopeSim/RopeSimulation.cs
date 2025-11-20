namespace RopeSimultaion;

using SimulationClass;
using GameClass;
using GlobalInfo;
using CollisionSystem;
using ParticleClass;
using RopeClass;
using System.Numerics;
using Raylib_cs;

public class RopeSim : Simulation
{

    public  List<Particle> particles = new List<Particle>();
    public  List<Rope> ropes = new List<Rope>();
    public  List<Particle> obstacles = new List<Particle>();

    public RopeSim(Game game) : base(game)
    {
    }

    public override void Init()
    {
        obstacles.Add(new Particle(25, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2 + 50), Vector2.Zero, particles));
        obstacles[0].mass = 20;

        new Rope(50, 10, new Vector2(Global.WIDTH / 2, 20), particles, ropes);

    }

    public override void Reset()
    {
        particles = new List<Particle>();
        ropes = new List<Rope>();
        obstacles = new List<Particle>();

        Global.cellSystem.ClearCells();

        Init();
    }

    public override void Update(float deltaTime)
    {
        Global.cellSystem.ClearCells();

        foreach (Particle particle in particles)
        {
            particle.Update(deltaTime);
            Raylib.DrawCircleV(particle.position, particle.radius, particle.colour);
        }


        foreach (Rope rope in ropes)
        {
            rope.ConstrainRope();
            rope.DrawLines(); 
        }

        if (Raylib.IsKeyReleased(KeyboardKey.A) && Global.mouse != null)
        {
            obstacles.Add(new Particle(25, Global.mouse.mousePosition, Vector2.Zero, particles));
            obstacles[obstacles.Count - 1].mass = 20;
        }

        Global.mouse?.Update(Raylib.GetFrameTime());


        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);

    }
}