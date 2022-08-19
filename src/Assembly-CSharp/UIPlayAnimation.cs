using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200006C RID: 108
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001CDB7 File Offset: 0x0001AFB7
	private bool dualState
	{
		get
		{
			return this.trigger == Trigger.OnPress || this.trigger == Trigger.OnHover;
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
	private void Awake()
	{
		UIButton component = base.GetComponent<UIButton>();
		if (component != null)
		{
			this.dragHighlight = component.dragHighlight;
		}
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001CE24 File Offset: 0x0001B024
	private void Start()
	{
		this.mStarted = true;
		if (this.target == null && this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (this.animator != null)
		{
			if (this.animator.enabled)
			{
				this.animator.enabled = false;
			}
			return;
		}
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null && this.target.enabled)
		{
			this.target.enabled = false;
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0001CECC File Offset: 0x0001B0CC
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (this.trigger == Trigger.OnPress || this.trigger == Trigger.OnPressTrue)
			{
				this.mActivated = (UICamera.currentTouch.pressed == base.gameObject);
			}
			if (this.trigger == Trigger.OnHover || this.trigger == Trigger.OnHoverTrue)
			{
				this.mActivated = (UICamera.currentTouch.current == base.gameObject);
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001CF7C File Offset: 0x0001B17C
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001CFB1 File Offset: 0x0001B1B1
	private void OnHover(bool isOver)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
		{
			this.Play(isOver, this.dualState);
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0001CFEB File Offset: 0x0001B1EB
	private void OnPress(bool isPressed)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed))
		{
			this.Play(isPressed, this.dualState);
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001D025 File Offset: 0x0001B225
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001D03F File Offset: 0x0001B23F
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0001D05B File Offset: 0x0001B25B
	private void OnSelect(bool isSelected)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected))
		{
			this.Play(isSelected, this.dualState);
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001D098 File Offset: 0x0001B298
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value, this.dualState);
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001D108 File Offset: 0x0001B308
	private void OnDragOver()
	{
		if (base.enabled && this.dualState)
		{
			if (UICamera.currentTouch.dragged == base.gameObject)
			{
				this.Play(true, true);
				return;
			}
			if (this.dragHighlight && this.trigger == Trigger.OnPress)
			{
				this.Play(true, true);
			}
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0001D15E File Offset: 0x0001B35E
	private void OnDragOut()
	{
		if (base.enabled && this.dualState && UICamera.hoveredObject != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001D18A File Offset: 0x0001B38A
	private void OnDrop(GameObject go)
	{
		if (base.enabled && this.trigger == Trigger.OnPress && UICamera.currentTouch.dragged != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001D1BC File Offset: 0x0001B3BC
	public void Play(bool forward)
	{
		this.Play(forward, true);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
	public void Play(bool forward, bool onlyIfDifferent)
	{
		if (this.target || this.animator)
		{
			if (onlyIfDifferent)
			{
				if (this.mActivated == forward)
				{
					return;
				}
				this.mActivated = forward;
			}
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			Direction direction = forward ? this.playDirection : ((Direction)num);
			ActiveAnimation activeAnimation = this.target ? ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished) : ActiveAnimation.Play(this.animator, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation != null)
			{
				if (this.resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < this.onFinished.Count; i++)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
			}
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001D2D0 File Offset: 0x0001B4D0
	private void OnFinished()
	{
		if (UIPlayAnimation.current == null)
		{
			UIPlayAnimation.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, 1);
			}
			this.eventReceiver = null;
			UIPlayAnimation.current = null;
		}
	}

	// Token: 0x0400036C RID: 876
	public static UIPlayAnimation current;

	// Token: 0x0400036D RID: 877
	public Animation target;

	// Token: 0x0400036E RID: 878
	public Animator animator;

	// Token: 0x0400036F RID: 879
	public string clipName;

	// Token: 0x04000370 RID: 880
	public Trigger trigger;

	// Token: 0x04000371 RID: 881
	public Direction playDirection = Direction.Forward;

	// Token: 0x04000372 RID: 882
	public bool resetOnPlay;

	// Token: 0x04000373 RID: 883
	public bool clearSelection;

	// Token: 0x04000374 RID: 884
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04000375 RID: 885
	public DisableCondition disableWhenFinished;

	// Token: 0x04000376 RID: 886
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000377 RID: 887
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000378 RID: 888
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x04000379 RID: 889
	private bool mStarted;

	// Token: 0x0400037A RID: 890
	private bool mActivated;

	// Token: 0x0400037B RID: 891
	private bool dragHighlight;
}
