using System;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class TooltipMag : MonoBehaviour
{
	// Token: 0x06001E12 RID: 7698 RVA: 0x000D48E0 File Offset: 0x000D2AE0
	private void Awake()
	{
		TooltipMag.inst = this;
	}

	// Token: 0x040018B4 RID: 6324
	public static TooltipMag inst;
}
