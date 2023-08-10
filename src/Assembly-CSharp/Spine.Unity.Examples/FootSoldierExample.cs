using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class FootSoldierExample : MonoBehaviour
{
	[SpineAnimation("Idle", "", true, false)]
	public string idleAnimation;

	[SpineAnimation("", "", true, false)]
	public string attackAnimation;

	[SpineAnimation("", "", true, false)]
	public string moveAnimation;

	[SpineSlot("", "", false, true, false)]
	public string eyesSlot;

	[SpineAttachment(true, false, false, "eyesSlot", "", "", true, false)]
	public string eyesOpenAttachment;

	[SpineAttachment(true, false, false, "eyesSlot", "", "", true, false)]
	public string blinkAttachment;

	[Range(0f, 0.2f)]
	public float blinkDuration = 0.05f;

	public KeyCode attackKey = (KeyCode)323;

	public KeyCode rightKey = (KeyCode)100;

	public KeyCode leftKey = (KeyCode)97;

	public float moveSpeed = 3f;

	private SkeletonAnimation skeletonAnimation;

	private void Awake()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		((SkeletonRenderer)skeletonAnimation).OnRebuild += new SkeletonRendererDelegate(Apply);
	}

	private void Apply(SkeletonRenderer skeletonRenderer)
	{
		((MonoBehaviour)this).StartCoroutine("Blink");
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKey(attackKey))
		{
			skeletonAnimation.AnimationName = attackAnimation;
		}
		else if (Input.GetKey(rightKey))
		{
			skeletonAnimation.AnimationName = moveAnimation;
			((SkeletonRenderer)skeletonAnimation).Skeleton.ScaleX = 1f;
			((Component)this).transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
		}
		else if (Input.GetKey(leftKey))
		{
			skeletonAnimation.AnimationName = moveAnimation;
			((SkeletonRenderer)skeletonAnimation).Skeleton.ScaleX = -1f;
			((Component)this).transform.Translate((0f - moveSpeed) * Time.deltaTime, 0f, 0f);
		}
		else
		{
			skeletonAnimation.AnimationName = idleAnimation;
		}
	}

	private IEnumerator Blink()
	{
		while (true)
		{
			yield return (object)new WaitForSeconds(Random.Range(0.25f, 3f));
			((SkeletonRenderer)skeletonAnimation).Skeleton.SetAttachment(eyesSlot, blinkAttachment);
			yield return (object)new WaitForSeconds(blinkDuration);
			((SkeletonRenderer)skeletonAnimation).Skeleton.SetAttachment(eyesSlot, eyesOpenAttachment);
		}
	}
}
