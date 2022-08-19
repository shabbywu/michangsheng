using System;
using UltimateSurvival.InputSystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200059A RID: 1434
	public class GameController : MonoBehaviour
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x0015650A File Offset: 0x0015470A
		public static PlayerEventHandler LocalPlayer
		{
			get
			{
				if (GameController.m_Player == null)
				{
					GameController.m_Player = Object.FindObjectOfType<PlayerEventHandler>();
				}
				return GameController.m_Player;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x00156528 File Offset: 0x00154728
		public static MGInputManager InputManager
		{
			get
			{
				if (GameController.m_InputManager == null)
				{
					GameController.m_InputManager = Object.FindObjectOfType<MGInputManager>();
				}
				return GameController.m_InputManager;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x00156546 File Offset: 0x00154746
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x0015654D File Offset: 0x0015474D
		public static float NormalizedTime { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x00156555 File Offset: 0x00154755
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x0015655C File Offset: 0x0015475C
		public static AudioUtils Audio { get; private set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x00156564 File Offset: 0x00154764
		// (set) Token: 0x06002F21 RID: 12065 RVA: 0x0015656B File Offset: 0x0015476B
		public static Camera WorldCamera { get; private set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x00156573 File Offset: 0x00154773
		// (set) Token: 0x06002F23 RID: 12067 RVA: 0x0015657A File Offset: 0x0015477A
		public static SurfaceDatabase SurfaceDatabase { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x00156582 File Offset: 0x00154782
		// (set) Token: 0x06002F25 RID: 12069 RVA: 0x00156589 File Offset: 0x00154789
		public static ItemDatabase ItemDatabase { get; private set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x00156591 File Offset: 0x00154791
		// (set) Token: 0x06002F27 RID: 12071 RVA: 0x00156598 File Offset: 0x00154798
		public static TreeManager TerrainHelpers { get; private set; }

		// Token: 0x06002F28 RID: 12072 RVA: 0x001565A0 File Offset: 0x001547A0
		private void Awake()
		{
			GameController.Audio = base.GetComponentInChildren<AudioUtils>();
			GameController.WorldCamera = GameController.LocalPlayer.transform.FindDeepChild("World Camera").GetComponent<Camera>();
			GameController.SurfaceDatabase = this.m_SurfaceDatabase;
			GameController.ItemDatabase = this.m_ItemDatabase;
			GameController.TerrainHelpers = base.GetComponent<TreeManager>();
		}

		// Token: 0x0400296D RID: 10605
		[SerializeField]
		private SurfaceDatabase m_SurfaceDatabase;

		// Token: 0x0400296E RID: 10606
		[SerializeField]
		private ItemDatabase m_ItemDatabase;

		// Token: 0x0400296F RID: 10607
		private static MGInputManager m_InputManager;

		// Token: 0x04002970 RID: 10608
		private static PlayerEventHandler m_Player;
	}
}
