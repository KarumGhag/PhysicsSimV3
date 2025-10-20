using Raylib_cs;
using GlobalInfo;
using SimulationClass;
using ParticleSimulation; 

namespace GameClass;

public class Game
{
    public void Run()
    {
        Simulation currentSim = new ParticleSim(this);
        Raylib.InitWindow(Global.WIDTH, Global.HEIGHT, Global.GAME_NAME);

        Raylib.SetTargetFPS(120);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Global.BACKGROUND_COL);


            if (currentSim != null) currentSim.Update(Raylib.GetFrameTime());
            Raylib.DrawText("FPS: " + Convert.ToString(Math.Round(1 / Raylib.GetFrameTime(), 0)), 10, 10, 25, Color.White);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}   