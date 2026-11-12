using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public float SoundVolume = 1f;
    public float MusicVolume = 1f;
    public bool Fullscreen = true;
    public float MouseSensitivity = 1f;
    public string Language = "Russian";
    public int ResolutionIndex = 0;
}
