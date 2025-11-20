namespace MouseSys;

using System.Numerics;
using CollisionSystem;
using GlobalInfo;
using Raylib_cs;
using ParticleClass;
using System.Runtime.InteropServices;

public class Mouse
{
    public Vector2 mousePosition;
    public int gridX;
    public int gridY;
    public int radius = 10;
    private CellSystem cellSystem;

    private List<Particle> potentialCollisions = new List<Particle>();

    public Mouse(CellSystem cellSystem)
    {
        this.cellSystem = cellSystem;
    }
    public void Update(float deltaTIme)
    {
        mousePosition = Raylib.GetMousePosition();
        gridX = Math.Clamp((int)Math.Floor(mousePosition.X / CellSystem.cellSize), 0, CellSystem.cols - 1);
        gridY = Math.Clamp((int)Math.Floor(mousePosition.Y / CellSystem.cellSize), 0, CellSystem.rows - 1);

        potentialCollisions = cellSystem.GetNeighbourCollisions(gridX, gridY);

        Raylib.DrawText("mousePos: " + Convert.ToString(gridX) + " " + Convert.ToString(gridY), 10, 50, 25, Color.White);


        foreach (Particle otherParticle in potentialCollisions)
        {
            if (radius + otherParticle.radius > Vector2.Distance(mousePosition, otherParticle.position))
            {
                if (Raylib.IsKeyDown(KeyboardKey.W)) otherParticle.isGrabbed = !otherParticle.isGrabbed;
            }
            
        }
    }
}