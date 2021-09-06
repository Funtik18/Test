using UnityEngine;

public class DataManager
{
    private static Data data;
    public static Data Data
    {
        get
        {
            if (data == null)
            {
                data = LoadData();

                if (data == null)//First Time
                {
                    data = NewData();
                    SaveData();
                }
            }

            return data;
        }
    }

    #region Statistics
    public static int ScoreBest
    {
        get => Data.statistics.scoreBest;
        set
        {
            Data.statistics.scoreBest = value;

            SaveData();
        }
    }
    #endregion

    #region Settings
    public static SettingsData Settings => Data.settings;

    public static bool IsMusic
    {
        get => Settings.music;
        set
        {
            Settings.music = value;

            SaveData();
        }
    }
    public static bool IsSound
    {
        get => Settings.sound;
        set
        {
            Settings.sound = value;

            SaveData();
        }
    }
    public static bool IsVibration
    {
        get => Settings.vibration;
        set
        {
            Settings.vibration = value;

            SaveData();
        }
    }
    #endregion

    private static Data NewData()
    {
        Data newData = new Data()
        {
            statistics = new StatisticsData()
            {
                scoreBest = 0,
            },
            settings = new SettingsData()
            {
                sound = true,
                music = true,
                vibration = true,
            },
        };

        return newData;
    }

    private static Data LoadData() => SaveLoadManager.LoadJsonDataFromPlayerPrefs<Data>("DATA");
    private static void SaveData() => SaveLoadManager.SaveJsonDataToPlayerPrefs("DATA", Data);
}
[System.Serializable]
public class Data
{
    public StatisticsData statistics;
    public SettingsData settings;
}
[System.Serializable]
public class StatisticsData
{
    public int scoreBest;
}

[System.Serializable]
public class SettingsData
{
    public bool music;
    public bool sound;
    public bool vibration;
}