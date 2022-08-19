using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class mainscenceBtn : MonoBehaviour
{
	// Token: 0x06000FBC RID: 4028 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x0005E50B File Offset: 0x0005C70B
	private void OnHover(bool isOver)
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, "0", false);
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x0005E525 File Offset: 0x0005C725
	private void OnPress()
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, "trigger", false);
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000BC4 RID: 3012
	public SkeletonAnimation skeletonAnimation;
}
