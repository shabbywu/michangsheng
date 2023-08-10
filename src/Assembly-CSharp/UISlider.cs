using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
public class UISlider : UIProgressBar
{
	private enum Direction
	{
		Horizontal,
		Vertical,
		Upgraded
	}

	[HideInInspector]
	[SerializeField]
	private Transform foreground;

	[HideInInspector]
	[SerializeField]
	private float rawValue = 1f;

	[HideInInspector]
	[SerializeField]
	private Direction direction = Direction.Upgraded;

	[HideInInspector]
	[SerializeField]
	protected bool mInverted;

	[Obsolete("Use 'value' instead")]
	public float sliderValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	[Obsolete("Use 'fillDirection' instead")]
	public bool inverted
	{
		get
		{
			return base.isInverted;
		}
		set
		{
		}
	}

	protected override void Upgrade()
	{
		if (direction != Direction.Upgraded)
		{
			mValue = rawValue;
			if ((Object)(object)foreground != (Object)null)
			{
				mFG = ((Component)foreground).GetComponent<UIWidget>();
			}
			if (direction == Direction.Horizontal)
			{
				mFill = (mInverted ? FillDirection.RightToLeft : FillDirection.LeftToRight);
			}
			else
			{
				mFill = (mInverted ? FillDirection.TopToBottom : FillDirection.BottomToTop);
			}
			direction = Direction.Upgraded;
		}
	}

	protected override void OnStart()
	{
		UIEventListener uIEventListener = UIEventListener.Get(((Object)(object)mBG != (Object)null && ((Object)(object)((Component)mBG).GetComponent<Collider>() != (Object)null || (Object)(object)((Component)mBG).GetComponent<Collider2D>() != (Object)null)) ? ((Component)mBG).gameObject : ((Component)this).gameObject);
		uIEventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener.onPress, new UIEventListener.BoolDelegate(OnPressBackground));
		uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDragBackground));
		if ((Object)(object)thumb != (Object)null && ((Object)(object)((Component)thumb).GetComponent<Collider>() != (Object)null || (Object)(object)((Component)thumb).GetComponent<Collider2D>() != (Object)null) && ((Object)(object)mFG == (Object)null || (Object)(object)thumb != (Object)(object)mFG.cachedTransform))
		{
			UIEventListener uIEventListener2 = UIEventListener.Get(((Component)thumb).gameObject);
			uIEventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener2.onPress, new UIEventListener.BoolDelegate(OnPressForeground));
			uIEventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener2.onDrag, new UIEventListener.VectorDelegate(OnDragForeground));
		}
	}

	protected void OnPressBackground(GameObject go, bool isPressed)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = ScreenToValue(UICamera.lastTouchPosition);
			if (!isPressed && onDragFinished != null)
			{
				onDragFinished();
			}
		}
	}

	protected void OnDragBackground(GameObject go, Vector2 delta)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = ScreenToValue(UICamera.lastTouchPosition);
		}
	}

	protected void OnPressForeground(GameObject go, bool isPressed)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			if (isPressed)
			{
				mOffset = (((Object)(object)mFG == (Object)null) ? 0f : (base.value - ScreenToValue(UICamera.lastTouchPosition)));
			}
			else if (onDragFinished != null)
			{
				onDragFinished();
			}
		}
	}

	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = mOffset + ScreenToValue(UICamera.lastTouchPosition);
		}
	}

	protected void OnKey(KeyCode key)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Invalid comparison between Unknown and I4
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Invalid comparison between Unknown and I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Invalid comparison between Unknown and I4
		if (!((Behaviour)this).enabled)
		{
			return;
		}
		float num = (((float)numberOfSteps > 1f) ? (1f / (float)(numberOfSteps - 1)) : 0.125f);
		if (base.fillDirection == FillDirection.LeftToRight || base.fillDirection == FillDirection.RightToLeft)
		{
			if ((int)key == 276)
			{
				base.value = mValue - num;
			}
			else if ((int)key == 275)
			{
				base.value = mValue + num;
			}
		}
		else if ((int)key == 274)
		{
			base.value = mValue - num;
		}
		else if ((int)key == 273)
		{
			base.value = mValue + num;
		}
	}
}
