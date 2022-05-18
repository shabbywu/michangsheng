using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012CF RID: 4815
	[RequireComponent(typeof(CameraManager))]
	[RequireComponent(typeof(MusicManager))]
	[RequireComponent(typeof(EventDispatcher))]
	[RequireComponent(typeof(GlobalVariables))]
	[RequireComponent(typeof(SaveManager))]
	[RequireComponent(typeof(NarrativeLog))]
	public sealed class FungusManager : MonoBehaviour
	{
		// Token: 0x06007531 RID: 30001 RVA: 0x002AF828 File Offset: 0x002ADA28
		private void Awake()
		{
			this.CameraManager = base.GetComponent<CameraManager>();
			this.MusicManager = base.GetComponent<MusicManager>();
			this.EventDispatcher = base.GetComponent<EventDispatcher>();
			this.GlobalVariables = base.GetComponent<GlobalVariables>();
			this.SaveManager = base.GetComponent<SaveManager>();
			this.NarrativeLog = base.GetComponent<NarrativeLog>();
		}

		// Token: 0x06007532 RID: 30002 RVA: 0x0004FF08 File Offset: 0x0004E108
		private void OnDestroy()
		{
			FungusManager.applicationIsQuitting = true;
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06007533 RID: 30003 RVA: 0x0004FF10 File Offset: 0x0004E110
		// (set) Token: 0x06007534 RID: 30004 RVA: 0x0004FF18 File Offset: 0x0004E118
		public CameraManager CameraManager { get; private set; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06007535 RID: 30005 RVA: 0x0004FF21 File Offset: 0x0004E121
		// (set) Token: 0x06007536 RID: 30006 RVA: 0x0004FF29 File Offset: 0x0004E129
		public MusicManager MusicManager { get; private set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06007537 RID: 30007 RVA: 0x0004FF32 File Offset: 0x0004E132
		// (set) Token: 0x06007538 RID: 30008 RVA: 0x0004FF3A File Offset: 0x0004E13A
		public EventDispatcher EventDispatcher { get; private set; }

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06007539 RID: 30009 RVA: 0x0004FF43 File Offset: 0x0004E143
		// (set) Token: 0x0600753A RID: 30010 RVA: 0x0004FF4B File Offset: 0x0004E14B
		public GlobalVariables GlobalVariables { get; private set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600753B RID: 30011 RVA: 0x0004FF54 File Offset: 0x0004E154
		// (set) Token: 0x0600753C RID: 30012 RVA: 0x0004FF5C File Offset: 0x0004E15C
		public SaveManager SaveManager { get; private set; }

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600753D RID: 30013 RVA: 0x0004FF65 File Offset: 0x0004E165
		// (set) Token: 0x0600753E RID: 30014 RVA: 0x0004FF6D File Offset: 0x0004E16D
		public NarrativeLog NarrativeLog { get; private set; }

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600753F RID: 30015 RVA: 0x002AF880 File Offset: 0x002ADA80
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

		// Token: 0x0400667C RID: 26236
		private static FungusManager instance;

		// Token: 0x0400667D RID: 26237
		private static bool applicationIsQuitting = false;

		// Token: 0x0400667E RID: 26238
		public Flowchart jieShaBlock;

		// Token: 0x0400667F RID: 26239
		private static object _lock = new object();
	}
}
