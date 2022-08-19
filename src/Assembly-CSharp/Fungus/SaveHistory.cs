using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000ECA RID: 3786
	[Serializable]
	public class SaveHistory
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06006AE5 RID: 27365 RVA: 0x002947A2 File Offset: 0x002929A2
		public int NumSavePoints
		{
			get
			{
				return this.savePoints.Count;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06006AE6 RID: 27366 RVA: 0x002947AF File Offset: 0x002929AF
		public int NumRewoundSavePoints
		{
			get
			{
				return this.rewoundSavePoints.Count;
			}
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x002947BC File Offset: 0x002929BC
		public void AddSavePoint(string savePointKey, string savePointDescription)
		{
			this.ClearRewoundSavePoints();
			string name = SceneManager.GetActiveScene().name;
			string item = SavePointData.Encode(savePointKey, savePointDescription, name);
			this.savePoints.Add(item);
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x002947F4 File Offset: 0x002929F4
		public void Rewind()
		{
			if (this.savePoints.Count > 0)
			{
				this.rewoundSavePoints.Add(this.savePoints[this.savePoints.Count - 1]);
				this.savePoints.RemoveAt(this.savePoints.Count - 1);
			}
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x0029484C File Offset: 0x00292A4C
		public void FastForward()
		{
			if (this.rewoundSavePoints.Count > 0)
			{
				this.savePoints.Add(this.rewoundSavePoints[this.rewoundSavePoints.Count - 1]);
				this.rewoundSavePoints.RemoveAt(this.rewoundSavePoints.Count - 1);
			}
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x002948A2 File Offset: 0x00292AA2
		public void LoadLatestSavePoint()
		{
			if (this.savePoints.Count > 0)
			{
				SavePointData.Decode(this.savePoints[this.savePoints.Count - 1]);
			}
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x002948CF File Offset: 0x00292ACF
		public void Clear()
		{
			this.savePoints.Clear();
			this.rewoundSavePoints.Clear();
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x002948E7 File Offset: 0x00292AE7
		public void ClearRewoundSavePoints()
		{
			this.rewoundSavePoints.Clear();
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x002948F4 File Offset: 0x00292AF4
		public virtual string GetDebugInfo()
		{
			string text = "Save points:\n";
			foreach (string text2 in this.savePoints)
			{
				text = text + text2.Substring(0, text2.IndexOf(',')).Replace("\n", "").Replace("{", "").Replace("}", "") + "\n";
			}
			text += "Rewound points:\n";
			foreach (string text3 in this.rewoundSavePoints)
			{
				text = text + text3.Substring(0, text3.IndexOf(',')).Replace("\n", "").Replace("{", "").Replace("}", "") + "\n";
			}
			return text;
		}

		// Token: 0x04005A32 RID: 23090
		protected const int SaveDataVersion = 1;

		// Token: 0x04005A33 RID: 23091
		[SerializeField]
		protected int version = 1;

		// Token: 0x04005A34 RID: 23092
		[SerializeField]
		protected List<string> savePoints = new List<string>();

		// Token: 0x04005A35 RID: 23093
		[SerializeField]
		protected List<string> rewoundSavePoints = new List<string>();
	}
}
