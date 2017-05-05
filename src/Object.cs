using Escapade.src.world;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src
{
  public abstract class Object
  {

    private int _id;
    private string _name;
    private Location _location;
    private Escapade _instance;
    
    public int ID { get { return _id; } set { _id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public Location Location { get { return _location; } set { _location = value; } }
    public Escapade Instance { get { return _instance; } set { _instance = value; } }

    public Object(int id, string name, Location loc, Escapade instance)
    {
      ID = id;
      Name = name;
      Location = loc;
      Instance = instance;
    }

    public Object(int id, string name, Escapade instance) : this(id, name, instance.World.RandomEmpty(), instance) { }

  }
}
