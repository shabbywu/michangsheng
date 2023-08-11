using Spine.Unity;
using UnityEngine;

public class autoRemoveSelf : MonoBehaviour
{
	public float time = 1f;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("nextplay", 3.1f);
	}

	public void nextplay()
	{
		SkeletonAnimation componentInChildren = ((Component)this).GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string text = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = text + "0";
		((MonoBehaviour)this).Invoke("endplay", 3.2f);
	}

	public void endplay()
	{
		SkeletonAnimation componentInChildren = ((Component)this).GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string text = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = text + "2";
		((MonoBehaviour)this).Invoke("Removere", 3.2f);
	}

	public void Removere()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
