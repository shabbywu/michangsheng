using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x0600050B RID: 1291 RVA: 0x00008783 File Offset: 0x00006983
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

	// Token: 0x0600050C RID: 1292 RVA: 0x000087BF File Offset: 0x000069BF
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00070C08 File Offset: 0x0006EE08
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

	// Token: 0x0600050E RID: 1294 RVA: 0x00070C54 File Offset: 0x0006EE54
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

	// Token: 0x0600050F RID: 1295 RVA: 0x00070CDC File Offset: 0x0006EEDC
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

	// Token: 0x06000510 RID: 1296 RVA: 0x000087DA File Offset: 0x000069DA
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0400035F RID: 863
	public Transform tweenTarget;

	// Token: 0x04000360 RID: 864
	public Vector3 hover = Vector3.zero;

	// Token: 0x04000361 RID: 865
	public Vector3 pressed = Vector3.zero;

	// Token: 0x04000362 RID: 866
	public float duration = 0.2f;

	// Token: 0x04000363 RID: 867
	private Quaternion mRot;

	// Token: 0x04000364 RID: 868
	private bool mStarted;
}
