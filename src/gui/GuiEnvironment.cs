using System.Collections.Generic;

namespace Escapade.gui
{
  public class GuiEnvironment
  {
    static GuiEnvironment _instance;
    GuiContainer _baseElement;

    public GuiContainer Base {
      get {
        return _baseElement;
      }
      set {
        _baseElement = value;
      }
    }

    protected GuiEnvironment ()
    {
    }

    public static GuiEnvironment GetInstance ()
    {
      if (_instance == null)
        _instance = new GuiEnvironment ();
      return _instance;
    }
  }
}
