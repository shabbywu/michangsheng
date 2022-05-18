using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02001369 RID: 4969
	[Serializable]
	public class SavePointData
	{
		// Token: 0x0600788A RID: 30858 RVA: 0x00051E46 File Offset: 0x00050046
		protected static SavePointData Create(string _savePointKey, string _savePointDescription, string _sceneName)
		{
			return new SavePointData
			{
				savePointKey = _savePointKey,
				savePointDescription = _savePointDescription,
				sceneName = _sceneName
			};
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x00051E62 File Offset: 0x00050062
		// (set) Token: 0x0600788C RID: 30860 RVA: 0x00051E6A File Offset: 0x0005006A
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

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600788D RID: 30861 RVA: 0x00051E73 File Offset: 0x00050073
		// (set) Token: 0x0600788E RID: 30862 RVA: 0x00051E7B File Offset: 0x0005007B
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

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600788F RID: 30863 RVA: 0x00051E84 File Offset: 0x00050084
		// (set) Token: 0x06007890 RID: 30864 RVA: 0x00051E8C File Offset: 0x0005008C
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

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06007891 RID: 30865 RVA: 0x00051E95 File Offset: 0x00050095
		public List<SaveDataItem> SaveDataItems
		{
			get
			{
				return this.saveDataItems;
			}
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x002B6D10 File Offset: 0x002B4F10
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

		// Token: 0x06007893 RID: 30867 RVA: 0x002B6D48 File Offset: 0x002B4F48
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

		// Token: 0x04006895 RID: 26773
		[SerializeField]
		protected string savePointKey;

		// Token: 0x04006896 RID: 26774
		[SerializeField]
		protected string savePointDescription;

		// Token: 0x04006897 RID: 26775
		[SerializeField]
		protected string sceneName;

		// Token: 0x04006898 RID: 26776
		[SerializeField]
		protected List<SaveDataItem> saveDataItems = new List<SaveDataItem>();
	}
}
