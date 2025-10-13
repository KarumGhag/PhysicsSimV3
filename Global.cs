using System.Numerics;
using Raylib_cs;
using SimulationClass;
using CollisionSystem;

namespace GlobalInfo;

public static class Global
{
    public static int WIDTH = 1920;
    public static int HEIGHT = 1080;
    public static string GAME_NAME = "Game";
    public static Color BACKGROUND_COL = Color.Black;
    public static Simulation currentSimulation;
    public static CellSystem cellSystem = new CellSystem();


    public static Vector2 RandomVec(int minX, int maxX, int minY, int maxY)
    {
        Random random = new Random();

        return new Vector2(random.Next(minX, maxX), random.Next(minY, maxY));
    }

    public static float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        float dx = pos1.X - pos2.X;
        float dy = pos1.Y - pos2.Y;

        return (float)Math.Sqrt(dx * dx + dy * dy);
    }

}