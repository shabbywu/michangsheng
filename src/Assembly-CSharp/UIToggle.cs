using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	public static UIToggle current;

	public int group;

	public UIWidget activeSprite;

	public Animation activeAnimation;

	public bool startsActive;

	public bool instantTween;

	public bool optionCanBeNone;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	private bool mIsActive = true;

	private bool mStarted;

	public bool value
	{
		get
		{
			if (!mStarted)
			{
				return startsActive;
			}
			return mIsActive;
		}
		set
		{
			if (!mStarted)
			{
				startsActive = value;
			}
			else if (group == 0 || value || optionCanBeNone || !mStarted)
			{
				Set(value);
			}
		}
	}

	[Obsolete("Use 'value' instead")]
	public bool isChecked
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < list.size; i++)
		{
			UIToggle uIToggle = list[i];
			if ((Object)(object)uIToggle != (Object)null && uIToggle.group == group && uIToggle.mIsActive)
			{
				return uIToggle;
			}
		}
		return null;
	}

	private void OnEnable()
	{
		list.Add(this);
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	private void Start()
	{
		if (startsChecked)
		{
			startsChecked = false;
			startsActive = true;
		}
		if (!Application.isPlaying)
		{
			if ((Object)(object)checkSprite != (Object)null && (Object)(object)activeSprite == (Object)null)
			{
				activeSprite = checkSprite;
				checkSprite = null;
			}
			if ((Object)(object)checkAnimation != (Object)null && (Object)(object)activeAnimation == (Object)null)
			{
				activeAnimation = checkAnimation;
				checkAnimation = null;
			}
			if (Application.isPlaying && (Object)(object)activeSprite != (Object)null)
			{
				activeSprite.alpha = (startsActive ? 1f : 0f);
			}
			if (EventDelegate.IsValid(onChange))
			{
				eventReceiver = null;
				functionName = null;
			}
		}
		else
		{
			mIsActive = !startsActive;
			mStarted = true;
			bool flag = instantTween;
			instantTween = true;
			Set(startsActive);
			instantTween = flag;
		}
	}

	private void OnClick()
	{
		if (((Behaviour)this).enabled)
		{
			value = !value;
		}
	}

	private void Set(bool state)
	{
		if (!mStarted)
		{
			mIsActive = state;
			startsActive = state;
			if ((Object)(object)activeSprite != (Object)null)
			{
				activeSprite.alpha = (state ? 1f : 0f);
			}
		}
		else
		{
			if (mIsActive == state)
			{
				return;
			}
			if (group != 0 && state)
			{
				int num = 0;
				int size = list.size;
				while (num < size)
				{
					UIToggle uIToggle = list[num];
					if ((Object)(object)uIToggle != (Object)(object)this && uIToggle.group == group)
					{
						uIToggle.Set(state: false);
					}
					if (list.size != size)
					{
						size = list.size;
						num = 0;
					}
					else
					{
						num++;
					}
				}
			}
			mIsActive = state;
			if ((Object)(object)activeSprite != (Object)null)
			{
				if (instantTween)
				{
					activeSprite.alpha = (mIsActive ? 1f : 0f);
				}
				else
				{
					TweenAlpha.Begin(((Component)activeSprite).gameObject, 0.15f, mIsActive ? 1f : 0f);
				}
			}
			if ((Object)(object)current == (Object)null)
			{
				current = this;
				if (EventDelegate.IsValid(onChange))
				{
					EventDelegate.Execute(onChange);
				}
				else if ((Object)(object)eventReceiver != (Object)null && !string.IsNullOrEmpty(functionName))
				{
					eventReceiver.SendMessage(functionName, (object)mIsActive, (SendMessageOptions)1);
				}
				current = null;
			}
			if ((Object)(object)this.activeAnimation != (Object)null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.activeAnimation, state ? Direction.Forward : Direction.Reverse);
				if (instantTween)
				{
					activeAnimation.Finish();
				}
			}
		}
	}
}
