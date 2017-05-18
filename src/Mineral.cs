using SwinGameSDK;

namespace Escapade
{
  public class Mineral
  {

    string _name;
    MineralType _type;

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
    public MineralType Type {
      get {
        return _type;
      }
      set {
        _type = value;
        switch (Type) {
        case MineralType.Diamond: Colour = Color.Aquamarine; break;
        case MineralType.Emerald: Colour = Color.ForestGreen; break;
        case MineralType.Ruby: Colour = Color.IndianRed; break;
        case MineralType.Sapphire: Colour = Color.RoyalBlue; break;
        }
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

    public Mineral (MineralType type)
    {
      Name = type.ToString ();
      Type = type;
    }
  }

  public enum MineralType
  {
    Diamond,
    Emerald,
    Ruby,
    Sapphire
  }
}