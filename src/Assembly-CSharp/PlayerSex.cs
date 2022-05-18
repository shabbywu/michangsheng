using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000293 RID: 659
public class PlayerSex : MonoBehaviour
{
	// Token: 0x06001430 RID: 5168 RVA: 0x00012BC6 File Offset: 0x00010DC6
	private void Start()
	{
		PlayerSex.SetSex(base.gameObject);
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x000B8C94 File Offset: 0x000B6E94
	public static void SetSex(GameObject root)
	{
		SkeletonRenderer componentInChildren = root.GetComponentInChildren<SkeletonRenderer>();
		if (componentInChildren != null)
		{
			Animator componentInChildren2 = root.GetComponentInChildren<Animator>();
			if (componentInChildren2 != null)
			{
				if (Tools.instance.getPlayer().Sex == 1)
				{
					componentInChildren.initialSkinName = "0";
				}
				else
				{
					componentInChildren.initialSkinName = "10";
				}
				componentInChildren.Initialize(true);
				componentInChildren2.SetFloat("Sex", (float)Tools.instance.getPlayer().Sex);
			}
		}
	}
}
