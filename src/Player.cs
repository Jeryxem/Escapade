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
      if (Path.TargetPath.Count > 0) {
        foreach (Location loc in Path.TargetPath) {
          SwinGame.FillCircle(Color.LightPink, (loc.X * Instance.Size) + (Instance.Size / 2), (loc.Y * Instance.Size) + (Instance.Size / 2), Instance.Size / 3);
          SwinGame.DrawRectangle(Color.White, loc.X* Instance.Size, loc.Y* Instance.Size, Instance.Size, Instance.Size);
        }
      }
    }

    public override void Update()
    {
      Move();
    }
  }
}
