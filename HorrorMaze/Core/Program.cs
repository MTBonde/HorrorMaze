using HorrorMaze;
using System;

//using var game = new HorrorMaze.GameWorld();
//game.Run();

public static class Program
{
    [STAThread]
    static void Main()
    {
        GameWorld.Instance.Run();
    }
}