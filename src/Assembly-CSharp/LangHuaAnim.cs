using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x0200053C RID: 1340
public class LangHuaAnim : MonoBehaviour
{
	// Token: 0x06002234 RID: 8756 RVA: 0x0001C0D5 File Offset: 0x0001A2D5
	private void OnEnable()
	{
		base.Invoke("NextPlay", 3.1f);
	}

	// Token: 0x06002235 RID: 8757 RVA: 0x0011AEF4 File Offset: 0x001190F4
	public void NextPlay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "0";
		base.Invoke("EndPlay", 3.2f);
	}

	// Token: 0x06002236 RID: 8758 RVA: 0x0011AF40 File Offset: 0x00119140
	public void EndPlay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "2";
		base.Invoke("Recovery", 3.2f);
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x0001C0E7 File Offset: 0x0001A2E7
	public void Recovery()
	{
		GameObjectPool.Recovery(base.gameObject);
	}

	// Token: 0x04001D9A RID: 7578
	public float time = 1f;
}
