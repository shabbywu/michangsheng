using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Desaturate")]
public class DesaturateEffect : ImageEffectBase
{
	public float desaturateAmount;

	public Texture textureRamp;

	public float rampOffsetR;

	public float rampOffsetG;

	public float rampOffsetB;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		base.material.SetTexture("_RampTex", textureRamp);
		base.material.SetFloat("_Desat", desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(rampOffsetR, rampOffsetG, rampOffsetB, 0f));
		Graphics.Blit((Texture)(object)source, destination, base.material);
	}
}
