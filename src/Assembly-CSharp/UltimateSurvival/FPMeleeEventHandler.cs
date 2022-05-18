using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C2 RID: 2242
	public class FPMeleeEventHandler : MonoBehaviour
	{
		// Token: 0x060039B1 RID: 14769 RVA: 0x00029DEE File Offset: 0x00027FEE
		public void On_Hit()
		{
			this.Hit.Send();
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x00029DFB File Offset: 0x00027FFB
		public void On_Woosh()
		{
			this.Woosh.Send();
		}

		// Token: 0x040033DC RID: 13276
		public Message Hit = new Message();

		// Token: 0x040033DD RID: 13277
		public Message Woosh = new Message();
	}
}
