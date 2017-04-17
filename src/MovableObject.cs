namespace Escapade
{
  class MovableObject : GameObject
  {
    private int _xDest;
    private int _yDest;

    public int XDest { get => _xDest; set => _xDest = value; }
    public int YDest { get => _yDest; set => _yDest = value; }

    public MovableObject ()
    {

    }
  }
}