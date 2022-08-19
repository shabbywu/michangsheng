using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
	// Token: 0x060004AA RID: 1194 RVA: 0x000199BF File Offset: 0x00017BBF
	protected override void OnEnable()
	{
		this.Upgrade();
		base.OnEnable();
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x000199D0 File Offset: 0x00017BD0
	public void Upgrade()
	{
		if (this.onClick == null && this.selectOnClick != null)
		{
			this.onClick = this.selectOnClick.gameObject;
			this.selectOnClick = null;
			NGUITools.SetDirty(this);
		}
		if (this.onLeft == null && this.selectOnLeft != null)
		{
			this.onLeft = this.selectOnLeft.gameObject;
			this.selectOnLeft = null;
			NGUITools.SetDirty(this);
		}
		if (this.onRight == null && this.selectOnRight != null)
		{
			this.onRight = this.selectOnRight.gameObject;
			this.selectOnRight = null;
			NGUITools.SetDirty(this);
		}
		if (this.onUp == null && this.selectOnUp != null)
		{
			this.onUp = this.selectOnUp.gameObject;
			this.selectOnUp = null;
			NGUITools.SetDirty(this);
		}
		if (this.onDown == null && this.selectOnDown != null)
		{
			this.onDown = this.selectOnDown.gameObject;
			this.selectOnDown = null;
			NGUITools.SetDirty(this);
		}
	}

	// Token: 0x040002D0 RID: 720
	public UIButtonKeys selectOnClick;

	// Token: 0x040002D1 RID: 721
	public UIButtonKeys selectOnUp;

	// Token: 0x040002D2 RID: 722
	public UIButtonKeys selectOnDown;

	// Token: 0x040002D3 RID: 723
	public UIButtonKeys selectOnLeft;

	// Token: 0x040002D4 RID: 724
	public UIButtonKeys selectOnRight;
}
