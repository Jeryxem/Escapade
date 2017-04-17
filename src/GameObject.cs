namespace Escapade
{
  public abstract class GameObject
  {
    private int _id;
    private string _name;
    private int _xPos;
    private int _yPos;

    public int ID { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int X { get => _xPos; set => _xPos = value; }
    public int Y { get => _yPos; set => _yPos = value; }

  }
}