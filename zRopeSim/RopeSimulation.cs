namespace RopeSim;

using SimulationClass;
using GameClass;
using GlobalInfo;
using CollisionSystem;
using ParticleClass;
using RopeClass;
using System.Numerics;
using Raylib_cs;
public class RopeSimulation : Simulation
{

    public static List<Particle> particles = new List<Particle>();
    public static List<Rope> ropes = new List<Rope>();

    public RopeSimulation(Game game) : base(game)
    {
        Global.currentSimulation = this;
        Global.cellSystem = new CellSystem();

        Particle obstacle = new Particle(25, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2 + 50), Vector2.Zero, particles);
        obstacle.stationary = false;

        new Rope(35, 25, new Vector2(600, 40), particles, ropes);
        new Rope(35, 25, new Vector2(1200, 40), particles, ropes);


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
        }

        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);

    }
}