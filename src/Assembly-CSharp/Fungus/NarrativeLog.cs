using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012E2 RID: 4834
	public class NarrativeLog : MonoBehaviour
	{
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060075AC RID: 30124 RVA: 0x002B0BE0 File Offset: 0x002AEDE0
		// (remove) Token: 0x060075AD RID: 30125 RVA: 0x002B0C14 File Offset: 0x002AEE14
		public static event NarrativeLog.NarrativeAddedHandler OnNarrativeAdded;

		// Token: 0x060075AE RID: 30126 RVA: 0x000502EB File Offset: 0x0004E4EB
		public static void DoNarrativeAdded()
		{
			if (NarrativeLog.OnNarrativeAdded != null)
			{
				NarrativeLog.OnNarrativeAdded();
			}
		}

		// Token: 0x060075AF RID: 30127 RVA: 0x000502FE File Offset: 0x0004E4FE
		protected virtual void Awake()
		{
			this.history = new NarrativeData();
		}

		// Token: 0x060075B0 RID: 30128 RVA: 0x0005030B File Offset: 0x0004E50B
		protected virtual void OnEnable()
		{
			WriterSignals.OnWriterState += this.OnWriterState;
		}

		// Token: 0x060075B1 RID: 30129 RVA: 0x0005031F File Offset: 0x0004E51F
		protected virtual void OnDisable()
		{
			WriterSignals.OnWriterState -= this.OnWriterState;
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x002B0C48 File Offset: 0x002AEE48
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

		// Token: 0x060075B3 RID: 30131 RVA: 0x002B0C74 File Offset: 0x002AEE74
		public void AddLine(string name, string text)
		{
			Line line = new Line();
			line.name = name;
			line.text = text;
			this.history.lines.Add(line);
			NarrativeLog.DoNarrativeAdded();
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x00050333 File Offset: 0x0004E533
		public void Clear()
		{
			this.history.lines.Clear();
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x00050345 File Offset: 0x0004E545
		public string GetJsonHistory()
		{
			return JsonUtility.ToJson(this.history, true);
		}

		// Token: 0x060075B6 RID: 30134 RVA: 0x002B0CAC File Offset: 0x002AEEAC
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

		// Token: 0x060075B7 RID: 30135 RVA: 0x00050353 File Offset: 0x0004E553
		public void LoadHistory(string narrativeData)
		{
			if (narrativeData == null)
			{
				Debug.LogError("Failed to decode History save data item");
				return;
			}
			this.history = JsonUtility.FromJson<NarrativeData>(narrativeData);
		}

		// Token: 0x040066C3 RID: 26307
		private NarrativeData history;

		// Token: 0x020012E3 RID: 4835
		// (Invoke) Token: 0x060075BA RID: 30138
		public delegate void NarrativeAddedHandler();
	}
}
