using System;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class TooltipStaticPop : TooltipBase
{
	// Token: 0x06002232 RID: 8754 RVA: 0x00004095 File Offset: 0x00002295
	private new void Start()
	{
	}

	// Token: 0x06002233 RID: 8755 RVA: 0x000EBE78 File Offset: 0x000EA078
	private new void Update()
	{
		if (this.shoudSetPos)
		{
			if (this.showTooltip)
			{
				base.transform.position = this.pos;
				return;
			}
			base.transform.position = new Vector3(0f, 10000f, 0f);
		}
	}

	// Token: 0x04001BB5 RID: 7093
	public Vector3 pos;

	// Token: 0x04001BB6 RID: 7094
	public Vector3 rotation;
}
