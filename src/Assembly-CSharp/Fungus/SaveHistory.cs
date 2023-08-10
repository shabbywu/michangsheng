using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

[Serializable]
public class SaveHistory
{
	protected const int SaveDataVersion = 1;

	[SerializeField]
	protected int version = 1;

	[SerializeField]
	protected List<string> savePoints = new List<string>();

	[SerializeField]
	protected List<string> rewoundSavePoints = new List<string>();

	public int NumSavePoints => savePoints.Count;

	public int NumRewoundSavePoints => rewoundSavePoints.Count;

	public void AddSavePoint(string savePointKey, string savePointDescription)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		ClearRewoundSavePoints();
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		string item = SavePointData.Encode(savePointKey, savePointDescription, name);
		savePoints.Add(item);
	}

	public void Rewind()
	{
		if (savePoints.Count > 0)
		{
			rewoundSavePoints.Add(savePoints[savePoints.Count - 1]);
			savePoints.RemoveAt(savePoints.Count - 1);
		}
	}

	public void FastForward()
	{
		if (rewoundSavePoints.Count > 0)
		{
			savePoints.Add(rewoundSavePoints[rewoundSavePoints.Count - 1]);
			rewoundSavePoints.RemoveAt(rewoundSavePoints.Count - 1);
		}
	}

	public void LoadLatestSavePoint()
	{
		if (savePoints.Count > 0)
		{
			SavePointData.Decode(savePoints[savePoints.Count - 1]);
		}
	}

	public void Clear()
	{
		savePoints.Clear();
		rewoundSavePoints.Clear();
	}

	public void ClearRewoundSavePoints()
	{
		rewoundSavePoints.Clear();
	}

	public virtual string GetDebugInfo()
	{
		string text = "Save points:\n";
		foreach (string savePoint in savePoints)
		{
			text = text + savePoint.Substring(0, savePoint.IndexOf(',')).Replace("\n", "").Replace("{", "")
				.Replace("}", "") + "\n";
		}
		text += "Rewound points:\n";
		foreach (string rewoundSavePoint in rewoundSavePoints)
		{
			text = text + rewoundSavePoint.Substring(0, rewoundSavePoint.IndexOf(',')).Replace("\n", "").Replace("{", "")
				.Replace("}", "") + "\n";
		}
		return text;
	}
}
