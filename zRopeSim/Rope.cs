namespace RopeClass;

using ParticleClass;
using CollisionSystem;
using System.Numerics;
using GlobalInfo;
using Raylib_cs;

public class Rope
{
    List<RopePart> strings = new List<RopePart>();
    List<Particle> points = new List<Particle>();

    public Rope(int numPoints, int restLen, Vector2 startPos, List<Particle> particles, List<Rope> ropes)
    {
        for (int i = 0; i < numPoints; i++)
        {
            points.Add(new Particle(7, startPos, Vector2.Zero, particles));
            points[i].myRope = this;
            startPos += new Vector2(15, 0);
        }

        points[0].stationary = true;

        for (int i = 1; i < numPoints; i++)
        {
            AddPart(new RopePart(points[i - 1], points[i], restLen));
        }

        ropes.Add(this);
    }
    

    public void AddPart(RopePart rope)
    {
        strings.Add(rope);
    }

    public void ConstrainRope()
    {
        foreach (RopePart rope in strings)
        {
            rope.ConstrainPoints();
        }
    }
    
    public void DrawLines()
    {
        for (int i = 1; i < points.Count; i++)
        {
            Raylib.DrawLineEx(points[i - 1].position, points[i].position, 2, Color.White);
        }
    }
}
public class RopePart
{
    Particle point1;
    Particle point2;

    float restLen;
    Vector2 midPoint;


    public RopePart(Particle p1, Particle p2, float rest)
    {
        point1 = p1;
        point2 = p2;

        restLen = rest;
    }

    public float getAngle()
    {
        Vector2 midToP1 = point1.position - midPoint;

        float opp = midToP1.Y;
        float adj = midToP1.X;

        return (float)Math.Atan2(opp, adj);
    }

    public Vector2[] getCorrectedPoints()
    {
        float angleToP1 = getAngle();
        float adj = (float)Math.Cos(angleToP1) * (restLen / 2);
        float opp = (float)Math.Sin(angleToP1) * (restLen / 2);

        Vector2 corrected1 = midPoint + new Vector2(adj, opp);
        Vector2 corrected2 = midPoint - new Vector2(adj, opp);

        Vector2[] correctedPositions = { corrected1, corrected2 };
        return correctedPositions;
    }
    public void ConstrainPoints()
    {
        midPoint = (point1.position + point2.position) / 2;

        Vector2 desiredP1 = getCorrectedPoints()[0];
        Vector2 desiredP2 = getCorrectedPoints()[1];

        Vector2 p1Offset = desiredP1 - point1.position;
        Vector2 p2Offset = desiredP2 - point2.position;

        if (!point1.stationary) point1.position += p1Offset;
        if (!point2.stationary) point2.position += p2Offset;
    }
}