using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000543 RID: 1347
public class autoRemoveSelf : MonoBehaviour
{
	// Token: 0x06002262 RID: 8802 RVA: 0x0001C322 File Offset: 0x0001A522
	private void Start()
	{
		base.Invoke("nextplay", 3.1f);
	}

	// Token: 0x06002263 RID: 8803 RVA: 0x0011B724 File Offset: 0x00119924
	public void nextplay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "0";
		base.Invoke("endplay", 3.2f);
	}

	// Token: 0x06002264 RID: 8804 RVA: 0x0011B770 File Offset: 0x00119970
	public void endplay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "2";
		base.Invoke("Removere", 3.2f);
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x000111B3 File Offset: 0x0000F3B3
	public void Removere()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04001DB7 RID: 7607
	public float time = 1f;
}
