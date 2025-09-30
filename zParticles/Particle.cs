using System.Numerics;
using Raylib_cs;
using GlobalInfo;

namespace ParticleClass;

public class Particle
{
    public static float gravity = 1750f;
    public static float friction = 0.9999f;
    public static float bounceLoss = 0.99999f;
    public Vector2 position;
    public Vector2 oldPosition;
    public Vector2 velocity;
    public int mass = 1;
    public int radius;
    public Color colour;

    public Particle(int radius, Vector2 startPos, Vector2 startVelocity, List<Particle> particles)
    {
        position = startPos;
        oldPosition = position - startVelocity;

        velocity = startVelocity;
        this.radius = radius;

        colour = Color.Red;

        particles.Add(this);

    }

    public void Update(float deltaTime)
    {
        velocity = position - oldPosition;
        oldPosition = position;
        position += velocity * friction;
        position.Y += gravity * deltaTime * deltaTime;

        Bounce();
    }

    public void Bounce()
    {
        if (position.X > Global.WIDTH)
        {
            position.X = Global.WIDTH;
            oldPosition.X = (velocity.X + position.X) * bounceLoss;
        }

        else if (position.X < 0)
        {
            position.X = 0;
            oldPosition.X = (velocity.X + position.X) * bounceLoss;
        }


        if (position.Y > Global.HEIGHT)
            {
                position.Y = Global.HEIGHT;
                oldPosition.Y = (velocity.Y + position.Y) * bounceLoss;
            }

            else if (position.Y < 0)
            {
                position.Y = 0;
                oldPosition.Y = (velocity.Y * bounceLoss) * bounceLoss;
            }
    }
}