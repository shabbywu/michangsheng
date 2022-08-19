using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x020003B1 RID: 945
public class LangHuaAnim : MonoBehaviour
{
	// Token: 0x06001EB1 RID: 7857 RVA: 0x000D7A39 File Offset: 0x000D5C39
	private void OnEnable()
	{
		base.Invoke("NextPlay", 3.1f);
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x000D7A4C File Offset: 0x000D5C4C
	public void NextPlay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "0";
		base.Invoke("EndPlay", 3.2f);
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x000D7A98 File Offset: 0x000D5C98
	public void EndPlay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "2";
		base.Invoke("Recovery", 3.2f);
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x000D7AE2 File Offset: 0x000D5CE2
	public void Recovery()
	{
		GameObjectPool.Recovery(base.gameObject);
	}

	// Token: 0x04001926 RID: 6438
	public float time = 1f;
}
