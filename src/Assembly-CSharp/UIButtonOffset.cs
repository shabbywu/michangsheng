using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x06000504 RID: 1284 RVA: 0x000086DD File Offset: 0x000068DD
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00008719 File Offset: 0x00006919
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00070AE4 File Offset: 0x0006ECE4
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00070B30 File Offset: 0x0006ED30
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? (this.mPos + this.pressed) : (UICamera.IsHighlighted(base.gameObject) ? (this.mPos + this.hover) : this.mPos)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00070BAC File Offset: 0x0006EDAC
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, isOver ? (this.mPos + this.hover) : this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00008734 File Offset: 0x00006934
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000359 RID: 857
	public Transform tweenTarget;

	// Token: 0x0400035A RID: 858
	public Vector3 hover = Vector3.zero;

	// Token: 0x0400035B RID: 859
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x0400035C RID: 860
	public float duration = 0.2f;

	// Token: 0x0400035D RID: 861
	private Vector3 mPos;

	// Token: 0x0400035E RID: 862
	private bool mStarted;
}
