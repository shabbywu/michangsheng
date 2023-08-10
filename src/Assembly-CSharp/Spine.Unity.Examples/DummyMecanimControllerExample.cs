using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class DummyMecanimControllerExample : MonoBehaviour
{
	public Animator logicAnimator;

	public SkeletonAnimationHandleExample animationHandle;

	[Header("Controls")]
	public KeyCode walkButton = (KeyCode)304;

	public KeyCode jumpButton = (KeyCode)32;

	[Header("Animator Properties")]
	public string horizontalSpeedProperty = "Speed";

	public string verticalSpeedProperty = "VerticalSpeed";

	public string groundedProperty = "Grounded";

	[Header("Fake Physics")]
	public float jumpDuration = 1.5f;

	public Vector2 speed;

	public bool isGrounded;

	private void Awake()
	{
		isGrounded = true;
	}

	private void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		float num = Input.GetAxisRaw("Horizontal");
		if (Input.GetKey(walkButton))
		{
			num += 0.4f;
		}
		speed.x = num;
		if (num != 0f)
		{
			animationHandle.SetFlip(num);
		}
		if (Input.GetKeyDown(jumpButton) && isGrounded)
		{
			((MonoBehaviour)this).StartCoroutine(FakeJump());
		}
		logicAnimator.SetFloat(horizontalSpeedProperty, Mathf.Abs(speed.x));
		logicAnimator.SetFloat(verticalSpeedProperty, speed.y);
		logicAnimator.SetBool(groundedProperty, isGrounded);
	}

	private IEnumerator FakeJump()
	{
		isGrounded = false;
		speed.y = 10f;
		float durationLeft = jumpDuration * 0.5f;
		while (durationLeft > 0f)
		{
			durationLeft -= Time.deltaTime;
			if (!Input.GetKey(jumpButton))
			{
				break;
			}
			yield return null;
		}
		speed.y = -10f;
		float num = jumpDuration * 0.5f - durationLeft;
		yield return (object)new WaitForSeconds(num);
		speed.y = 0f;
		isGrounded = true;
		yield return null;
	}
}
