namespace PlinkoSimulation;
using SimulationClass;
using ParticleClass;
using GlobalInfo;
using GameClass;
using CollisionSystem;
using MouseSys;
using Raylib_cs;
using System.Numerics;

public class PlinkoSim : Simulation
{
    public List<Particle> particles = new List<Particle>();
    int cellSize = 40;
    int gravity = 750;
    public PlinkoSim(Game game) : base(game)
    {
    }

    public override void Init()
    {
        Global.cellSystem.ClearCells();
        particles = new List<Particle>();
        new Particle(20, new Vector2(Global.WIDTH / 2 + new Random().Next(-5, 5), 30), Vector2.Zero, particles);
        CreatePlinko(10);
    }

    public override void Reset()
    {
        particles = new List<Particle>();

        Init();
    }

    public void CreatePlinko(int layers)
    {
        List<Particle> plinkoPoints = new List<Particle>();

        int lowest = 1000;

        for (int i = layers; i > -1; i--)
        {
            for (int j = 0; j < i; j++)
            {
                int divisor = (int)Math.Pow(2, i);
                int x = Global.WIDTH / divisor;
                plinkoPoints.Add(new Particle(10, new Vector2(x, lowest), Vector2.Zero, particles));
            }

            lowest -= 100;
        }

        foreach (Particle plinkoPoint in plinkoPoints)
        {
            plinkoPoint.stationary = true;
            plinkoPoint.mass = 10000;
        }
    }

    public override void Update(float deltaTime)
    {
        CellSystem.cellSize = cellSize;
        Global.gravity = gravity;
        Global.cellSystem.ClearCells();

        foreach (Particle particle in particles)
        {
            particle.Update(deltaTime);
            Raylib.DrawCircleV(particle.position, particle.radius, particle.colour);
        }


        Global.mouse?.Update(Raylib.GetFrameTime());

    }

}