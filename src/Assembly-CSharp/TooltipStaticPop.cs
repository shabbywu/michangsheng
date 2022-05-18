using System;
using UnityEngine;

// Token: 0x020005E6 RID: 1510
public class TooltipStaticPop : TooltipBase
{
	// Token: 0x060025F1 RID: 9713 RVA: 0x000042DD File Offset: 0x000024DD
	private new void Start()
	{
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x0012D018 File Offset: 0x0012B218
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

	// Token: 0x04002081 RID: 8321
	public Vector3 pos;

	// Token: 0x04002082 RID: 8322
	public Vector3 rotation;
}
