using System;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class ADDItem : MonoBehaviour
{
	// Token: 0x060011FB RID: 4603 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x000042DD File Offset: 0x000024DD
	private void OnGUI()
	{
	}

	// Token: 0x04000E83 RID: 3715
	public int item;

	// Token: 0x04000E84 RID: 3716
	public int[] SetStaticValue = new int[2];

	// Token: 0x04000E85 RID: 3717
	public int money;

	// Token: 0x04000E86 RID: 3718
	public int startItemID;

	// Token: 0x04000E87 RID: 3719
	public int EndItemID;

	// Token: 0x04000E88 RID: 3720
	public int ListAddNum = 1;

	// Token: 0x04000E89 RID: 3721
	[Tooltip("需要增加悟道经验的悟道类型")]
	public int wudaoType;

	// Token: 0x04000E8A RID: 3722
	[Tooltip("增加悟道经验")]
	public int AddWuDaoExNum;

	// Token: 0x04000E8B RID: 3723
	[Tooltip("增加悟道点")]
	public int AddWuDaoDian;

	// Token: 0x04000E8C RID: 3724
	[Tooltip("临时值")]
	public int TempValue;
}
