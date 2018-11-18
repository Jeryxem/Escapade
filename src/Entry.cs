using SwinGameSDK;

namespace Escapade
{
  public class Entry
  {
    /// <summary>
    /// Entry point of the program - starts the game up
    /// </summary>
    public static void Main ()
    {
      Escapade.GetInstance ().Start ();
    }
  }
}