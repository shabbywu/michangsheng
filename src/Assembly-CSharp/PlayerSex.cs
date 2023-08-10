using Spine.Unity;
using UnityEngine;

public class PlayerSex : MonoBehaviour
{
	private void Start()
	{
		SetSex(((Component)this).gameObject);
	}

	public static void SetSex(GameObject root)
	{
		SkeletonRenderer componentInChildren = root.GetComponentInChildren<SkeletonRenderer>();
		if (!((Object)(object)componentInChildren != (Object)null))
		{
			return;
		}
		Animator componentInChildren2 = root.GetComponentInChildren<Animator>();
		if ((Object)(object)componentInChildren2 != (Object)null)
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
