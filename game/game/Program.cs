using game;
using System;

public static class Program
{
    [STAThread]
    static void Main()
    {           
        GameplayPresenter game = new GameplayPresenter(
          new GameCycleView(), new GameCycle()
        );
        game.LaunchGame();
    }
}