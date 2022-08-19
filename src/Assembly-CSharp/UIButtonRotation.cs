using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x060004BD RID: 1213 RVA: 0x00019E1E File Offset: 0x0001801E
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00019E5A File Offset: 0x0001805A
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00019E78 File Offset: 0x00018078
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00019EC4 File Offset: 0x000180C4
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? (this.mRot * Quaternion.Euler(this.pressed)) : (UICamera.IsHighlighted(base.gameObject) ? (this.mRot * Quaternion.Euler(this.hover)) : this.mRot)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00019F4C File Offset: 0x0001814C
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, isOver ? (this.mRot * Quaternion.Euler(this.hover)) : this.mRot).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00019FAC File Offset: 0x000181AC
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x040002E0 RID: 736
	public Transform tweenTarget;

	// Token: 0x040002E1 RID: 737
	public Vector3 hover = Vector3.zero;

	// Token: 0x040002E2 RID: 738
	public Vector3 pressed = Vector3.zero;

	// Token: 0x040002E3 RID: 739
	public float duration = 0.2f;

	// Token: 0x040002E4 RID: 740
	private Quaternion mRot;

	// Token: 0x040002E5 RID: 741
	private bool mStarted;
}
