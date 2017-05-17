namespace Escapade
{
  public abstract class MoveableObject : Object
  {

    public Location _target;
    public Path _path;

    #region Properties
    public Location Target {
      get {
        return _target;
      }
      set {
        _target = value;
      }
    }
    public Path Path {
      get {
        return _path;
      }
      set {
        _path = value;
      }
    }
    #endregion Properties

    protected MoveableObject (int id, string name, Location location, Instance instance) : base (id, name, location, instance) { }

    public void Move ()
    {

    }

  }
}