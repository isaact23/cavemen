using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    [Serializable]
    public class SaveData
    {
        public int level;
    }
    
    public static void SaveGame(int level)
    {
        SaveData save = new SaveData();
        save.level = level;
        
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath + "/savedata.save"); 
        bf.Serialize(file, save); 
        file.Close();

        Debug.Log("Game saved successfully - level " + level.ToString());
        Debug.Log(Application.persistentDataPath);
    }
    
    public static int LoadGame()
    { 
        if (File.Exists(Application.persistentDataPath + "/savedata.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedata.save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            
            Debug.Log("Game loaded successfully.");
            return save.level;
        } else {
            Debug.Log("Could not load game - no save data found.");
            return 0;
        }
    }
}
