using SwinGameSDK;

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

    protected MoveableObject (int id, string name, Location location, Instance instance) : base (id, name, location, instance)
    {
      Path = new Path(instance);
    }

    public void Move ()
    {
      if (Path.TargetPath.Count > 0)
      {
        Path.TargetPath.Reverse();
        Location = Path.TargetPath[0];
        SwinGame.Delay (500);
        Path.TargetPath.RemoveAt(0);
        if (Path.TargetPath.Count == 0) Target = null;
      }
    }

    public void NewPath(Location target)
    {
      Target = target;
      Path.GetPath(Location, Target);
    }

  }
}