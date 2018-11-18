using System;
using SwinGameSDK;
using System.Collections.Generic;

namespace Escapade
{
  public static class GameResources
  {
    private static Dictionary<string, Bitmap> _Images = new Dictionary<string, Bitmap> ();
    private static Dictionary<string, Music> _Music = new Dictionary<string, Music>();
    private static Dictionary<string, SoundEffect> _Sounds = new Dictionary<string, SoundEffect>();

    private static void NewImage (string imageName, string filename)
    {
      _Images.Add (imageName, SwinGame.LoadBitmap (SwinGame.PathToResource (filename, ResourceKind.BitmapResource)));
    }

    private static void NewSound (string soundName, string filename)
    {
    	_Sounds.Add (soundName, Audio.LoadSoundEffect (SwinGame.PathToResource (filename, ResourceKind.SoundResource)));
    }

    private static void NewMusic (string musicName, string filename)
    {
    	_Music.Add (musicName, Audio.LoadMusic (SwinGame.PathToResource (filename, ResourceKind.SoundResource)));
    }

    public static Bitmap GameImage (string image)
    {
      return _Images [image];
    }

    public static SoundEffect GameSound (string sound)
    {
    	return _Sounds [sound];
    }

    public static Music GameMusic (string music)
    {
    	return _Music [music];
    }

    public static void LoadImages ()
    {
      NewImage ("main_menu", "main menu.png");
    }

    public static void LoadSounds ()
    { 

    }

    public static void LoadMusic ()
    {
        NewMusic ("game_music","A_Night_Of_Dizzy_Spells.mp3");
        NewMusic ("main_menu_music", "Pixel-Island.mp3");
    }

    public static void LoadResources ()
    {
      LoadImages ();
      LoadMusic ();
      LoadSounds ();
    }

    private static void FreeImages ()
    {
    	foreach (Bitmap obj in _Images.Values) {
    		SwinGame.FreeBitmap (obj);      }
    }

    private static void FreeSounds ()
    {
    	foreach (SoundEffect obj in _Sounds.Values) {
    		Audio.FreeSoundEffect (obj);      }
    }

    private static void FreeMusic ()
    {
    	foreach (Music obj in _Music.Values) {
    		Audio.FreeMusic (obj);      }
    }

    public static void FreeResources ()
    {
    	FreeImages ();
    	FreeMusic ();
    	FreeSounds ();
    	SwinGame.ProcessEvents ();
     }

  }
}
