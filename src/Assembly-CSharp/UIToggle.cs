using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000077 RID: 119
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00021BDC File Offset: 0x0001FDDC
	// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00021BF3 File Offset: 0x0001FDF3
	public bool value
	{
		get
		{
			if (!this.mStarted)
			{
				return this.startsActive;
			}
			return this.mIsActive;
		}
		set
		{
			if (!this.mStarted)
			{
				this.startsActive = value;
				return;
			}
			if (this.group == 0 || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value);
			}
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00021C29 File Offset: 0x0001FE29
	// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00021C31 File Offset: 0x0001FE31
	[Obsolete("Use 'value' instead")]
	public bool isChecked
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00021C3C File Offset: 0x0001FE3C
	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < UIToggle.list.size; i++)
		{
			UIToggle uitoggle = UIToggle.list[i];
			if (uitoggle != null && uitoggle.group == group && uitoggle.mIsActive)
			{
				return uitoggle;
			}
		}
		return null;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00021C87 File Offset: 0x0001FE87
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00021C94 File Offset: 0x0001FE94
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00021CA4 File Offset: 0x0001FEA4
	private void Start()
	{
		if (this.startsChecked)
		{
			this.startsChecked = false;
			this.startsActive = true;
		}
		if (!Application.isPlaying)
		{
			if (this.checkSprite != null && this.activeSprite == null)
			{
				this.activeSprite = this.checkSprite;
				this.checkSprite = null;
			}
			if (this.checkAnimation != null && this.activeAnimation == null)
			{
				this.activeAnimation = this.checkAnimation;
				this.checkAnimation = null;
			}
			if (Application.isPlaying && this.activeSprite != null)
			{
				this.activeSprite.alpha = (this.startsActive ? 1f : 0f);
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				this.eventReceiver = null;
				this.functionName = null;
				return;
			}
		}
		else
		{
			this.mIsActive = !this.startsActive;
			this.mStarted = true;
			bool flag = this.instantTween;
			this.instantTween = true;
			this.Set(this.startsActive);
			this.instantTween = flag;
		}
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00021DB6 File Offset: 0x0001FFB6
	private void OnClick()
	{
		if (base.enabled)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00021DD0 File Offset: 0x0001FFD0
	private void Set(bool state)
	{
		if (!this.mStarted)
		{
			this.mIsActive = state;
			this.startsActive = state;
			if (this.activeSprite != null)
			{
				this.activeSprite.alpha = (state ? 1f : 0f);
				return;
			}
		}
		else if (this.mIsActive != state)
		{
			if (this.group != 0 && state)
			{
				int i = 0;
				int size = UIToggle.list.size;
				while (i < size)
				{
					UIToggle uitoggle = UIToggle.list[i];
					if (uitoggle != this && uitoggle.group == this.group)
					{
						uitoggle.Set(false);
					}
					if (UIToggle.list.size != size)
					{
						size = UIToggle.list.size;
						i = 0;
					}
					else
					{
						i++;
					}
				}
			}
			this.mIsActive = state;
			if (this.activeSprite != null)
			{
				if (this.instantTween)
				{
					this.activeSprite.alpha = (this.mIsActive ? 1f : 0f);
				}
				else
				{
					TweenAlpha.Begin(this.activeSprite.gameObject, 0.15f, this.mIsActive ? 1f : 0f);
				}
			}
			if (UIToggle.current == null)
			{
				UIToggle.current = this;
				if (EventDelegate.IsValid(this.onChange))
				{
					EventDelegate.Execute(this.onChange);
				}
				else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
				{
					this.eventReceiver.SendMessage(this.functionName, this.mIsActive, 1);
				}
				UIToggle.current = null;
			}
			if (this.activeAnimation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.activeAnimation, state ? Direction.Forward : Direction.Reverse);
				if (this.instantTween)
				{
					activeAnimation.Finish();
				}
			}
		}
	}

	// Token: 0x040003FB RID: 1019
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x040003FC RID: 1020
	public static UIToggle current;

	// Token: 0x040003FD RID: 1021
	public int group;

	// Token: 0x040003FE RID: 1022
	public UIWidget activeSprite;

	// Token: 0x040003FF RID: 1023
	public Animation activeAnimation;

	// Token: 0x04000400 RID: 1024
	public bool startsActive;

	// Token: 0x04000401 RID: 1025
	public bool instantTween;

	// Token: 0x04000402 RID: 1026
	public bool optionCanBeNone;

	// Token: 0x04000403 RID: 1027
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04000404 RID: 1028
	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	// Token: 0x04000405 RID: 1029
	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	// Token: 0x04000406 RID: 1030
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000407 RID: 1031
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	// Token: 0x04000408 RID: 1032
	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	// Token: 0x04000409 RID: 1033
	private bool mIsActive = true;

	// Token: 0x0400040A RID: 1034
	private bool mStarted;
}
