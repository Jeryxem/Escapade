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
      NewImage ("main_menu", "main_menu.png");
      NewImage("instructions","instructions.png");
      NewImage ("wasd", "WASD.png");
      NewImage ("arrow_key", "Arrow Key.png");
      NewImage ("arrow_key_O", "Arrow Key O.png");
      NewImage ("b_key", "B Key.png");
      NewImage ("j_p_key", "J P Key.png");
      NewImage ("o_key", "O Key.png");
      NewImage ("p_key", "P Key.png");
      NewImage ("shift_b_key", "Shift B Key.png");
      NewImage ("v_key", "V Key.png");
      NewImage ("wasd_v_key", "WASD V Key.png");
      NewImage ("player_one_wins", "player_one_wins.png");
      NewImage ("player_two_wins", "player_two_wins.png");
      NewImage ("you_died", "you_died.png");
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
