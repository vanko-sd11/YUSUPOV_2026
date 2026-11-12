using UnityEngine;
using System.IO;
using System;

public class DataStorage : MonoBehaviour
{
    private string FolderPath => Application.persistentDataPath;

    private string SettingsParh => Path.Combine(FolderPath, "settings.json");

    private string PlayerPath => Path.Combine(FolderPath, "player.json");
    private string KeyBindingsPath => Path.Combine(FolderPath, "keybndings.json");

    public void SaveSettings(GameSettings settings)
    {
        try
        {
            string json = JsonUtility.ToJson(settings, true);
            File.WriteAllText(SettingsParh, json);
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public GameSettings LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsParh))
            {
                string json = File.ReadAllText(SettingsParh);
                return JsonUtility.FromJson<GameSettings>(json);
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
        return null;
    }
    public void PlayerSave(PlayerData player)
    {
        try
        {
            string json = JsonUtility.ToJson(player, true);
            File.WriteAllText(PlayerPath, json);
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public PlayerData LoadPlayer()
    {
        try
        {
            if (File.Exists(PlayerPath))
            {
                string json = File.ReadAllText(PlayerPath);
                return JsonUtility.FromJson<PlayerData>(json);
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
        return null;
    }

    public void SaveKeyBindings(string json)
    {
        try
        {
            File.WriteAllText(KeyBindingsPath, json);
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public string LoadKeyBindings()
    {
        try
        {
            if (File.Exists(KeyBindingsPath))
            {
                return File.ReadAllText(KeyBindingsPath);
            }
        }
        catch( Exception ex)
        {
            Debug.LogException(ex);
        }
        return null;
    }
    
}
