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

        int numParticles;

        new Particle(10, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2), new Vector2(0, 0), particles);
        new Particle(10, new Vector2(Global.WIDTH / 2 + 100, Global.HEIGHT / 2), new Vector2(0, 0), particles);
        new Particle(10, new Vector2(Global.WIDTH / 2 + 150, Global.HEIGHT / 2 + 100), new Vector2(0, 0), particles);
        new Particle(10, new Vector2(Global.WIDTH / 2 + 200, Global.HEIGHT / 2 + 200), new Vector2(0, 0), particles);


        particles[0].colour = Color.Gray;

        particles[0].stationary = true;
        ropes.Add(new Rope(particles[0], particles[1], 50));
        ropes.Add(new Rope(particles[1], particles[2], 50));
        ropes.Add(new Rope(particles[2], particles[3], 50));


    }

    public override void Update(float deltaTime)
    {
        foreach (Particle particle in particles)
        {
            particle.Update(deltaTime);
            Raylib.DrawCircleV(particle.position, particle.radius, particle.colour);

        }
        foreach (Rope rope in ropes)
        {
            rope.ConstrainPoints();
        }
        
        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);

    }
}