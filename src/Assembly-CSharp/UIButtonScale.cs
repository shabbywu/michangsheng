using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x060004C4 RID: 1220 RVA: 0x00019FF1 File Offset: 0x000181F1
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mScale = this.tweenTarget.localScale;
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0001A02D File Offset: 0x0001822D
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0001A048 File Offset: 0x00018248
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.value = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0001A094 File Offset: 0x00018294
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? Vector3.Scale(this.mScale, this.pressed) : (UICamera.IsHighlighted(base.gameObject) ? Vector3.Scale(this.mScale, this.hover) : this.mScale)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0001A110 File Offset: 0x00018310
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, isOver ? Vector3.Scale(this.mScale, this.hover) : this.mScale).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0001A16B File Offset: 0x0001836B
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x040002E6 RID: 742
	public Transform tweenTarget;

	// Token: 0x040002E7 RID: 743
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x040002E8 RID: 744
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x040002E9 RID: 745
	public float duration = 0.2f;

	// Token: 0x040002EA RID: 746
	private Vector3 mScale;

	// Token: 0x040002EB RID: 747
	private bool mStarted;
}
