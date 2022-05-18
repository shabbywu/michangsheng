using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x020012EE RID: 4846
	public class SaveManager : MonoBehaviour
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060075FB RID: 30203 RVA: 0x00050570 File Offset: 0x0004E770
		public static string STORAGE_DIRECTORY
		{
			get
			{
				return Paths.GetSavePath() + "/FungusSaves/";
			}
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x00050581 File Offset: 0x0004E781
		private static string GetFullFilePath(string saveDataKey)
		{
			return SaveManager.STORAGE_DIRECTORY + saveDataKey + ".json";
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x002B20C4 File Offset: 0x002B02C4
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

		// Token: 0x060075FE RID: 30206 RVA: 0x002B2108 File Offset: 0x002B0308
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

		// Token: 0x060075FF RID: 30207 RVA: 0x002B2148 File Offset: 0x002B0348
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

		// Token: 0x06007600 RID: 30208 RVA: 0x002B21AC File Offset: 0x002B03AC
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

		// Token: 0x06007601 RID: 30209 RVA: 0x00050593 File Offset: 0x0004E793
		protected virtual void LoadSavedGame(string saveDataKey)
		{
			if (this.ReadSaveHistory(saveDataKey))
			{
				SaveManager.saveHistory.ClearRewoundSavePoints();
				SaveManager.saveHistory.LoadLatestSavePoint();
			}
		}

		// Token: 0x06007602 RID: 30210 RVA: 0x000505B2 File Offset: 0x0004E7B2
		protected virtual void OnEnable()
		{
			SaveManagerSignals.OnSavePointLoaded += this.OnSavePointLoaded;
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x06007603 RID: 30211 RVA: 0x000505D8 File Offset: 0x0004E7D8
		protected virtual void OnDisable()
		{
			SaveManagerSignals.OnSavePointLoaded -= this.OnSavePointLoaded;
			SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x002B21F4 File Offset: 0x002B03F4
		protected virtual void OnSavePointLoaded(string savePointKey)
		{
			this.loadAction = delegate()
			{
				this.ExecuteBlocks(savePointKey);
			};
		}

		// Token: 0x06007605 RID: 30213 RVA: 0x000505FE File Offset: 0x0004E7FE
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

		// Token: 0x06007606 RID: 30214 RVA: 0x00050620 File Offset: 0x0004E820
		protected virtual void Start()
		{
			if (this.loadAction == null)
			{
				this.loadAction = new Action(this.ExecuteStartBlock);
			}
		}

		// Token: 0x06007607 RID: 30215 RVA: 0x0005063D File Offset: 0x0004E83D
		protected virtual void Update()
		{
			if (this.loadAction != null)
			{
				this.loadAction();
				this.loadAction = null;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06007608 RID: 30216 RVA: 0x00050659 File Offset: 0x0004E859
		// (set) Token: 0x06007609 RID: 30217 RVA: 0x00050661 File Offset: 0x0004E861
		public string StartScene { get; set; }

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600760A RID: 30218 RVA: 0x0005066A File Offset: 0x0004E86A
		public virtual int NumSavePoints
		{
			get
			{
				return SaveManager.saveHistory.NumSavePoints;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600760B RID: 30219 RVA: 0x00050676 File Offset: 0x0004E876
		public virtual int NumRewoundSavePoints
		{
			get
			{
				return SaveManager.saveHistory.NumRewoundSavePoints;
			}
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x00050682 File Offset: 0x0004E882
		public virtual void Save(string saveDataKey)
		{
			this.WriteSaveHistory(saveDataKey);
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x002B2228 File Offset: 0x002B0428
		public void Load(string saveDataKey)
		{
			this.loadAction = delegate()
			{
				this.LoadSavedGame(saveDataKey);
			};
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x002B225C File Offset: 0x002B045C
		public void Delete(string saveDataKey)
		{
			string fullFilePath = SaveManager.GetFullFilePath(saveDataKey);
			if (File.Exists(fullFilePath))
			{
				File.Delete(fullFilePath);
			}
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x0005068C File Offset: 0x0004E88C
		public bool SaveDataExists(string saveDataKey)
		{
			return File.Exists(SaveManager.GetFullFilePath(saveDataKey));
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x00050699 File Offset: 0x0004E899
		public virtual void AddSavePoint(string savePointKey, string savePointDescription)
		{
			SaveManager.saveHistory.AddSavePoint(savePointKey, savePointDescription);
			SaveManagerSignals.DoSavePointAdded(savePointKey, savePointDescription);
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x000506AE File Offset: 0x0004E8AE
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

		// Token: 0x06007612 RID: 30226 RVA: 0x000506DE File Offset: 0x0004E8DE
		public virtual void FastForward()
		{
			if (SaveManager.saveHistory.NumRewoundSavePoints > 0)
			{
				SaveManager.saveHistory.FastForward();
				SaveManager.saveHistory.LoadLatestSavePoint();
			}
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x00050701 File Offset: 0x0004E901
		public virtual void ClearHistory()
		{
			SaveManager.saveHistory.Clear();
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x0005070D File Offset: 0x0004E90D
		public virtual string GetDebugInfo()
		{
			return SaveManager.saveHistory.GetDebugInfo();
		}

		// Token: 0x040066EF RID: 26351
		protected static SaveHistory saveHistory = new SaveHistory();

		// Token: 0x040066F0 RID: 26352
		protected Action loadAction;
	}
}
