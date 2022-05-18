using System;
using UnityEngine;

// Token: 0x020005FE RID: 1534
public class FightFlashBlack : MonoBehaviour
{
	// Token: 0x06002664 RID: 9828 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x000042DD File Offset: 0x000024DD
	public void Flash()
	{
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x0001E996 File Offset: 0x0001CB96
	public void Hide()
	{
		this.SR.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x040020C6 RID: 8390
	public SpriteRenderer SR;
}
