using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02001368 RID: 4968
	[Serializable]
	public class SaveHistory
	{
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06007880 RID: 30848 RVA: 0x00051DB5 File Offset: 0x0004FFB5
		public int NumSavePoints
		{
			get
			{
				return this.savePoints.Count;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x00051DC2 File Offset: 0x0004FFC2
		public int NumRewoundSavePoints
		{
			get
			{
				return this.rewoundSavePoints.Count;
			}
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x002B6AFC File Offset: 0x002B4CFC
		public void AddSavePoint(string savePointKey, string savePointDescription)
		{
			this.ClearRewoundSavePoints();
			string name = SceneManager.GetActiveScene().name;
			string item = SavePointData.Encode(savePointKey, savePointDescription, name);
			this.savePoints.Add(item);
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x002B6B34 File Offset: 0x002B4D34
		public void Rewind()
		{
			if (this.savePoints.Count > 0)
			{
				this.rewoundSavePoints.Add(this.savePoints[this.savePoints.Count - 1]);
				this.savePoints.RemoveAt(this.savePoints.Count - 1);
			}
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x002B6B8C File Offset: 0x002B4D8C
		public void FastForward()
		{
			if (this.rewoundSavePoints.Count > 0)
			{
				this.savePoints.Add(this.rewoundSavePoints[this.rewoundSavePoints.Count - 1]);
				this.rewoundSavePoints.RemoveAt(this.rewoundSavePoints.Count - 1);
			}
		}

		// Token: 0x06007885 RID: 30853 RVA: 0x00051DCF File Offset: 0x0004FFCF
		public void LoadLatestSavePoint()
		{
			if (this.savePoints.Count > 0)
			{
				SavePointData.Decode(this.savePoints[this.savePoints.Count - 1]);
			}
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x00051DFC File Offset: 0x0004FFFC
		public void Clear()
		{
			this.savePoints.Clear();
			this.rewoundSavePoints.Clear();
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x00051E14 File Offset: 0x00050014
		public void ClearRewoundSavePoints()
		{
			this.rewoundSavePoints.Clear();
		}

		// Token: 0x06007888 RID: 30856 RVA: 0x002B6BE4 File Offset: 0x002B4DE4
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

		// Token: 0x04006891 RID: 26769
		protected const int SaveDataVersion = 1;

		// Token: 0x04006892 RID: 26770
		[SerializeField]
		protected int version = 1;

		// Token: 0x04006893 RID: 26771
		[SerializeField]
		protected List<string> savePoints = new List<string>();

		// Token: 0x04006894 RID: 26772
		[SerializeField]
		protected List<string> rewoundSavePoints = new List<string>();
	}
}
