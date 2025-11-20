using Raylib_cs;
using GlobalInfo;
using SimulationClass;
using ParticleSimulation;
using RopeSimultaion;
using CollisionSystem;
using RopeClass;
using MouseSys;

namespace GameClass;

public class Game
{
    public void Run()
    {
        Raylib.InitWindow(Global.WIDTH, Global.HEIGHT, Global.GAME_NAME);
        Raylib.SetTargetFPS(120);

        Global.cellSystem = new CellSystem();
        Global.mouse = new Mouse(Global.cellSystem);

        RopeSim ropeSim = new RopeSim(this);
        ParticleSim particleSim = new ParticleSim(this);
        List<Simulation> simulations = new List<Simulation>{ ropeSim, particleSim };
        int simInt = 0;
        Simulation currentSim = simulations[simInt];

        ropeSim.Init();
        particleSim.Init();

        Global.currentSimulation = currentSim;

        while (!Raylib.WindowShouldClose())
        {

            if (Raylib.IsKeyReleased(KeyboardKey.Right))
            {
                simInt++;
                simInt %= simulations.Count;
                currentSim = simulations[simInt];
                Global.currentSimulation = currentSim;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Global.BACKGROUND_COL);

            if (currentSim != null) currentSim.Update(Raylib.GetFrameTime());

            if (Raylib.IsKeyReleased(KeyboardKey.Space)) currentSim?.Reset();

            Raylib.DrawText("FPS: " + Convert.ToString(Math.Round(1 / Raylib.GetFrameTime(), 0)), 10, 10, 25, Color.White);

         
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
