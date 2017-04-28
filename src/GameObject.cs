namespace Escapade
{
  public abstract class GameObject
  {
    private int _id;
    private string _name;
    private int _xPos;
    private int _yPos;

    protected int ID { get { return _id; } set { _id = value; } }
    protected string Name { get { return _name; } set { _name = value; } }
		protected int X { get { return _xPos; } set { _xPos = value; } }
		protected int Y { get { return _yPos; } set { _yPos = value; } }

		protected GameObject(int i, string name, Coordinate c)
    {
      ID = i;
      Name = name;
      X = c.X;
      Y = c.Y;
    }
  }
}