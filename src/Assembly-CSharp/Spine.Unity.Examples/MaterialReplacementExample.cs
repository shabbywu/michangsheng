using UnityEngine;

namespace Spine.Unity.Examples;

public class MaterialReplacementExample : MonoBehaviour
{
	public Material originalMaterial;

	public Material replacementMaterial;

	public bool replacementEnabled = true;

	public SkeletonAnimation skeletonAnimation;

	[Space]
	public string phasePropertyName = "_FillPhase";

	[Range(0f, 1f)]
	public float phase = 1f;

	private bool previousEnabled;

	private MaterialPropertyBlock mpb;

	private void Start()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		previousEnabled = replacementEnabled;
		SetReplacementEnabled(replacementEnabled);
		mpb = new MaterialPropertyBlock();
	}

	private void Update()
	{
		mpb.SetFloat(phasePropertyName, phase);
		((Renderer)((Component)this).GetComponent<MeshRenderer>()).SetPropertyBlock(mpb);
		if (previousEnabled != replacementEnabled)
		{
			SetReplacementEnabled(replacementEnabled);
		}
		previousEnabled = replacementEnabled;
	}

	private void SetReplacementEnabled(bool active)
	{
		if (replacementEnabled)
		{
			((SkeletonRenderer)skeletonAnimation).CustomMaterialOverride[originalMaterial] = replacementMaterial;
		}
		else
		{
			((SkeletonRenderer)skeletonAnimation).CustomMaterialOverride.Remove(originalMaterial);
		}
	}
}
