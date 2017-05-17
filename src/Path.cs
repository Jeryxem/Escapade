using System;
using System.Collections.Generic;

namespace Escapade
{
  public class Path
  {
    List<Location> _targetpath = new List<Location> ();
    Instance _instance;

    #region Properties
    public List<Location> TargetPath {
      get {
        return _targetpath;
      }
      set {
        _targetpath = value;
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

    public Path (List<Location> targetpath, Instance instance)
    {
      TargetPath = targetpath;
      Instance = instance;
    }

    public bool GetPath (Location start, Location target)
    {
      TargetPath = null;
      Pathfind (start, target);
      return TargetPath != null;
    }

    void Pathfind (Location start, Location target)
    {
      
    }
  }
}