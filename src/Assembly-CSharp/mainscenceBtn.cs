using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class mainscenceBtn : MonoBehaviour
{
	// Token: 0x0600121C RID: 4636 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x0001143F File Offset: 0x0000F63F
	private void OnHover(bool isOver)
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, "0", false);
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x00011459 File Offset: 0x0000F659
	private void OnPress()
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, "trigger", false);
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000E95 RID: 3733
	public SkeletonAnimation skeletonAnimation;
}
