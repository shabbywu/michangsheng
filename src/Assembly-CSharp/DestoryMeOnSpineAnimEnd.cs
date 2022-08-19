using System;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class DestoryMeOnSpineAnimEnd : MonoBehaviour
{
	// Token: 0x060022A3 RID: 8867 RVA: 0x000ED7CB File Offset: 0x000EB9CB
	private void Awake()
	{
		if (this.Anim == null)
		{
			this.Anim = base.GetComponent<SkeletonAnimation>();
		}
		if (this.Anim == null)
		{
			this.Anim = base.GetComponentInChildren<SkeletonAnimation>();
		}
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x000ED801 File Offset: 0x000EBA01
	private void Start()
	{
		if (this.Anim != null)
		{
			this.Anim.AnimationState.Complete += new AnimationState.TrackEntryDelegate(this.AnimationState_Complete);
		}
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x0005C928 File Offset: 0x0005AB28
	private void AnimationState_Complete(TrackEntry trackEntry)
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04001BF9 RID: 7161
	public SkeletonAnimation Anim;
}
