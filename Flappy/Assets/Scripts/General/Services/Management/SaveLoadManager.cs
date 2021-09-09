using System.IO;

using UnityEngine;

public class SaveLoadManager
{
    private const string DATA_KEY = "DATA";

    private const string AUDIO_RESOURCES_PATH = "Audio";

    public static AudioClip[] LoadAllAudio() => Resources.LoadAll<AudioClip>(AUDIO_RESOURCES_PATH);

    public static Data LoadData() => LoadJsonDataFromPlayerPrefs<Data>(DATA_KEY);
    public static void SaveData(Data data) => SaveJsonDataToPlayerPrefs(DATA_KEY, data);



    public static void SaveJsonDataToPlayerPrefs<T>(string key, T data) => PlayerPrefs.SetString(key, ConvertToJson(data));
    public static T LoadJsonDataFromPlayerPrefs<T>(string key, string defaultValue = "") => ConvertFromJson<T>(PlayerPrefs.GetString(key, defaultValue));

    public static string ConvertToJson<T>(T data) => JsonUtility.ToJson(data, true);
    public static T ConvertFromJson<T>(string json) => JsonUtility.FromJson<T>(json);

    /// <summary>
	/// Сохранение объекта в Json.
	/// </summary>
	private static void SaveDataToJson<T>(T data, string directory, string fileName)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string jsonData = ConvertToJson(data);
        File.WriteAllText(dir + fileName, jsonData);
    }

    /// <summary>
    /// Загрузка объекта из Json в объект.
    /// </summary>
    private static T LoadDataFromJson<T>(string directory, string fileName)
    {
        string fullPath = Application.persistentDataPath + directory + fileName;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            return ConvertFromJson<T>(json);
        }
        else
        {
            //Debug.LogError("File doesn't exist");
        }

        return default;
    }
    private static T LoadDataFromJson<T>(string fullPath)
    {
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            return ConvertFromJson<T>(json);
        }
        else
        {
            //Debug.LogError("File doesn't exist");
        }

        return default;
    }
}