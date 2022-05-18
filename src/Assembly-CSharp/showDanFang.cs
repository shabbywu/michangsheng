using System;
using UnityEngine;

// Token: 0x02000626 RID: 1574
public class showDanFang : MonoBehaviour
{
	// Token: 0x0600271F RID: 10015 RVA: 0x0001F173 File Offset: 0x0001D373
	private void Start()
	{
		this.lianDanDanFang.InitDanFang();
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x0001F173 File Offset: 0x0001D373
	public void Open()
	{
		this.lianDanDanFang.InitDanFang();
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002141 RID: 8513
	public LianDanDanFang lianDanDanFang;
}
