using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000E81 RID: 3713
	public class SaveManager : MonoBehaviour
	{
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600690C RID: 26892 RVA: 0x0028FB2D File Offset: 0x0028DD2D
		public static string STORAGE_DIRECTORY
		{
			get
			{
				return Paths.GetSavePath() + "/FungusSaves/";
			}
		}

		// Token: 0x0600690D RID: 26893 RVA: 0x0028FB3E File Offset: 0x0028DD3E
		private static string GetFullFilePath(string saveDataKey)
		{
			return SaveManager.STORAGE_DIRECTORY + saveDataKey + ".json";
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x0028FB50 File Offset: 0x0028DD50
		protected virtual bool ReadSaveHistory(string saveDataKey)
		{
			string text = string.Empty;
			string fullFilePath = SaveManager.GetFullFilePath(saveDataKey);
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

		// Token: 0x0600690F RID: 26895 RVA: 0x0028FB94 File Offset: 0x0028DD94
		protected virtual bool WriteSaveHistory(string saveDataKey)
		{
			string text = JsonUtility.ToJson(SaveManager.saveHistory, true);
			if (!string.IsNullOrEmpty(text))
			{
				string fullFilePath = SaveManager.GetFullFilePath(saveDataKey);
				new FileInfo(fullFilePath).Directory.Create();
				File.WriteAllText(fullFilePath, text);
				return true;
			}
			return false;
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x0028FBD4 File Offset: 0x0028DDD4
		protected virtual void ExecuteBlocks(string savePointKey)
		{
			SavePointLoaded.NotifyEventHandlers(savePointKey);
			foreach (SavePoint savePoint in Object.FindObjectsOfType<SavePoint>())
			{
				if (savePoint.ResumeOnLoad && string.Compare(savePoint.SavePointKey, savePointKey, true) == 0)
				{
					int commandIndex = savePoint.CommandIndex;
					Block parentBlock = savePoint.ParentBlock;
					savePoint.GetFlowchart().ExecuteBlock(parentBlock, commandIndex + 1, null);
					return;
				}
			}
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0028FC38 File Offset: 0x0028DE38
		protected virtual void ExecuteStartBlock()
		{
			foreach (SavePoint savePoint in Object.FindObjectsOfType<SavePoint>())
			{
				if (savePoint.IsStartPoint)
				{
					savePoint.GetFlowchart().ExecuteBlock(savePoint.ParentBlock, savePoint.CommandIndex, null);
					return;
				}
			}
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0028FC7F File Offset: 0x0028DE7F
		protected virtual void LoadSavedGame(string saveDataKey)
		{
			if (this.ReadSaveHistory(saveDataKey))
			{
				SaveManager.saveHistory.ClearRewoundSavePoints();
				SaveManager.saveHistory.LoadLatestSavePoint();
			}
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x0028FC9E File Offset: 0x0028DE9E
		protected virtual void OnEnable()
		{
			SaveManagerSignals.OnSavePointLoaded += this.OnSavePointLoaded;
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x0028FCC4 File Offset: 0x0028DEC4
		protected virtual void OnDisable()
		{
			SaveManagerSignals.OnSavePointLoaded -= this.OnSavePointLoaded;
			SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x0028FCEC File Offset: 0x0028DEEC
		protected virtual void OnSavePointLoaded(string savePointKey)
		{
			this.loadAction = delegate()
			{
				this.ExecuteBlocks(savePointKey);
			};
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x0028FD1F File Offset: 0x0028DF1F
		protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (mode == 1)
			{
				return;
			}
			if (this.loadAction == null)
			{
				this.loadAction = new Action(this.ExecuteStartBlock);
			}
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x0028FD41 File Offset: 0x0028DF41
		protected virtual void Start()
		{
			if (this.loadAction == null)
			{
				this.loadAction = new Action(this.ExecuteStartBlock);
			}
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x0028FD5E File Offset: 0x0028DF5E
		protected virtual void Update()
		{
			if (this.loadAction != null)
			{
				this.loadAction();
				this.loadAction = null;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06006919 RID: 26905 RVA: 0x0028FD7A File Offset: 0x0028DF7A
		// (set) Token: 0x0600691A RID: 26906 RVA: 0x0028FD82 File Offset: 0x0028DF82
		public string StartScene { get; set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600691B RID: 26907 RVA: 0x0028FD8B File Offset: 0x0028DF8B
		public virtual int NumSavePoints
		{
			get
			{
				return SaveManager.saveHistory.NumSavePoints;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600691C RID: 26908 RVA: 0x0028FD97 File Offset: 0x0028DF97
		public virtual int NumRewoundSavePoints
		{
			get
			{
				return SaveManager.saveHistory.NumRewoundSavePoints;
			}
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x0028FDA3 File Offset: 0x0028DFA3
		public virtual void Save(string saveDataKey)
		{
			this.WriteSaveHistory(saveDataKey);
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x0028FDB0 File Offset: 0x0028DFB0
		public void Load(string saveDataKey)
		{
			this.loadAction = delegate()
			{
				this.LoadSavedGame(saveDataKey);
			};
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x0028FDE4 File Offset: 0x0028DFE4
		public void Delete(string saveDataKey)
		{
			string fullFilePath = SaveManager.GetFullFilePath(saveDataKey);
			if (File.Exists(fullFilePath))
			{
				File.Delete(fullFilePath);
			}
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x0028FE06 File Offset: 0x0028E006
		public bool SaveDataExists(string saveDataKey)
		{
			return File.Exists(SaveManager.GetFullFilePath(saveDataKey));
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x0028FE13 File Offset: 0x0028E013
		public virtual void AddSavePoint(string savePointKey, string savePointDescription)
		{
			SaveManager.saveHistory.AddSavePoint(savePointKey, savePointDescription);
			SaveManagerSignals.DoSavePointAdded(savePointKey, savePointDescription);
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x0028FE28 File Offset: 0x0028E028
		public virtual void Rewind()
		{
			if (SaveManager.saveHistory.NumSavePoints > 0)
			{
				if (SaveManager.saveHistory.NumSavePoints > 1)
				{
					SaveManager.saveHistory.Rewind();
				}
				SaveManager.saveHistory.LoadLatestSavePoint();
			}
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x0028FE58 File Offset: 0x0028E058
		public virtual void FastForward()
		{
			if (SaveManager.saveHistory.NumRewoundSavePoints > 0)
			{
				SaveManager.saveHistory.FastForward();
				SaveManager.saveHistory.LoadLatestSavePoint();
			}
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x0028FE7B File Offset: 0x0028E07B
		public virtual void ClearHistory()
		{
			SaveManager.saveHistory.Clear();
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x0028FE87 File Offset: 0x0028E087
		public virtual string GetDebugInfo()
		{
			return SaveManager.saveHistory.GetDebugInfo();
		}

		// Token: 0x04005921 RID: 22817
		protected static SaveHistory saveHistory = new SaveHistory();

		// Token: 0x04005922 RID: 22818
		protected Action loadAction;
	}
}
