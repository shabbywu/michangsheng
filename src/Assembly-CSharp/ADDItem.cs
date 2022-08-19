using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class ADDItem : MonoBehaviour
{
	// Token: 0x06000F9D RID: 3997 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00004095 File Offset: 0x00002295
	private void OnGUI()
	{
	}

	// Token: 0x04000BB3 RID: 2995
	public int item;

	// Token: 0x04000BB4 RID: 2996
	public int[] SetStaticValue = new int[2];

	// Token: 0x04000BB5 RID: 2997
	public int money;

	// Token: 0x04000BB6 RID: 2998
	public int startItemID;

	// Token: 0x04000BB7 RID: 2999
	public int EndItemID;

	// Token: 0x04000BB8 RID: 3000
	public int ListAddNum = 1;

	// Token: 0x04000BB9 RID: 3001
	[Tooltip("需要增加悟道经验的悟道类型")]
	public int wudaoType;

	// Token: 0x04000BBA RID: 3002
	[Tooltip("增加悟道经验")]
	public int AddWuDaoExNum;

	// Token: 0x04000BBB RID: 3003
	[Tooltip("增加悟道点")]
	public int AddWuDaoDian;

	// Token: 0x04000BBC RID: 3004
	[Tooltip("临时值")]
	public int TempValue;
}
