namespace Escapade
{
  public abstract class Entity
  {

    protected int _id;
	  protected string _name;
	  protected Location _location;

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
    #endregion Properties

    protected Entity (int id, string name, Location location)
    {
      Id = id;
      Name = name;
      Location = location;
    }

    /// <summary>
    /// Update the entity - children should override this method
    /// </summary>
    public abstract void Update ();

    /// <summary>
    /// Draw the entity at its current location - children should override this method
    /// </summary>
    public abstract void Draw ();

  }
}