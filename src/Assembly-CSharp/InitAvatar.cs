using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class InitAvatar : MonoBehaviour
{
	// Token: 0x06001890 RID: 6288 RVA: 0x000B0630 File Offset: 0x000AE830
	private void Awake()
	{
		this.SkeletonAnimation.gameObject.SetActive(false);
		if (this.SexType == 1)
		{
			this.SkeletonAnimation.skeletonDataAsset = ResManager.inst.LoadABSkeletonDataAsset(this.SexType, "Fight_new_sanxiu_2_SkeletonData");
		}
		else
		{
			this.SkeletonAnimation.skeletonDataAsset = ResManager.inst.LoadABSkeletonDataAsset(this.SexType, "Fight_womensanxiu_0_SkeletonData");
		}
		this.SkeletonAnimation.gameObject.SetActive(true);
	}

	// Token: 0x04001398 RID: 5016
	public int SexType = 1;

	// Token: 0x04001399 RID: 5017
	public SkeletonAnimation SkeletonAnimation;
}
