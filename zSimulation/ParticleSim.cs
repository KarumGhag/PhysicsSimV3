using GameClass;
using Raylib_cs;
using ParticleClass;
using SimulationClass;
using System.Security.Cryptography;
using System.Numerics;
using GlobalInfo;
using CollisionSystem;
using System.Reflection.Metadata;
using System.Security;
using System.Security.Principal;

namespace ParticleSimulation;

public class ParticleSim : Simulation
{
    public List<Particle> particles = new List<Particle>();
    public int cellSize = 10;
    public int gravity = 10;
    public ParticleSim(Game game) : base(game)
    {
    }

    public override void Init()
    {
        int numParticles = 0;
        for (int i = 0; i < numParticles; i++) new Particle(10, Global.RandomVec(0, Global.WIDTH, 0, Global.HEIGHT), Global.RandomVec(-10, 10, -10, 10), particles);
    }

    public override void Reset()
    {
        particles = new List<Particle>();
        Init();
    }

    public int framesBetweenAdding = 5;
    public int currentFrame;
    public override void Update(float deltaTime)
    {

        Global.gravity = gravity;
        CellSystem.cellSize = cellSize;

        if (Global.currentSimulation == this) Global.gravity = 10;

        currentFrame++;
        if (currentFrame % framesBetweenAdding == 0)
        {
            int radius = 10;
            new Particle(radius, new Vector2(5, 5), new Vector2(7, 0), particles);
        }
        Global.cellSystem.ClearCells();
        foreach (Particle particle in particles)
        {
            particle.Update(deltaTime);
            Raylib.DrawCircleV(particle.position, particle.radius, particle.colour);
        }

        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);
    }
}