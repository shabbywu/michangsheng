using System;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
	// Token: 0x02000EA1 RID: 3745
	public class EIU_RebindButton : MonoBehaviour
	{
		// Token: 0x060059DE RID: 23006 RVA: 0x0003FC5C File Offset: 0x0003DE5C
		public void init(string n)
		{
			this.axis_text.text = n;
		}

		// Token: 0x0400592F RID: 22831
		public Text axis_text;
	}
}
