using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200063A RID: 1594
	public class GUIBehaviour : MonoBehaviour
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x00165EB9 File Offset: 0x001640B9
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x00165EDB File Offset: 0x001640DB
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

		// Token: 0x04002D08 RID: 11528
		private GUIController m_Controller;
	}
}
