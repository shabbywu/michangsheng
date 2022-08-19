using System;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
	// Token: 0x02000B2B RID: 2859
	public class EIU_DebugItem : MonoBehaviour
	{
		// Token: 0x06004FBA RID: 20410 RVA: 0x0021AA73 File Offset: 0x00218C73
		public void Init(string axisName, string keyName)
		{
			this.axisName.text = axisName;
			this.keyName.text = keyName;
		}

		// Token: 0x04004EB2 RID: 20146
		public Text axisName;

		// Token: 0x04004EB3 RID: 20147
		public Text keyName;
	}
}
