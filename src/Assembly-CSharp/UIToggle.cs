using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000AA RID: 170
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000662 RID: 1634 RVA: 0x00009AC6 File Offset: 0x00007CC6
	// (set) Token: 0x06000663 RID: 1635 RVA: 0x00009ADD File Offset: 0x00007CDD
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

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000664 RID: 1636 RVA: 0x00009B13 File Offset: 0x00007D13
	// (set) Token: 0x06000665 RID: 1637 RVA: 0x00009B1B File Offset: 0x00007D1B
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

	// Token: 0x06000666 RID: 1638 RVA: 0x00077754 File Offset: 0x00075954
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

	// Token: 0x06000667 RID: 1639 RVA: 0x00009B24 File Offset: 0x00007D24
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00009B31 File Offset: 0x00007D31
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x000777A0 File Offset: 0x000759A0
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

	// Token: 0x0600066A RID: 1642 RVA: 0x00009B3F File Offset: 0x00007D3F
	private void OnClick()
	{
		if (base.enabled)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x000778B4 File Offset: 0x00075AB4
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

	// Token: 0x040004CB RID: 1227
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x040004CC RID: 1228
	public static UIToggle current;

	// Token: 0x040004CD RID: 1229
	public int group;

	// Token: 0x040004CE RID: 1230
	public UIWidget activeSprite;

	// Token: 0x040004CF RID: 1231
	public Animation activeAnimation;

	// Token: 0x040004D0 RID: 1232
	public bool startsActive;

	// Token: 0x040004D1 RID: 1233
	public bool instantTween;

	// Token: 0x040004D2 RID: 1234
	public bool optionCanBeNone;

	// Token: 0x040004D3 RID: 1235
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040004D4 RID: 1236
	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	// Token: 0x040004D5 RID: 1237
	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	// Token: 0x040004D6 RID: 1238
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x040004D7 RID: 1239
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	// Token: 0x040004D8 RID: 1240
	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	// Token: 0x040004D9 RID: 1241
	private bool mIsActive = true;

	// Token: 0x040004DA RID: 1242
	private bool mStarted;
}
