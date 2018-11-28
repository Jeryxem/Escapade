using System;
namespace Escapade
{
  /// <summary>
  /// The different states the game can be in
  /// </summary>
  public enum GameState
  {
    ViewingMainMenu,
    ViewingInstructions,
    SinglePlayerMode,
    TwoPlayerMode,
    PlayerOneWins,
    PlayerTwoWins,
    SinglePlayerEndGame,
    QuittingGame
  }
}
