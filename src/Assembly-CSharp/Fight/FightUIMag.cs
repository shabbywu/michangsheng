using System;
using UnityEngine;

namespace Fight
{
	// Token: 0x02000728 RID: 1832
	public class FightUIMag : MonoBehaviour
	{
		// Token: 0x06003A6A RID: 14954 RVA: 0x00191358 File Offset: 0x0018F558
		private void Awake()
		{
			FightUIMag.inst = this;
		}

		// Token: 0x04003290 RID: 12944
		public static FightUIMag inst;
	}
}
