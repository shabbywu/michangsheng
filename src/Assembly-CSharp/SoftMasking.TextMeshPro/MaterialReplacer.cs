using UnityEngine;

namespace SoftMasking.TextMeshPro;

[GlobalMaterialReplacer]
public class MaterialReplacer : IMaterialReplacer
{
	public int order => 10;

	public Material Replace(Material material)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		if (Object.op_Implicit((Object)(object)material) && Object.op_Implicit((Object)(object)material.shader) && ((Object)material.shader).name.StartsWith("TextMeshPro/"))
		{
			Shader val = Shader.Find("Soft Mask/" + ((Object)material.shader).name);
			if (Object.op_Implicit((Object)(object)val))
			{
				Material val2 = new Material(val);
				val2.CopyPropertiesFromMaterial(material);
				return val2;
			}
		}
		return null;
	}
}
