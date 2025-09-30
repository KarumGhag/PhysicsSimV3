using GameClass;
using Raylib_cs;
using ParticleClass;
using SimulationClass;
using System.Security.Cryptography;
using System.Numerics;
using GlobalInfo;

namespace ParticleSimulation;

public class ParticleSim : Simulation
{
    public ParticleSim(Game game) : base(game) { }

    public static List<Particle> particles = new List<Particle>();
    Particle particle = new Particle(10, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2), new Vector2(1, 0), particles);

    public override void Update(float deltaTime)
    {
        foreach (Particle particle in particles)
        {
            particle.Update(deltaTime);
            Raylib.DrawCircleV(particle.position, particle.radius, particle.colour);
        }
    }
}