using System;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public static class GreyMatManager
{
	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06002047 RID: 8263 RVA: 0x0001A792 File Offset: 0x00018992
	public static Material Grey1
	{
		get
		{
			if (GreyMatManager.grey1 == null)
			{
				GreyMatManager.grey1 = Resources.Load<Material>("NewUI/Misc/ImageGreyMat");
			}
			return GreyMatManager.grey1;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06002048 RID: 8264 RVA: 0x0001A7B5 File Offset: 0x000189B5
	public static Material Grey2
	{
		get
		{
			if (GreyMatManager.grey2 == null)
			{
				GreyMatManager.grey2 = Resources.Load<Material>("NewUI/Misc/ImageGreyShader2");
			}
			return GreyMatManager.grey2;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06002049 RID: 8265 RVA: 0x0001A7D8 File Offset: 0x000189D8
	public static Material Black
	{
		get
		{
			if (GreyMatManager.black == null)
			{
				GreyMatManager.black = Resources.Load<Material>("NewUI/Misc/ImageBlackMat");
			}
			return GreyMatManager.black;
		}
	}

	// Token: 0x04001BC3 RID: 7107
	private static Material grey1;

	// Token: 0x04001BC4 RID: 7108
	private static Material grey2;

	// Token: 0x04001BC5 RID: 7109
	private static Material black;
}
