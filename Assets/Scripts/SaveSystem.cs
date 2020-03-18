using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void savePlayer(Player player)
    {
        // Create file for writing data
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        // Player data
        PlayerData data = new PlayerData(player);

        //Writing data to file
        formatter.Serialize(stream, data);

        //Close stream
        stream.Close();
    }


    public static PlayerData loadPlayer()
    {
        // Save file
        string path = Application.persistentDataPath + "/player.save";

        if (File.Exists(path))
        {
            // Open file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // Read file
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            // Close stream;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No file object");
            return null;
        }
    }
}
