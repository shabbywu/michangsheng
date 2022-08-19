using System;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public class FightFlashBlack : MonoBehaviour
{
	// Token: 0x060022A7 RID: 8871 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060022A8 RID: 8872 RVA: 0x00004095 File Offset: 0x00002295
	public void Flash()
	{
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x000ED82D File Offset: 0x000EBA2D
	public void Hide()
	{
		this.SR.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x04001BFA RID: 7162
	public SpriteRenderer SR;
}
