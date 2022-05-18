using System;
using UnityEngine;

namespace Fight
{
	// Token: 0x02000A7F RID: 2687
	public class FightUIMag : MonoBehaviour
	{
		// Token: 0x0600450C RID: 17676 RVA: 0x00031697 File Offset: 0x0002F897
		private void Awake()
		{
			FightUIMag.inst = this;
		}

		// Token: 0x04003D2B RID: 15659
		public static FightUIMag inst;
	}
}
