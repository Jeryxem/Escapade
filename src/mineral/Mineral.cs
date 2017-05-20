using Escapade.item;
using SwinGameSDK;

namespace Escapade
{
  public abstract class Mineral : Item
  {
    int _value;
    Color _colour;

    #region Properties
    public int Value {
      get {
        return _value;
      }
      set {
        _value = value;
      }
    }
    public Color Colour {
      get {
        return _colour;
      }
      set {
        _colour = value;
      }
    }
    #endregion Properties

    protected Mineral(string name, int meta, Color colour) : base(10, meta, name)
    {
      Colour = colour;
    }
  }
}