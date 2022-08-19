using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000ECB RID: 3787
	[Serializable]
	public class SavePointData
	{
		// Token: 0x06006AEF RID: 27375 RVA: 0x00294A45 File Offset: 0x00292C45
		protected static SavePointData Create(string _savePointKey, string _savePointDescription, string _sceneName)
		{
			return new SavePointData
			{
				savePointKey = _savePointKey,
				savePointDescription = _savePointDescription,
				sceneName = _sceneName
			};
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06006AF0 RID: 27376 RVA: 0x00294A61 File Offset: 0x00292C61
		// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x00294A69 File Offset: 0x00292C69
		public string SavePointKey
		{
			get
			{
				return this.savePointKey;
			}
			set
			{
				this.savePointKey = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x00294A72 File Offset: 0x00292C72
		// (set) Token: 0x06006AF3 RID: 27379 RVA: 0x00294A7A File Offset: 0x00292C7A
		public string SavePointDescription
		{
			get
			{
				return this.savePointDescription;
			}
			set
			{
				this.savePointDescription = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06006AF4 RID: 27380 RVA: 0x00294A83 File Offset: 0x00292C83
		// (set) Token: 0x06006AF5 RID: 27381 RVA: 0x00294A8B File Offset: 0x00292C8B
		public string SceneName
		{
			get
			{
				return this.sceneName;
			}
			set
			{
				this.sceneName = value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06006AF6 RID: 27382 RVA: 0x00294A94 File Offset: 0x00292C94
		public List<SaveDataItem> SaveDataItems
		{
			get
			{
				return this.saveDataItems;
			}
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x00294A9C File Offset: 0x00292C9C
		public static string Encode(string _savePointKey, string _savePointDescription, string _sceneName)
		{
			SavePointData savePointData = SavePointData.Create(_savePointKey, _savePointDescription, _sceneName);
			SaveData saveData = Object.FindObjectOfType<SaveData>();
			if (saveData != null)
			{
				saveData.Encode(savePointData.SaveDataItems);
			}
			return JsonUtility.ToJson(savePointData, true);
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x00294AD4 File Offset: 0x00292CD4
		public static void Decode(string saveDataJSON)
		{
			SavePointData savePointData = JsonUtility.FromJson<SavePointData>(saveDataJSON);
			UnityAction<Scene, LoadSceneMode> onSceneLoadedAction = null;
			onSceneLoadedAction = delegate(Scene scene, LoadSceneMode mode)
			{
				if (mode == 1 || scene.name != savePointData.SceneName)
				{
					return;
				}
				SceneManager.sceneLoaded -= onSceneLoadedAction;
				SaveData saveData = Object.FindObjectOfType<SaveData>();
				if (saveData != null)
				{
					saveData.Decode(savePointData.SaveDataItems);
				}
				SaveManagerSignals.DoSavePointLoaded(savePointData.savePointKey);
			};
			SceneManager.sceneLoaded += onSceneLoadedAction;
			SceneManager.LoadScene(savePointData.SceneName);
		}

		// Token: 0x04005A36 RID: 23094
		[SerializeField]
		protected string savePointKey;

		// Token: 0x04005A37 RID: 23095
		[SerializeField]
		protected string savePointDescription;

		// Token: 0x04005A38 RID: 23096
		[SerializeField]
		protected string sceneName;

		// Token: 0x04005A39 RID: 23097
		[SerializeField]
		protected List<SaveDataItem> saveDataItems = new List<SaveDataItem>();
	}
}
