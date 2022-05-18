using System;
using UnityEngine;

// Token: 0x02000515 RID: 1301
public class TooltipMag : MonoBehaviour
{
	// Token: 0x0600218D RID: 8589 RVA: 0x0001B95B File Offset: 0x00019B5B
	private void Awake()
	{
		TooltipMag.inst = this;
	}

	// Token: 0x04001D14 RID: 7444
	public static TooltipMag inst;
}
