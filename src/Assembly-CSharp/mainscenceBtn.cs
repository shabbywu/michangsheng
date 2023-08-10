using Spine.Unity;
using UnityEngine;

public class mainscenceBtn : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	private void Start()
	{
	}

	private void OnHover(bool isOver)
	{
		skeletonAnimation.AnimationState.SetAnimation(0, "0", false);
	}

	private void OnPress()
	{
		skeletonAnimation.AnimationState.SetAnimation(0, "trigger", false);
	}

	private void Update()
	{
	}
}
