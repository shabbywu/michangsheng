using UnityEngine;

namespace Fungus;

public class NarrativeLog : MonoBehaviour
{
	public delegate void NarrativeAddedHandler();

	private NarrativeData history;

	public static event NarrativeAddedHandler OnNarrativeAdded;

	public static void DoNarrativeAdded()
	{
		if (NarrativeLog.OnNarrativeAdded != null)
		{
			NarrativeLog.OnNarrativeAdded();
		}
	}

	protected virtual void Awake()
	{
		history = new NarrativeData();
	}

	protected virtual void OnEnable()
	{
		WriterSignals.OnWriterState += OnWriterState;
	}

	protected virtual void OnDisable()
	{
		WriterSignals.OnWriterState -= OnWriterState;
	}

	protected virtual void OnWriterState(Writer writer, WriterState writerState)
	{
		if (writerState == WriterState.End)
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			string nameText = sayDialog.NameText;
			string storyText = sayDialog.StoryText;
			AddLine(nameText, storyText);
		}
	}

	public void AddLine(string name, string text)
	{
		Line line = new Line();
		line.name = name;
		line.text = text;
		history.lines.Add(line);
		DoNarrativeAdded();
	}

	public void Clear()
	{
		history.lines.Clear();
	}

	public string GetJsonHistory()
	{
		return JsonUtility.ToJson((object)history, true);
	}

	public string GetPrettyHistory(bool previousOnly = false)
	{
		string text = "\n ";
		int num = (previousOnly ? (history.lines.Count - 1) : history.lines.Count);
		for (int i = 0; i < num; i++)
		{
			text = text + "<b>" + history.lines[i].name + "</b>\n";
			text = text + history.lines[i].text + "\n\n";
		}
		return text;
	}

	public void LoadHistory(string narrativeData)
	{
		if (narrativeData == null)
		{
			Debug.LogError((object)"Failed to decode History save data item");
		}
		else
		{
			history = JsonUtility.FromJson<NarrativeData>(narrativeData);
		}
	}
}
