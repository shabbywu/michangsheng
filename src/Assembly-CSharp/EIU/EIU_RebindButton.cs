using System;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
	// Token: 0x02000B2D RID: 2861
	public class EIU_RebindButton : MonoBehaviour
	{
		// Token: 0x06004FBF RID: 20415 RVA: 0x0021AC02 File Offset: 0x00218E02
		public void init(string n)
		{
			this.axis_text.text = n;
		}

		// Token: 0x04004EB8 RID: 20152
		public Text axis_text;
	}
}
