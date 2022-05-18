using System;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
	// Token: 0x02000E9F RID: 3743
	public class EIU_DebugItem : MonoBehaviour
	{
		// Token: 0x060059D9 RID: 23001 RVA: 0x0003FBF3 File Offset: 0x0003DDF3
		public void Init(string axisName, string keyName)
		{
			this.axisName.text = axisName;
			this.keyName.text = keyName;
		}

		// Token: 0x04005929 RID: 22825
		public Text axisName;

		// Token: 0x0400592A RID: 22826
		public Text keyName;
	}
}
