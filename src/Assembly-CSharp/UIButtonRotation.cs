using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	public Transform tweenTarget;

	public Vector3 hover = Vector3.zero;

	public Vector3 pressed = Vector3.zero;

	public float duration = 0.2f;

	private Quaternion mRot;

	private bool mStarted;

	private void Start()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (!mStarted)
		{
			mStarted = true;
			if ((Object)(object)tweenTarget == (Object)null)
			{
				tweenTarget = ((Component)this).transform;
			}
			mRot = tweenTarget.localRotation;
		}
	}

	private void OnEnable()
	{
		if (mStarted)
		{
			OnHover(UICamera.IsHighlighted(((Component)this).gameObject));
		}
	}

	private void OnDisable()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (mStarted && (Object)(object)tweenTarget != (Object)null)
		{
			TweenRotation component = ((Component)tweenTarget).GetComponent<TweenRotation>();
			if ((Object)(object)component != (Object)null)
			{
				component.value = mRot;
				((Behaviour)component).enabled = false;
			}
		}
	}

	private void OnPress(bool isPressed)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled)
		{
			if (!mStarted)
			{
				Start();
			}
			TweenRotation.Begin(((Component)tweenTarget).gameObject, duration, isPressed ? (mRot * Quaternion.Euler(pressed)) : (UICamera.IsHighlighted(((Component)this).gameObject) ? (mRot * Quaternion.Euler(hover)) : mRot)).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnHover(bool isOver)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled)
		{
			if (!mStarted)
			{
				Start();
			}
			TweenRotation.Begin(((Component)tweenTarget).gameObject, duration, isOver ? (mRot * Quaternion.Euler(hover)) : mRot).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (((Behaviour)this).enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			OnHover(isSelected);
		}
	}
}
