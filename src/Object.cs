namespace Escapade
{
  public abstract class Object
  {

    int _id;
    string _name;
    Location _location;
    Instance _instance;

    #region Properties
    public int Id {
      get {
        return _id;
      }
      set {
        _id = value;
      }
    }
    public string Name {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }
    public Location Location {
      get {
        return _location;
      }
      set {
        _location = value;
      }
    }
    public Instance Instance {
      get {
        return _instance;
      }
      set {
        _instance = value;
      }
    }
    #endregion Properties

    protected Object (int id, string name, Location location, Instance instance)
    {
      Id = id;
      Name = name;
      Location = location;
      Instance = instance;
    }

    public abstract void Update ();

    public abstract void Draw ();

  }
}