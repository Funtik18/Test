public class DataHolder
{
    private Data data = null;

    public DataHolder()
    {
        data = LoadData();
        if (data == null)//First Time
        {
            data = NewData();
            SaveData();
        }
    }

    #region Statistics
    public int ScoreBest
    {
        get => data.statistics.scoreBest;
        set
        {
            data.statistics.scoreBest = value;

            SaveData();
        }
    }
    #endregion

    #region Settings
    private SettingsData Settings => data.settings;

    public bool IsMusic
    {
        get => Settings.music;
        set
        {
            Settings.music = value;

            SaveData();
        }
    }
    public bool IsSound
    {
        get => Settings.sound;
        set
        {
            Settings.sound = value;

            SaveData();
        }
    }
    public bool IsVibration
    {
        get => Settings.vibration;
        set
        {
            Settings.vibration = value;

            SaveData();
        }
    }

    public void ChandeMusic(bool trigger)
    {
        IsMusic = trigger;
    }
    public void ChandeSound(bool trigger)
    {
        IsSound = trigger;
    }
    public void ChandeVibration(bool trigger)
    {
        IsVibration = trigger;
    }
    #endregion

    private Data NewData()
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

    private void SaveData() => SaveLoadManager.SaveData(data);
    private Data LoadData() => SaveLoadManager.LoadData();
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