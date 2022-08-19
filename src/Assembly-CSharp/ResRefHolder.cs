using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class ResRefHolder : MonoBehaviour
{
	// Token: 0x06001335 RID: 4917 RVA: 0x00078C55 File Offset: 0x00076E55
	private void Awake()
	{
		ResRefHolder.Inst = this;
	}

	// Token: 0x04000E86 RID: 3718
	public static ResRefHolder Inst;

	// Token: 0x04000E87 RID: 3719
	public List<SkeletonDataAsset> SeaJiZhiRes;
}
