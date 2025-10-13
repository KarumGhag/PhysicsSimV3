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

    public static Color HsvToRgb(float hue, float saturation)
    {
        // Normalize hue into [0,360)
        hue = hue % 360;
        if (hue < 0) hue += 360;

        // Always full brightness
        float value = 1.0f;

        if (saturation <= 0.0f)
        {
            // Achromatic (white, since value=1)
            return new Color(255, 255, 255, 255);
        }

        float hueSector = hue / 60.0f;
        int sectorIndex = (int)hueSector;
        float fractionalPart = hueSector - sectorIndex;

        float p = value * (1 - saturation);
        float q = value * (1 - saturation * fractionalPart);
        float t = value * (1 - saturation * (1 - fractionalPart));

        float r, g, b;

        switch (sectorIndex)
        {
            case 0:
                r = value; g = t; b = p;
                break;
            case 1:
                r = q; g = value; b = p;
                break;
            case 2:
                r = p; g = value; b = t;
                break;
            case 3:
                r = p; g = q; b = value;
                break;
            case 4:
                r = t; g = p; b = value;
                break;
            default: // case 5
                r = value; g = p; b = q;
                break;
        }

        return new Color(
            (byte)(r * 255),
            (byte)(g * 255),
            (byte)(b * 255)
        );
    }


}