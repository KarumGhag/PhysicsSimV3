using System.Numerics;
using Raylib_cs;
using GlobalInfo;

namespace ParticleClass;

public class Particle
{
    public static float gravity = 1750f;
    public Vector2 position;
    public Vector2 oldPosition;
    public Vector2 velocity;
    public int mass = 1;
    public int radius;
    public Color colour;

    public Particle(int radius, Vector2 startPos, Vector2 startVelocity)
    {
        position = startPos;
        oldPosition = position - startVelocity;

        velocity = startVelocity;
        this.radius = radius;

    }

    public void Update(float deltaTime)
    {
        velocity = position - oldPosition;
        oldPosition = position;
        position += velocity * 0.9999f;
        position.Y += gravity * deltaTime * deltaTime;
    }
}