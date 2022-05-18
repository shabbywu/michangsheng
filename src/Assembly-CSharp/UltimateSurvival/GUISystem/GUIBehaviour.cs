using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200092F RID: 2351
	public class GUIBehaviour : MonoBehaviour
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x0002B4CF File Offset: 0x000296CF
		public GUIController Controller
		{
			get
			{
				if (this.m_Controller == null)
				{
					this.m_Controller = base.GetComponentInParent<GUIController>();
				}
				return this.m_Controller;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x0002B4F1 File Offset: 0x000296F1
		public PlayerEventHandler Player
		{
			get
			{
				if (!this.Controller)
				{
					return null;
				}
				return this.Controller.Player;
			}
		}

		// Token: 0x04003664 RID: 13924
		private GUIController m_Controller;
	}
}
