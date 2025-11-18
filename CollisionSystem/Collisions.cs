using System.Numerics;
using ParticleClass;
using GlobalInfo;
using ParticleSimulation;
using System.Data;
using System.Reflection.Metadata;

namespace CollisionSystem;

public class CircleCollider
{
    Particle particle;
    Vector2 position;
    int gridX;
    int gridY;

    List<Particle> potentialCollisions = new List<Particle>();
    public CircleCollider(Particle particle)
    {
        this.particle = particle;
        position = this.particle.position;
    }

    public void Collide(CellSystem cellSystem)
    {
        int iterations = 1;
        for (int i = 0; i < iterations; i++)
        {
            position = particle.position;

            gridX = Math.Clamp((int)Math.Floor(position.X / CellSystem.cellSize), 0, CellSystem.cols - 1);
            gridY = Math.Clamp((int)Math.Floor(position.Y / CellSystem.cellSize), 0, CellSystem.rows - 1);

            cellSystem.grid[gridX, gridY].particles.Add(particle);
            cellSystem.grid[gridX, gridY].isEmpty = false;

            potentialCollisions = cellSystem.GetNeighbourCollisions(gridX, gridY);

            foreach (Particle otherParticle in potentialCollisions)
            {
                if (otherParticle == particle) continue;
                if (particle.radius + otherParticle.radius > Vector2.Distance(position, otherParticle.position))
                {

                    float distance = Global.GetDistance(particle.position, otherParticle.position);
                    float overlap = 0.5f * (distance - particle.radius - otherParticle.radius);

                    float bounceLoss = 0.1f;

                    particle.position.X -= overlap * (particle.position.X - otherParticle.position.X) / distance * bounceLoss;
                    particle.position.Y -= overlap * (particle.position.Y - otherParticle.position.Y) / distance * bounceLoss;

                    otherParticle.position.X += overlap * (particle.position.X - otherParticle.position.X) / distance * bounceLoss;
                    otherParticle.position.Y += overlap * (particle.position.Y - otherParticle.position.Y) / distance * bounceLoss;

                }
            }
        }
    }
}


public class CellSystem
{
    public static int cellSize = 50;
    public static int cols = (int)Math.Ceiling((float)Global.WIDTH / cellSize);
    public static int rows = (int)Math.Ceiling((float)Global.HEIGHT / cellSize);


    public Cell[,] grid;

    public CellSystem()
    {
        grid = new Cell[cols, rows];

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x, y] = new Cell();
            }
        }
    }

    public void ClearCells()
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x, y].particles.Clear();
                grid[x, y].isEmpty = true;
            }
        }
    }

    public List<Particle> GetNeighbourCollisions(int x, int y)
    {
        List<Particle> particles = new List<Particle>();

        // Check current cell and all 8 surrounding cells
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (IsInBounds(x + i, y + j))
                {
                    particles.AddRange(grid[x + i, y + j].particles);
                }
            }
        }

        return particles;
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < cols && y >= 0 && y < rows;
    }


}

public class Cell
{
    public List<Particle> particles = new List<Particle>();
    public bool isEmpty;
}
