
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveBestScore(int bestScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.fap";
        FileStream stream =  new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, bestScore);
        stream.Close();
    }
    public static int LoadBestScore()
    {
  
        string path = Application.persistentDataPath + "/PlayerData.fap";
        int bestScore = 0;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            bestScore = (int) formatter.Deserialize(stream);
            stream.Close();
        }
        return bestScore;
    }
}
