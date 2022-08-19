using System;
using Spine.Unity;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class PlayerSex : MonoBehaviour
{
	// Token: 0x0600118F RID: 4495 RVA: 0x0006A87C File Offset: 0x00068A7C
	private void Start()
	{
		PlayerSex.SetSex(base.gameObject);
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0006A88C File Offset: 0x00068A8C
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
