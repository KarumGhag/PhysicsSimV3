using Raylib_cs;
using GlobalInfo;

namespace GameClass;

public class Game
{
    public void Run()
    {
        Raylib.InitWindow(Global.WIDTH,Global.HEIGHT, Global.GAME_NAME);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Global.BACKGROUND_COL);


            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}   