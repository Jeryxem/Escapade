using System;
namespace Escapade
{
  /// <summary>
  /// Bitmap Mask for tiles
  /// This allows tiles with a specific pattern of neighbours to use the same
  /// bitmap without having to have a copy of the specific bitmap for every individual tile
  /// </summary>
  [Flags]
  public enum BitmapMask
  {
    None = 0,
    North = 1 << 0,
    East = 1 << 1,
    South = 1 << 2,
    West = 1 << 3
  }
}
