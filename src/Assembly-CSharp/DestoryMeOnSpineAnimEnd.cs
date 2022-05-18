using System;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x020005FD RID: 1533
public class DestoryMeOnSpineAnimEnd : MonoBehaviour
{
	// Token: 0x06002660 RID: 9824 RVA: 0x0001E934 File Offset: 0x0001CB34
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

	// Token: 0x06002661 RID: 9825 RVA: 0x0001E96A File Offset: 0x0001CB6A
	private void Start()
	{
		if (this.Anim != null)
		{
			this.Anim.AnimationState.Complete += new AnimationState.TrackEntryDelegate(this.AnimationState_Complete);
		}
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x000111B3 File Offset: 0x0000F3B3
	private void AnimationState_Complete(TrackEntry trackEntry)
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040020C5 RID: 8389
	public SkeletonAnimation Anim;
}
