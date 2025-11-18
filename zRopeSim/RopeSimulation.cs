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

        ropes.Add(addRope(10, 50, new Vector2(Global.WIDTH / 2, 10)));
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
            rope.ConstrainRope();
        }
        
        Raylib.DrawText("Particles: " + Convert.ToString(particles.Count()), 10, 30, 25, Color.White);

    }

    public Rope addRope(int numPoints, int restLen, Vector2 startPos)
    {
        List<Particle> points = new List<Particle>();
        Rope thisRope = new Rope();

        for (int i = 0; i < numPoints; i++)
        {
            points.Add(new Particle(10, startPos, Vector2.Zero, particles));
            startPos += new Vector2(restLen , 0 );
        }

        points[0].stationary = true;

        for (int i = 1; i < numPoints; i++)
        {
            thisRope.Add(new RopePart(points[i - 1], points[i], restLen));
        }

        return thisRope;

    }
}