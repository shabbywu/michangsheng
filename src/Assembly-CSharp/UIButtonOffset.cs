using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x060004B6 RID: 1206 RVA: 0x00019C54 File Offset: 0x00017E54
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

	// Token: 0x060004B7 RID: 1207 RVA: 0x00019C90 File Offset: 0x00017E90
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00019CAC File Offset: 0x00017EAC
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

	// Token: 0x060004B9 RID: 1209 RVA: 0x00019CF8 File Offset: 0x00017EF8
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

	// Token: 0x060004BA RID: 1210 RVA: 0x00019D74 File Offset: 0x00017F74
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

	// Token: 0x060004BB RID: 1211 RVA: 0x00019DCF File Offset: 0x00017FCF
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x040002DA RID: 730
	public Transform tweenTarget;

	// Token: 0x040002DB RID: 731
	public Vector3 hover = Vector3.zero;

	// Token: 0x040002DC RID: 732
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x040002DD RID: 733
	public float duration = 0.2f;

	// Token: 0x040002DE RID: 734
	private Vector3 mPos;

	// Token: 0x040002DF RID: 735
	private bool mStarted;
}
