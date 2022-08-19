using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class autoRemoveSelf : MonoBehaviour
{
	// Token: 0x06001EE4 RID: 7908 RVA: 0x000D8674 File Offset: 0x000D6874
	private void Start()
	{
		base.Invoke("nextplay", 3.1f);
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x000D8688 File Offset: 0x000D6888
	public void nextplay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "0";
		base.Invoke("endplay", 3.2f);
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x000D86D4 File Offset: 0x000D68D4
	public void endplay()
	{
		SkeletonAnimation componentInChildren = base.GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string str = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = str + "2";
		base.Invoke("Removere", 3.2f);
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x0005C928 File Offset: 0x0005AB28
	public void Removere()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400194A RID: 6474
	public float time = 1f;
}
