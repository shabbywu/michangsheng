using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E78 RID: 3704
	public class NarrativeLog : MonoBehaviour
	{
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060068C9 RID: 26825 RVA: 0x0028E498 File Offset: 0x0028C698
		// (remove) Token: 0x060068CA RID: 26826 RVA: 0x0028E4CC File Offset: 0x0028C6CC
		public static event NarrativeLog.NarrativeAddedHandler OnNarrativeAdded;

		// Token: 0x060068CB RID: 26827 RVA: 0x0028E4FF File Offset: 0x0028C6FF
		public static void DoNarrativeAdded()
		{
			if (NarrativeLog.OnNarrativeAdded != null)
			{
				NarrativeLog.OnNarrativeAdded();
			}
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x0028E512 File Offset: 0x0028C712
		protected virtual void Awake()
		{
			this.history = new NarrativeData();
		}

		// Token: 0x060068CD RID: 26829 RVA: 0x0028E51F File Offset: 0x0028C71F
		protected virtual void OnEnable()
		{
			WriterSignals.OnWriterState += this.OnWriterState;
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x0028E533 File Offset: 0x0028C733
		protected virtual void OnDisable()
		{
			WriterSignals.OnWriterState -= this.OnWriterState;
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x0028E548 File Offset: 0x0028C748
		protected virtual void OnWriterState(Writer writer, WriterState writerState)
		{
			if (writerState == WriterState.End)
			{
				SayDialog sayDialog = SayDialog.GetSayDialog();
				string nameText = sayDialog.NameText;
				string storyText = sayDialog.StoryText;
				this.AddLine(nameText, storyText);
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x0028E574 File Offset: 0x0028C774
		public void AddLine(string name, string text)
		{
			Line line = new Line();
			line.name = name;
			line.text = text;
			this.history.lines.Add(line);
			NarrativeLog.DoNarrativeAdded();
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x0028E5AB File Offset: 0x0028C7AB
		public void Clear()
		{
			this.history.lines.Clear();
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x0028E5BD File Offset: 0x0028C7BD
		public string GetJsonHistory()
		{
			return JsonUtility.ToJson(this.history, true);
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x0028E5CC File Offset: 0x0028C7CC
		public string GetPrettyHistory(bool previousOnly = false)
		{
			string text = "\n ";
			int num = previousOnly ? (this.history.lines.Count - 1) : this.history.lines.Count;
			for (int i = 0; i < num; i++)
			{
				text = text + "<b>" + this.history.lines[i].name + "</b>\n";
				text = text + this.history.lines[i].text + "\n\n";
			}
			return text;
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x0028E65D File Offset: 0x0028C85D
		public void LoadHistory(string narrativeData)
		{
			if (narrativeData == null)
			{
				Debug.LogError("Failed to decode History save data item");
				return;
			}
			this.history = JsonUtility.FromJson<NarrativeData>(narrativeData);
		}

		// Token: 0x040058FB RID: 22779
		private NarrativeData history;

		// Token: 0x020016DE RID: 5854
		// (Invoke) Token: 0x0600884E RID: 34894
		public delegate void NarrativeAddedHandler();
	}
}
