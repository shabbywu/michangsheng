using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x00008606 File Offset: 0x00006806
	protected override void OnEnable()
	{
		this.Upgrade();
		base.OnEnable();
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00070928 File Offset: 0x0006EB28
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

	// Token: 0x04000348 RID: 840
	public UIButtonKeys selectOnClick;

	// Token: 0x04000349 RID: 841
	public UIButtonKeys selectOnUp;

	// Token: 0x0400034A RID: 842
	public UIButtonKeys selectOnDown;

	// Token: 0x0400034B RID: 843
	public UIButtonKeys selectOnLeft;

	// Token: 0x0400034C RID: 844
	public UIButtonKeys selectOnRight;
}
