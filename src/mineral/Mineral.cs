using SwinGameSDK;

namespace Escapade
{
  public abstract class Mineral
  {
    string _name;
    int _value;
    Color _colour;

    #region Properties
    public string Name {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }
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

    public Mineral(string name, Color colour)
    {
      Name = name;
      Colour = colour;
    }
  }
}