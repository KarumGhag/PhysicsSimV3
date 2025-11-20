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

    Mouse mouse;

    Particle obstacle;
    public RopeSimulation(Game game) : base(game)
    {
        Global.currentSimulation = this;
        Global.cellSystem = new CellSystem();

        obstacle = new Particle(25, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2 + 50), Vector2.Zero, particles);
        obstacle.stationary = false;

        new Rope(35, 25, new Vector2(Global.WIDTH / 2, 20), particles, ropes);
      //  new Rope(35, 25, new Vector2(1200, 40), particles, ropes);

        mouse = new Mouse(Global.cellSystem);


    }

    public override void Update(float deltaTime)
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            obstacle.isGrabbed = true;
        } else
        {
            obstacle.isGrabbed = false;
        }

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

        mouse.Update(deltaTime);

        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);

    }
}