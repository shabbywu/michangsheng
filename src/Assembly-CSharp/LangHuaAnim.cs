using Spine.Unity;
using UnityEngine;

public class LangHuaAnim : MonoBehaviour
{
	public float time = 1f;

	private void OnEnable()
	{
		((MonoBehaviour)this).Invoke("NextPlay", 3.1f);
	}

	public void NextPlay()
	{
		SkeletonAnimation componentInChildren = ((Component)this).GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string text = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = text + "0";
		((MonoBehaviour)this).Invoke("EndPlay", 3.2f);
	}

	public void EndPlay()
	{
		SkeletonAnimation componentInChildren = ((Component)this).GetComponentInChildren<SkeletonAnimation>();
		string animationName = componentInChildren.AnimationName;
		string text = animationName.Substring(0, animationName.Length - 1);
		componentInChildren.AnimationName = text + "2";
		((MonoBehaviour)this).Invoke("Recovery", 3.2f);
	}

	public void Recovery()
	{
		GameObjectPool.Recovery(((Component)this).gameObject);
	}
}
