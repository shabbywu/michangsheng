using Spine;
using Spine.Unity;
using UnityEngine;

public class DestoryMeOnSpineAnimEnd : MonoBehaviour
{
	public SkeletonAnimation Anim;

	private void Awake()
	{
		if ((Object)(object)Anim == (Object)null)
		{
			Anim = ((Component)this).GetComponent<SkeletonAnimation>();
		}
		if ((Object)(object)Anim == (Object)null)
		{
			Anim = ((Component)this).GetComponentInChildren<SkeletonAnimation>();
		}
	}

	private void Start()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		if ((Object)(object)Anim != (Object)null)
		{
			Anim.AnimationState.Complete += new TrackEntryDelegate(AnimationState_Complete);
		}
	}

	private void AnimationState_Complete(TrackEntry trackEntry)
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
