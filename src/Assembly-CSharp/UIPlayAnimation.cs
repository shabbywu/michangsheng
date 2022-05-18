using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000090 RID: 144
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600059F RID: 1439 RVA: 0x000091DD File Offset: 0x000073DD
	private bool dualState
	{
		get
		{
			return this.trigger == Trigger.OnPress || this.trigger == Trigger.OnHover;
		}
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00073154 File Offset: 0x00071354
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

	// Token: 0x060005A1 RID: 1441 RVA: 0x000731A8 File Offset: 0x000713A8
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

	// Token: 0x060005A2 RID: 1442 RVA: 0x00073250 File Offset: 0x00071450
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

	// Token: 0x060005A3 RID: 1443 RVA: 0x00073300 File Offset: 0x00071500
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x000091F3 File Offset: 0x000073F3
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

	// Token: 0x060005A5 RID: 1445 RVA: 0x0000922D File Offset: 0x0000742D
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

	// Token: 0x060005A6 RID: 1446 RVA: 0x00009267 File Offset: 0x00007467
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00009281 File Offset: 0x00007481
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0000929D File Offset: 0x0000749D
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

	// Token: 0x060005A9 RID: 1449 RVA: 0x00073338 File Offset: 0x00071538
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

	// Token: 0x060005AA RID: 1450 RVA: 0x000733A8 File Offset: 0x000715A8
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

	// Token: 0x060005AB RID: 1451 RVA: 0x000092DA File Offset: 0x000074DA
	private void OnDragOut()
	{
		if (base.enabled && this.dualState && UICamera.hoveredObject != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00009306 File Offset: 0x00007506
	private void OnDrop(GameObject go)
	{
		if (base.enabled && this.trigger == Trigger.OnPress && UICamera.currentTouch.dragged != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00009338 File Offset: 0x00007538
	public void Play(bool forward)
	{
		this.Play(forward, true);
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00073400 File Offset: 0x00071600
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

	// Token: 0x060005AF RID: 1455 RVA: 0x00073508 File Offset: 0x00071708
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

	// Token: 0x0400040A RID: 1034
	public static UIPlayAnimation current;

	// Token: 0x0400040B RID: 1035
	public Animation target;

	// Token: 0x0400040C RID: 1036
	public Animator animator;

	// Token: 0x0400040D RID: 1037
	public string clipName;

	// Token: 0x0400040E RID: 1038
	public Trigger trigger;

	// Token: 0x0400040F RID: 1039
	public Direction playDirection = Direction.Forward;

	// Token: 0x04000410 RID: 1040
	public bool resetOnPlay;

	// Token: 0x04000411 RID: 1041
	public bool clearSelection;

	// Token: 0x04000412 RID: 1042
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04000413 RID: 1043
	public DisableCondition disableWhenFinished;

	// Token: 0x04000414 RID: 1044
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000415 RID: 1045
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000416 RID: 1046
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x04000417 RID: 1047
	private bool mStarted;

	// Token: 0x04000418 RID: 1048
	private bool mActivated;

	// Token: 0x04000419 RID: 1049
	private bool dragHighlight;
}
