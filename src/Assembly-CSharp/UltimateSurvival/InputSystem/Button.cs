using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000924 RID: 2340
	[Serializable]
	public class Button
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x0002B117 File Offset: 0x00029317
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x0002B11F File Offset: 0x0002931F
		public string Name
		{
			get
			{
				return this.m_ButtonName;
			}
			set
			{
				this.m_ButtonName = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x0002B128 File Offset: 0x00029328
		public KeyCode Key
		{
			get
			{
				return this.m_Key;
			}
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x0002B130 File Offset: 0x00029330
		public Button(string name)
		{
			this.m_ButtonName = name;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x0002B13F File Offset: 0x0002933F
		public Button(string name, KeyCode key)
		{
			this.m_ButtonName = name;
			this.m_Key = key;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x0002B130 File Offset: 0x00029330
		public Button(string name, ButtonHandler handler)
		{
			this.m_ButtonName = name;
		}

		// Token: 0x04003639 RID: 13881
		[SerializeField]
		private string m_ButtonName;

		// Token: 0x0400363A RID: 13882
		[SerializeField]
		private KeyCode m_Key;
	}
}
