using UnityEngine;

namespace SoftMasking.Extensions;

public static class MaterialOps
{
	public static bool SupportsSoftMask(this Material mat)
	{
		return mat.HasProperty("_SoftMask");
	}

	public static bool HasDefaultUIShader(this Material mat)
	{
		return (Object)(object)mat.shader == (Object)(object)Canvas.GetDefaultCanvasMaterial().shader;
	}

	public static bool HasDefaultETC1UIShader(this Material mat)
	{
		return (Object)(object)mat.shader == (Object)(object)Canvas.GetETC1SupportedCanvasMaterial().shader;
	}

	public static void EnableKeyword(this Material mat, string keyword, bool enabled)
	{
		if (enabled)
		{
			mat.EnableKeyword(keyword);
		}
		else
		{
			mat.DisableKeyword(keyword);
		}
	}
}
