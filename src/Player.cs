using System;
using SwinGameSDK;

namespace Escapade
{
  public class Player : MoveableObject
  {

    #region Properties

    #endregion Properties

    public Player(int id, string name, Location location, Instance instance) : base(id, name, location, instance)
    {
    }

    public override void Draw()
    {
      SwinGame.FillRectangle(Color.MediumVioletRed, Location.X * Instance.Size, Location.Y * Instance.Size, Instance.Size, Instance.Size);
      SwinGame.DrawRectangle(Color.White, Location.X * Instance.Size, Location.Y * Instance.Size, Instance.Size, Instance.Size);
    }

    public override void Update()
    {
      Move();
    }
  }
}
