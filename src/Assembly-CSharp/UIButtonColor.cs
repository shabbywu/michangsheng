using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	public enum State
	{
		Normal,
		Hover,
		Pressed,
		Disabled
	}

	public GameObject tweenTarget;

	public Color hover = new Color(0.88235295f, 40f / 51f, 0.5882353f, 1f);

	public Color pressed = new Color(61f / 85f, 0.6392157f, 41f / 85f, 1f);

	public Color disabledColor = Color.grey;

	public float duration = 0.2f;

	[NonSerialized]
	protected Color mStartingColor;

	[NonSerialized]
	protected Color mDefaultColor;

	[NonSerialized]
	protected bool mInitDone;

	[NonSerialized]
	protected UIWidget mWidget;

	[NonSerialized]
	protected State mState;

	public State state
	{
		get
		{
			return mState;
		}
		set
		{
			SetState(value, instant: false);
		}
	}

	public Color defaultColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (!mInitDone)
			{
				OnInit();
			}
			return mDefaultColor;
		}
		set
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (!mInitDone)
			{
				OnInit();
			}
			mDefaultColor = value;
			State state = mState;
			mState = State.Disabled;
			SetState(state, instant: false);
		}
	}

	public virtual bool isEnabled
	{
		get
		{
			return ((Behaviour)this).enabled;
		}
		set
		{
			((Behaviour)this).enabled = value;
		}
	}

	public void ResetDefaultColor()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		defaultColor = mStartingColor;
	}

	private void Awake()
	{
		if (!mInitDone)
		{
			OnInit();
		}
	}

	private void Start()
	{
		if (!isEnabled)
		{
			SetState(State.Disabled, instant: true);
		}
	}

	protected virtual void OnInit()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		mInitDone = true;
		if ((Object)(object)tweenTarget == (Object)null)
		{
			tweenTarget = ((Component)this).gameObject;
		}
		mWidget = tweenTarget.GetComponent<UIWidget>();
		if ((Object)(object)mWidget != (Object)null)
		{
			mDefaultColor = mWidget.color;
			mStartingColor = mDefaultColor;
			return;
		}
		Renderer component = tweenTarget.GetComponent<Renderer>();
		if ((Object)(object)component != (Object)null)
		{
			mDefaultColor = (Application.isPlaying ? component.material.color : component.sharedMaterial.color);
			mStartingColor = mDefaultColor;
			return;
		}
		Light component2 = tweenTarget.GetComponent<Light>();
		if ((Object)(object)component2 != (Object)null)
		{
			mDefaultColor = component2.color;
			mStartingColor = mDefaultColor;
		}
		else
		{
			tweenTarget = null;
			mInitDone = false;
		}
	}

	protected virtual void OnEnable()
	{
		if (mInitDone)
		{
			OnHover(UICamera.IsHighlighted(((Component)this).gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if ((Object)(object)UICamera.currentTouch.pressed == (Object)(object)((Component)this).gameObject)
			{
				OnPress(isPressed: true);
			}
			else if ((Object)(object)UICamera.currentTouch.current == (Object)(object)((Component)this).gameObject)
			{
				OnHover(isOver: true);
			}
		}
	}

	protected virtual void OnDisable()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (mInitDone && (Object)(object)tweenTarget != (Object)null)
		{
			SetState(State.Normal, instant: true);
			TweenColor component = tweenTarget.GetComponent<TweenColor>();
			if ((Object)(object)component != (Object)null)
			{
				component.value = mDefaultColor;
				((Behaviour)component).enabled = false;
			}
		}
	}

	protected virtual void OnHover(bool isOver)
	{
		if (isEnabled)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if ((Object)(object)tweenTarget != (Object)null)
			{
				SetState(isOver ? State.Hover : State.Normal, instant: false);
			}
		}
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (!isEnabled || UICamera.currentTouch == null)
		{
			return;
		}
		if (!mInitDone)
		{
			OnInit();
		}
		if (!((Object)(object)tweenTarget != (Object)null))
		{
			return;
		}
		if (isPressed)
		{
			SetState(State.Pressed, instant: false);
		}
		else if ((Object)(object)UICamera.currentTouch.current == (Object)(object)((Component)this).gameObject)
		{
			if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
			{
				SetState(State.Hover, instant: false);
			}
			else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && (Object)(object)UICamera.hoveredObject == (Object)(object)((Component)this).gameObject)
			{
				SetState(State.Hover, instant: false);
			}
			else
			{
				SetState(State.Normal, instant: false);
			}
		}
		else
		{
			SetState(State.Normal, instant: false);
		}
	}

	protected virtual void OnDragOver()
	{
		if (isEnabled)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if ((Object)(object)tweenTarget != (Object)null)
			{
				SetState(State.Pressed, instant: false);
			}
		}
	}

	protected virtual void OnDragOut()
	{
		if (isEnabled)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if ((Object)(object)tweenTarget != (Object)null)
			{
				SetState(State.Normal, instant: false);
			}
		}
	}

	protected virtual void OnSelect(bool isSelected)
	{
		if (isEnabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller) && (Object)(object)tweenTarget != (Object)null)
		{
			OnHover(isSelected);
		}
	}

	public virtual void SetState(State state, bool instant)
	{
		if (!mInitDone)
		{
			mInitDone = true;
			OnInit();
		}
		if (mState != state)
		{
			mState = state;
			UpdateColor(instant);
		}
	}

	public void UpdateColor(bool instant)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		TweenColor tweenColor = mState switch
		{
			State.Hover => TweenColor.Begin(tweenTarget, duration, hover), 
			State.Pressed => TweenColor.Begin(tweenTarget, duration, pressed), 
			State.Disabled => TweenColor.Begin(tweenTarget, duration, disabledColor), 
			_ => TweenColor.Begin(tweenTarget, duration, mDefaultColor), 
		};
		if (instant && (Object)(object)tweenColor != (Object)null)
		{
			tweenColor.value = tweenColor.to;
			((Behaviour)tweenColor).enabled = false;
		}
	}
}
