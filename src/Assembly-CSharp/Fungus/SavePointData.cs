using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus;

[Serializable]
public class SavePointData
{
	[SerializeField]
	protected string savePointKey;

	[SerializeField]
	protected string savePointDescription;

	[SerializeField]
	protected string sceneName;

	[SerializeField]
	protected List<SaveDataItem> saveDataItems = new List<SaveDataItem>();

	public string SavePointKey
	{
		get
		{
			return savePointKey;
		}
		set
		{
			savePointKey = value;
		}
	}

	public string SavePointDescription
	{
		get
		{
			return savePointDescription;
		}
		set
		{
			savePointDescription = value;
		}
	}

	public string SceneName
	{
		get
		{
			return sceneName;
		}
		set
		{
			sceneName = value;
		}
	}

	public List<SaveDataItem> SaveDataItems => saveDataItems;

	protected static SavePointData Create(string _savePointKey, string _savePointDescription, string _sceneName)
	{
		return new SavePointData
		{
			savePointKey = _savePointKey,
			savePointDescription = _savePointDescription,
			sceneName = _sceneName
		};
	}

	public static string Encode(string _savePointKey, string _savePointDescription, string _sceneName)
	{
		SavePointData savePointData = Create(_savePointKey, _savePointDescription, _sceneName);
		SaveData saveData = Object.FindObjectOfType<SaveData>();
		if ((Object)(object)saveData != (Object)null)
		{
			saveData.Encode(savePointData.SaveDataItems);
		}
		return JsonUtility.ToJson((object)savePointData, true);
	}

	public static void Decode(string saveDataJSON)
	{
		SavePointData savePointData = JsonUtility.FromJson<SavePointData>(saveDataJSON);
		UnityAction<Scene, LoadSceneMode> onSceneLoadedAction = null;
		onSceneLoadedAction = delegate(Scene scene, LoadSceneMode mode)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Invalid comparison between Unknown and I4
			if ((int)mode != 1 && !(((Scene)(ref scene)).name != savePointData.SceneName))
			{
				SceneManager.sceneLoaded -= onSceneLoadedAction;
				SaveData saveData = Object.FindObjectOfType<SaveData>();
				if ((Object)(object)saveData != (Object)null)
				{
					saveData.Decode(savePointData.SaveDataItems);
				}
				SaveManagerSignals.DoSavePointLoaded(savePointData.savePointKey);
			}
		};
		SceneManager.sceneLoaded += onSceneLoadedAction;
		SceneManager.LoadScene(savePointData.SceneName);
	}
}
