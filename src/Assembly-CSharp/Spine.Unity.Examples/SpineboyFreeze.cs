using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineboyFreeze : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	public AnimationReferenceAsset freeze;

	public AnimationReferenceAsset idle;

	public Color freezeColor;

	public Color freezeBlackColor;

	public ParticleSystem particles;

	public float freezePoint = 0.5f;

	public string colorProperty = "_Color";

	public string blackTintProperty = "_Black";

	private MaterialPropertyBlock block;

	private MeshRenderer meshRenderer;

	private IEnumerator Start()
	{
		block = new MaterialPropertyBlock();
		meshRenderer = ((Component)this).GetComponent<MeshRenderer>();
		particles.Stop();
		particles.Clear();
		MainModule main = particles.main;
		((MainModule)(ref main)).loop = false;
		AnimationState state = skeletonAnimation.AnimationState;
		while (true)
		{
			yield return (object)new WaitForSeconds(1f);
			state.SetAnimation(0, AnimationReferenceAsset.op_Implicit(freeze), false);
			yield return (object)new WaitForSeconds(freezePoint);
			particles.Play();
			block.SetColor(colorProperty, freezeColor);
			block.SetColor(blackTintProperty, freezeBlackColor);
			((Renderer)meshRenderer).SetPropertyBlock(block);
			yield return (object)new WaitForSeconds(2f);
			state.SetAnimation(0, AnimationReferenceAsset.op_Implicit(idle), true);
			block.SetColor(colorProperty, Color.white);
			block.SetColor(blackTintProperty, Color.black);
			((Renderer)meshRenderer).SetPropertyBlock(block);
			yield return null;
		}
	}
}
