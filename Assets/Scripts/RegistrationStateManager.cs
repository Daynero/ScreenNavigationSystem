using System;
using Newtonsoft.Json;
using UnityEngine;

public class RegistrationStateManager
{
    private const string ScreenToLoadKey = "ScreenToLoad";
    private readonly ScreenNavigationSystem _screenNavigationSystem;
    private readonly ScreenName _defaultScreenName;

    public RegistrationStateManager(ScreenNavigationSystem screenNavigationSystem, ScreenName defaultScreenName)
    {
        _screenNavigationSystem = screenNavigationSystem;
        _defaultScreenName = defaultScreenName;
    }

    public void SaveData<T>(ScreenName screenName, T data)
    {
        string json = JsonConvert.SerializeObject(new TypedData<T> { Type = typeof(T).AssemblyQualifiedName, Data = data });
        PlayerPrefs.SetString(screenName.ToString(), json);
        PlayerPrefs.SetString(ScreenToLoadKey, screenName.ToString());
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        string savedScreenName = PlayerPrefs.GetString(ScreenToLoadKey, _defaultScreenName.ToString());
        if (!string.IsNullOrEmpty(savedScreenName) && Enum.TryParse(savedScreenName, out ScreenName screenName))
        {
            string json = PlayerPrefs.GetString(screenName.ToString(), string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                var typedData = JsonConvert.DeserializeObject<TypedData<object>>(json);
                if (typedData != null)
                {
                    Type dataType = Type.GetType(typedData.Type);
                    object data = JsonConvert.DeserializeObject(typedData.Data.ToString(), dataType);
                    _screenNavigationSystem.ShowWithData(screenName, data);
                }
            }
            else
            {
                _screenNavigationSystem.Show(ScreenName.First);
            }
        }
    }

    public void ClearData()
    {
        string savedScreenName = PlayerPrefs.GetString(ScreenToLoadKey, string.Empty);
        if (!string.IsNullOrEmpty(savedScreenName))
        {
            PlayerPrefs.DeleteKey(savedScreenName);
        }
        PlayerPrefs.DeleteKey(ScreenToLoadKey);
        PlayerPrefs.Save();
    }

    private class TypedData<T>
    {
        public string Type { get; set; }
        public T Data { get; set; }
    }
}
