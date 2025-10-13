using GameClass;
using Raylib_cs;
using ParticleClass;
using SimulationClass;
using System.Security.Cryptography;
using System.Numerics;
using GlobalInfo;
using CollisionSystem;

namespace ParticleSimulation;

public class ParticleSim : Simulation
{
    public ParticleSim(Game game) : base(game)
    {
        Global.currentSimulation = this;
        Global.cellSystem = new CellSystem();

        int numParticles = 100;
        for (int i = 0; i < numParticles; i++) new Particle(10, Global.RandomVec(0, Global.WIDTH, 0, Global.HEIGHT), Global.RandomVec(-50, 50, -10, 10), particles);
    }

    public static List<Particle> particles = new List<Particle>();

    public override void Update(float deltaTime)
    {
        foreach (Particle particle in particles)
        {
            particle.Update(deltaTime);
            Raylib.DrawCircleV(particle.position, particle.radius, particle.colour);
        }
    }
}