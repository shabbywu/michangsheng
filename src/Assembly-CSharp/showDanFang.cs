using System;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class showDanFang : MonoBehaviour
{
	// Token: 0x06002369 RID: 9065 RVA: 0x000F25BB File Offset: 0x000F07BB
	private void Start()
	{
		this.lianDanDanFang.InitDanFang();
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x000F25BB File Offset: 0x000F07BB
	public void Open()
	{
		this.lianDanDanFang.InitDanFang();
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C6C RID: 7276
	public LianDanDanFang lianDanDanFang;
}
