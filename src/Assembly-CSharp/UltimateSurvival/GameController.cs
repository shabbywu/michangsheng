using System;
using UltimateSurvival.InputSystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000856 RID: 2134
	public class GameController : MonoBehaviour
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000285D5 File Offset: 0x000267D5
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

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000285F3 File Offset: 0x000267F3
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

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x00028611 File Offset: 0x00026811
		// (set) Token: 0x0600379B RID: 14235 RVA: 0x00028618 File Offset: 0x00026818
		public static float NormalizedTime { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x00028620 File Offset: 0x00026820
		// (set) Token: 0x0600379D RID: 14237 RVA: 0x00028627 File Offset: 0x00026827
		public static AudioUtils Audio { get; private set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x0002862F File Offset: 0x0002682F
		// (set) Token: 0x0600379F RID: 14239 RVA: 0x00028636 File Offset: 0x00026836
		public static Camera WorldCamera { get; private set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x0002863E File Offset: 0x0002683E
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x00028645 File Offset: 0x00026845
		public static SurfaceDatabase SurfaceDatabase { get; private set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x0002864D File Offset: 0x0002684D
		// (set) Token: 0x060037A3 RID: 14243 RVA: 0x00028654 File Offset: 0x00026854
		public static ItemDatabase ItemDatabase { get; private set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x0002865C File Offset: 0x0002685C
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x00028663 File Offset: 0x00026863
		public static TreeManager TerrainHelpers { get; private set; }

		// Token: 0x060037A6 RID: 14246 RVA: 0x001A0CF4 File Offset: 0x0019EEF4
		private void Awake()
		{
			GameController.Audio = base.GetComponentInChildren<AudioUtils>();
			GameController.WorldCamera = GameController.LocalPlayer.transform.FindDeepChild("World Camera").GetComponent<Camera>();
			GameController.SurfaceDatabase = this.m_SurfaceDatabase;
			GameController.ItemDatabase = this.m_ItemDatabase;
			GameController.TerrainHelpers = base.GetComponent<TreeManager>();
		}

		// Token: 0x040031EE RID: 12782
		[SerializeField]
		private SurfaceDatabase m_SurfaceDatabase;

		// Token: 0x040031EF RID: 12783
		[SerializeField]
		private ItemDatabase m_ItemDatabase;

		// Token: 0x040031F0 RID: 12784
		private static MGInputManager m_InputManager;

		// Token: 0x040031F1 RID: 12785
		private static PlayerEventHandler m_Player;
	}
}
