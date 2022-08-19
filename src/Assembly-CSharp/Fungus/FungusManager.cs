using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E71 RID: 3697
	[RequireComponent(typeof(CameraManager))]
	[RequireComponent(typeof(MusicManager))]
	[RequireComponent(typeof(EventDispatcher))]
	[RequireComponent(typeof(GlobalVariables))]
	[RequireComponent(typeof(SaveManager))]
	[RequireComponent(typeof(NarrativeLog))]
	public sealed class FungusManager : MonoBehaviour
	{
		// Token: 0x06006878 RID: 26744 RVA: 0x0028D1E8 File Offset: 0x0028B3E8
		private void Awake()
		{
			this.CameraManager = base.GetComponent<CameraManager>();
			this.MusicManager = base.GetComponent<MusicManager>();
			this.EventDispatcher = base.GetComponent<EventDispatcher>();
			this.GlobalVariables = base.GetComponent<GlobalVariables>();
			this.SaveManager = base.GetComponent<SaveManager>();
			this.NarrativeLog = base.GetComponent<NarrativeLog>();
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x0028D23D File Offset: 0x0028B43D
		private void OnDestroy()
		{
			FungusManager.applicationIsQuitting = true;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x0028D245 File Offset: 0x0028B445
		// (set) Token: 0x0600687B RID: 26747 RVA: 0x0028D24D File Offset: 0x0028B44D
		public CameraManager CameraManager { get; private set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x0028D256 File Offset: 0x0028B456
		// (set) Token: 0x0600687D RID: 26749 RVA: 0x0028D25E File Offset: 0x0028B45E
		public MusicManager MusicManager { get; private set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600687E RID: 26750 RVA: 0x0028D267 File Offset: 0x0028B467
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x0028D26F File Offset: 0x0028B46F
		public EventDispatcher EventDispatcher { get; private set; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x0028D278 File Offset: 0x0028B478
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x0028D280 File Offset: 0x0028B480
		public GlobalVariables GlobalVariables { get; private set; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x0028D289 File Offset: 0x0028B489
		// (set) Token: 0x06006883 RID: 26755 RVA: 0x0028D291 File Offset: 0x0028B491
		public SaveManager SaveManager { get; private set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x0028D29A File Offset: 0x0028B49A
		// (set) Token: 0x06006885 RID: 26757 RVA: 0x0028D2A2 File Offset: 0x0028B4A2
		public NarrativeLog NarrativeLog { get; private set; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06006886 RID: 26758 RVA: 0x0028D2AC File Offset: 0x0028B4AC
		public static FungusManager Instance
		{
			get
			{
				if (FungusManager.applicationIsQuitting)
				{
					Debug.LogWarning("FungusManager.Instance() was called while application is quitting. Returning null instead.");
					return null;
				}
				object @lock = FungusManager._lock;
				FungusManager result;
				lock (@lock)
				{
					if (FungusManager.instance == null)
					{
						GameObject gameObject = new GameObject();
						gameObject.name = "FungusManager";
						Object.DontDestroyOnLoad(gameObject);
						FungusManager.instance = gameObject.AddComponent<FungusManager>();
					}
					result = FungusManager.instance;
				}
				return result;
			}
		}

		// Token: 0x040058DE RID: 22750
		private static FungusManager instance;

		// Token: 0x040058DF RID: 22751
		private static bool applicationIsQuitting = false;

		// Token: 0x040058E0 RID: 22752
		public Flowchart jieShaBlock;

		// Token: 0x040058E1 RID: 22753
		private static object _lock = new object();
	}
}
