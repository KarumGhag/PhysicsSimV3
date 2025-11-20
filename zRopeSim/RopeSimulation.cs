namespace RopeSim;

using SimulationClass;
using GameClass;
using GlobalInfo;
using CollisionSystem;
using ParticleClass;
using RopeClass;
using System.Numerics;
using Raylib_cs;
using System.Runtime.Intrinsics.X86;
using MouseSys;

public class RopeSimulation : Simulation
{

    public static List<Particle> particles = new List<Particle>();
    public static List<Rope> ropes = new List<Rope>();
    public static List<Particle> obstacles = new List<Particle>();

    Mouse mouse;
    public RopeSimulation(Game game) : base(game)
    {
        Global.currentSimulation = this;
        Global.cellSystem = new CellSystem();

        obstacles.Add(new Particle(25, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2 + 50), Vector2.Zero, particles));
        obstacles[0].mass = 20;

        new Rope(50, 10, new Vector2(Global.WIDTH / 2, 20), particles, ropes);

        mouse = new Mouse(Global.cellSystem);

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

        if (Raylib.IsKeyReleased(KeyboardKey.A))
        {
            obstacles.Add(new Particle(25, mouse.mousePosition, Vector2.Zero, particles));
            obstacles[obstacles.Count - 1].mass = 20;
        }

        mouse.Update(deltaTime);

        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);

    }
}