using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public static class GreyMatManager
{
	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06001CDD RID: 7389 RVA: 0x000CE1D7 File Offset: 0x000CC3D7
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

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000CE1FA File Offset: 0x000CC3FA
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

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06001CDF RID: 7391 RVA: 0x000CE21D File Offset: 0x000CC41D
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

	// Token: 0x0400176B RID: 5995
	private static Material grey1;

	// Token: 0x0400176C RID: 5996
	private static Material grey2;

	// Token: 0x0400176D RID: 5997
	private static Material black;
}
