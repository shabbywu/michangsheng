using System;
using UnityEngine;

namespace SoftMasking.Extensions
{
	// Token: 0x020006E3 RID: 1763
	public static class MaterialOps
	{
		// Token: 0x060038E8 RID: 14568 RVA: 0x00184A33 File Offset: 0x00182C33
		public static bool SupportsSoftMask(this Material mat)
		{
			return mat.HasProperty("_SoftMask");
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x00184A40 File Offset: 0x00182C40
		public static bool HasDefaultUIShader(this Material mat)
		{
			return mat.shader == Canvas.GetDefaultCanvasMaterial().shader;
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x00184A57 File Offset: 0x00182C57
		public static bool HasDefaultETC1UIShader(this Material mat)
		{
			return mat.shader == Canvas.GetETC1SupportedCanvasMaterial().shader;
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x00184A6E File Offset: 0x00182C6E
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
