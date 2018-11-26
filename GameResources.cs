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

    public static void LoadFonts()
        {
            SwinGame.LoadFont("Arial", 20);
        }

    public static void LoadImages ()
    {
      NewImage ("main_menu", "main_menu.png");
      NewImage("instructions","instructions.png");
      NewImage ("wasd", "WASD.png");
      NewImage ("arrow_key", "ArrowKey.png");
      NewImage ("arrow_key_O", "ArrowKeyO.png");
      NewImage ("b_key", "BKey.png");
      NewImage ("j_p_key", "JPKey.png");
      NewImage ("o_key", "OKey.png");
      NewImage ("p_key", "PKey.png");
      NewImage ("shift_b_key", "ShiftBKey.png");
      NewImage ("v_key", "VKey.png");
      NewImage ("wasd_v_key", "WASD_VKey.png");
      NewImage ("player_one_wins", "player_one_wins.png");
      NewImage ("player_two_wins", "player_two_wins.png");
      NewImage ("you_died", "you_died.png");
      NewImage ("arrow_key_K", "ArrowKeyK.png");
      NewImage ("arrow_key_L", "ArrowKeyL.png");
      NewImage ("wasd_f_key", "WASD_FKey.png");
      NewImage ("wasd_g_key", "WASD_GKey.png");
    }

    public static void LoadSounds ()
    { 

    }

    public static void LoadMusic ()
    {
        NewMusic ("game_music","A_Night_Of_Dizzy_Spells.wav");
        NewMusic ("main_menu_music", "Pixel-Island.wav");
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
    		SwinGame.FreeBitmap (obj);
      }
    }

    private static void FreeSounds ()
    {
    	foreach (SoundEffect obj in _Sounds.Values) {
    		Audio.FreeSoundEffect (obj);
      }
    }

    private static void FreeMusic ()
    {
    	foreach (Music obj in _Music.Values) {
    		Audio.FreeMusic (obj);
      }
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
