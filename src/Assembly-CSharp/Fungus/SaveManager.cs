using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

public class SaveManager : MonoBehaviour
{
	protected static SaveHistory saveHistory = new SaveHistory();

	protected Action loadAction;

	public static string STORAGE_DIRECTORY => Paths.GetSavePath() + "/FungusSaves/";

	public string StartScene { get; set; }

	public virtual int NumSavePoints => saveHistory.NumSavePoints;

	public virtual int NumRewoundSavePoints => saveHistory.NumRewoundSavePoints;

	private static string GetFullFilePath(string saveDataKey)
	{
		return STORAGE_DIRECTORY + saveDataKey + ".json";
	}

	protected virtual bool ReadSaveHistory(string saveDataKey)
	{
		string text = string.Empty;
		string fullFilePath = GetFullFilePath(saveDataKey);
		if (File.Exists(fullFilePath))
		{
			text = File.ReadAllText(fullFilePath);
		}
		if (!string.IsNullOrEmpty(text))
		{
			SaveHistory saveHistory = JsonUtility.FromJson<SaveHistory>(text);
			if (saveHistory != null)
			{
				SaveManager.saveHistory = saveHistory;
				return true;
			}
		}
		return false;
	}

	protected virtual bool WriteSaveHistory(string saveDataKey)
	{
		string text = JsonUtility.ToJson((object)saveHistory, true);
		if (!string.IsNullOrEmpty(text))
		{
			string fullFilePath = GetFullFilePath(saveDataKey);
			new FileInfo(fullFilePath).Directory.Create();
			File.WriteAllText(fullFilePath, text);
			return true;
		}
		return false;
	}

	protected virtual void ExecuteBlocks(string savePointKey)
	{
		SavePointLoaded.NotifyEventHandlers(savePointKey);
		SavePoint[] array = Object.FindObjectsOfType<SavePoint>();
		foreach (SavePoint savePoint in array)
		{
			if (savePoint.ResumeOnLoad && string.Compare(savePoint.SavePointKey, savePointKey, ignoreCase: true) == 0)
			{
				int commandIndex = savePoint.CommandIndex;
				Block parentBlock = savePoint.ParentBlock;
				savePoint.GetFlowchart().ExecuteBlock(parentBlock, commandIndex + 1);
				break;
			}
		}
	}

	protected virtual void ExecuteStartBlock()
	{
		SavePoint[] array = Object.FindObjectsOfType<SavePoint>();
		foreach (SavePoint savePoint in array)
		{
			if (savePoint.IsStartPoint)
			{
				savePoint.GetFlowchart().ExecuteBlock(savePoint.ParentBlock, savePoint.CommandIndex);
				break;
			}
		}
	}

	protected virtual void LoadSavedGame(string saveDataKey)
	{
		if (ReadSaveHistory(saveDataKey))
		{
			saveHistory.ClearRewoundSavePoints();
			saveHistory.LoadLatestSavePoint();
		}
	}

	protected virtual void OnEnable()
	{
		SaveManagerSignals.OnSavePointLoaded += OnSavePointLoaded;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	protected virtual void OnDisable()
	{
		SaveManagerSignals.OnSavePointLoaded -= OnSavePointLoaded;
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	protected virtual void OnSavePointLoaded(string savePointKey)
	{
		loadAction = delegate
		{
			ExecuteBlocks(savePointKey);
		};
	}

	protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		if ((int)mode != 1 && loadAction == null)
		{
			loadAction = ExecuteStartBlock;
		}
	}

	protected virtual void Start()
	{
		if (loadAction == null)
		{
			loadAction = ExecuteStartBlock;
		}
	}

	protected virtual void Update()
	{
		if (loadAction != null)
		{
			loadAction();
			loadAction = null;
		}
	}

	public virtual void Save(string saveDataKey)
	{
		WriteSaveHistory(saveDataKey);
	}

	public void Load(string saveDataKey)
	{
		loadAction = delegate
		{
			LoadSavedGame(saveDataKey);
		};
	}

	public void Delete(string saveDataKey)
	{
		string fullFilePath = GetFullFilePath(saveDataKey);
		if (File.Exists(fullFilePath))
		{
			File.Delete(fullFilePath);
		}
	}

	public bool SaveDataExists(string saveDataKey)
	{
		return File.Exists(GetFullFilePath(saveDataKey));
	}

	public virtual void AddSavePoint(string savePointKey, string savePointDescription)
	{
		saveHistory.AddSavePoint(savePointKey, savePointDescription);
		SaveManagerSignals.DoSavePointAdded(savePointKey, savePointDescription);
	}

	public virtual void Rewind()
	{
		if (saveHistory.NumSavePoints > 0)
		{
			if (saveHistory.NumSavePoints > 1)
			{
				saveHistory.Rewind();
			}
			saveHistory.LoadLatestSavePoint();
		}
	}

	public virtual void FastForward()
	{
		if (saveHistory.NumRewoundSavePoints > 0)
		{
			saveHistory.FastForward();
			saveHistory.LoadLatestSavePoint();
		}
	}

	public virtual void ClearHistory()
	{
		saveHistory.Clear();
	}

	public virtual string GetDebugInfo()
	{
		return saveHistory.GetDebugInfo();
	}
}
