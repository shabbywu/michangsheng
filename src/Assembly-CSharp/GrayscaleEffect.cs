using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Grayscale")]
public class GrayscaleEffect : ImageEffectBase
{
	public Texture textureRamp;

	public float rampOffset;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", textureRamp);
		base.material.SetFloat("_RampOffset", rampOffset);
		Graphics.Blit((Texture)(object)source, destination, base.material);
	}
}
