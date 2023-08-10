using UnityEngine;

namespace Spine.Unity.Examples;

public class MecanimToAnimationHandleExample : StateMachineBehaviour
{
	private SkeletonAnimationHandleExample animationHandle;

	private bool initialized;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!initialized)
		{
			animationHandle = ((Component)animator).GetComponent<SkeletonAnimationHandleExample>();
			initialized = true;
		}
		animationHandle.PlayAnimationForState(((AnimatorStateInfo)(ref stateInfo)).shortNameHash, layerIndex);
	}
}
