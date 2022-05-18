using System;
using UnityEngine;

namespace SoftMasking.Extensions
{
	// Token: 0x02000A22 RID: 2594
	public static class MaterialOps
	{
		// Token: 0x0600434F RID: 17231 RVA: 0x000301AA File Offset: 0x0002E3AA
		public static bool SupportsSoftMask(this Material mat)
		{
			return mat.HasProperty("_SoftMask");
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000301B7 File Offset: 0x0002E3B7
		public static bool HasDefaultUIShader(this Material mat)
		{
			return mat.shader == Canvas.GetDefaultCanvasMaterial().shader;
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000301CE File Offset: 0x0002E3CE
		public static bool HasDefaultETC1UIShader(this Material mat)
		{
			return mat.shader == Canvas.GetETC1SupportedCanvasMaterial().shader;
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000301E5 File Offset: 0x0002E3E5
		public static void EnableKeyword(this Material mat, string keyword, bool enabled)
		{
			if (enabled)
			{
				mat.EnableKeyword(keyword);
				return;
			}
			mat.DisableKeyword(keyword);
		}
	}
}
