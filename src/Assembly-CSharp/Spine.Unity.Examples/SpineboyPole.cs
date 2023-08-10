using System.Collections;
using Spine.Unity.Modules;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineboyPole : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	public SkeletonRenderSeparator separator;

	[Space(18f)]
	public AnimationReferenceAsset run;

	public AnimationReferenceAsset pole;

	public float startX;

	public float endX;

	private const float Speed = 18f;

	private const float RunTimeScale = 1.5f;

	private IEnumerator Start()
	{
		AnimationState state = skeletonAnimation.state;
		while (true)
		{
			SetXPosition(startX);
			((Behaviour)separator).enabled = false;
			state.SetAnimation(0, AnimationReferenceAsset.op_Implicit(run), true);
			state.TimeScale = 1.5f;
			while (((Component)this).transform.localPosition.x < endX)
			{
				((Component)this).transform.Translate(Vector3.right * 18f * Time.deltaTime);
				yield return null;
			}
			SetXPosition(endX);
			((Behaviour)separator).enabled = true;
			TrackEntry val = state.SetAnimation(0, AnimationReferenceAsset.op_Implicit(pole), false);
			yield return (object)new WaitForSpineAnimationComplete(val);
			yield return (object)new WaitForSeconds(1f);
		}
	}

	private void SetXPosition(float x)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = ((Component)this).transform.localPosition;
		localPosition.x = x;
		((Component)this).transform.localPosition = localPosition;
	}
}
