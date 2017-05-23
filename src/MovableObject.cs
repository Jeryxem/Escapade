using System;
using SwinGameSDK;

namespace Escapade
{
  public abstract class MoveableObject : Entity
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

    /// <summary>
    /// Initializes a new <see cref="T:Escapade.MoveableObject"/>
    /// </summary>
    /// <param name="id">Unique id</param>
    /// <param name="name">Name</param>
    /// <param name="location">Location</param>
    protected MoveableObject (int id, string name, Location location) : base (id, name, location)
    {
      Path = new Path();
    }

    /// <summary>
    /// Move on the path towards the target location - if we have reached
    /// our destination, set the target location to null
    /// </summary>
    public void Move ()
    {
      if (Path.TargetPath.Count > 0)
      {
        Location = Path.TargetPath[0];
        SwinGame.Delay (100);
        Path.TargetPath.RemoveAt(0);
        if (Path.TargetPath.Count == 0) Target = null;
      }
    }

    /// <summary>
    /// Gets the pathfinder to find a path between the current location
    /// and the target location - if a path cannot be found (the target
    /// is in a disconnected cave, set this object's target to null
    /// so it stops trying to move on the path.
    /// </summary>
    /// <param name="target">Target.</param>
    public void NewPath(Location target)
    {
      Target = target;
      try {
        Path.GetPath (Location, Target);
      } catch {
        Target = null;
      }
    }

  }
}