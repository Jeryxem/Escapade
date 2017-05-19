using SwinGameSDK;

namespace Escapade
{
  public abstract class Mineral
  {
    string _name;
    int _value;

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
    #endregion Properties

    public Mineral(string name)
    {
      Name = name;
    }
  }
}