namespace MouseSys;

using System.Numerics;
using CollisionSystem;
using GlobalInfo;
using Raylib_cs;
using ParticleClass;
using System.Runtime.InteropServices;
using System.Diagnostics.Tracing;
using RopeClass;

public class Mouse
{
    public Vector2 mousePosition;
    public int gridX;
    public int gridY;
    public int radius = 15;
    private CellSystem cellSystem;

    private List<Particle> potentialCollisions = new List<Particle>();
    private Particle? selected;

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

        bool selectedLastFrame = false;

        foreach (Particle otherParticle in potentialCollisions)
        {
            if (!(radius + otherParticle.radius > Vector2.Distance(mousePosition, otherParticle.position))) continue;

            if (Raylib.IsKeyReleased(KeyboardKey.W) && selected == null)
            {
                selected = otherParticle;
                selectedLastFrame = true;
            }
        }

        if (selected != null)
        {
            selected.position = mousePosition;
            selected.oldPosition = mousePosition;

            Raylib.DrawText("Particle info: ", 10, 95, 25, Color.White);
            Raylib.DrawText("Mass: " + Convert.ToString(selected.mass), 17, 125, 25, Color.White);
            Raylib.DrawText("Stationary: " + Convert.ToString(selected.stationary), 17, 155, 25, Color.White);


            if (Raylib.IsKeyReleased(KeyboardKey.S)) selected.stationary = !selected.stationary;
            if (Raylib.IsKeyDown(KeyboardKey.P)) selected.mass += 1f;
            if (Raylib.IsKeyDown(KeyboardKey.O)) selected.mass -= 1f;
        }

        if (Raylib.IsKeyReleased(KeyboardKey.W) && selected != null && !selectedLastFrame) selected = null;
    }
}
