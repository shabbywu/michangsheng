using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000632 RID: 1586
	[Serializable]
	public class Button
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x001653C0 File Offset: 0x001635C0
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x001653C8 File Offset: 0x001635C8
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x001653D1 File Offset: 0x001635D1
		public KeyCode Key
		{
			get
			{
				return this.m_Key;
			}
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x001653D9 File Offset: 0x001635D9
		public Button(string name)
		{
			this.m_ButtonName = name;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x001653E8 File Offset: 0x001635E8
		public Button(string name, KeyCode key)
		{
			this.m_ButtonName = name;
			this.m_Key = key;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x001653D9 File Offset: 0x001635D9
		public Button(string name, ButtonHandler handler)
		{
			this.m_ButtonName = name;
		}

		// Token: 0x04002CE8 RID: 11496
		[SerializeField]
		private string m_ButtonName;

		// Token: 0x04002CE9 RID: 11497
		[SerializeField]
		private KeyCode m_Key;
	}
}
