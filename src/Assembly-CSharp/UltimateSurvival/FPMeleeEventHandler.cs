using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005EE RID: 1518
	public class FPMeleeEventHandler : MonoBehaviour
	{
		// Token: 0x060030D3 RID: 12499 RVA: 0x0015D1B3 File Offset: 0x0015B3B3
		public void On_Hit()
		{
			this.Hit.Send();
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0015D1C0 File Offset: 0x0015B3C0
		public void On_Woosh()
		{
			this.Woosh.Send();
		}

		// Token: 0x04002B06 RID: 11014
		public Message Hit = new Message();

		// Token: 0x04002B07 RID: 11015
		public Message Woosh = new Message();
	}
}
