using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class ResRefHolder : MonoBehaviour
{
	// Token: 0x060015F1 RID: 5617 RVA: 0x00013B6E File Offset: 0x00011D6E
	private void Awake()
	{
		ResRefHolder.Inst = this;
	}

	// Token: 0x040011C5 RID: 4549
	public static ResRefHolder Inst;

	// Token: 0x040011C6 RID: 4550
	public List<SkeletonDataAsset> SeaJiZhiRes;
}
