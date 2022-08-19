using System;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class SubPanelPosition : MonoBehaviour
{
	// Token: 0x060021F9 RID: 8697 RVA: 0x000E9FBD File Offset: 0x000E81BD
	private void Start()
	{
		base.Invoke("SetPanel", 0.5f);
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x000E9FD0 File Offset: 0x000E81D0
	private void SetPanel()
	{
		this.parent = base.transform.parent;
		this.child = base.transform.GetChild(0);
		this.PanelScript = base.transform.GetComponent<UIPanel>();
		base.transform.parent = null;
		this.child.parent = null;
		if (this.screenDirection == SubPanelPosition.ScreenDirection.vertical)
		{
			this.rateX = (float)Screen.width / this.size;
			this.rateY = 1f;
			this.ScaleSize = base.transform.localScale.y;
		}
		else if (this.screenDirection == SubPanelPosition.ScreenDirection.horizontal)
		{
			this.rateX = 1f;
			this.rateY = (float)Screen.height / this.size;
			this.ScaleSize = base.transform.localScale.x;
		}
		base.transform.localScale = new Vector4(this.ScaleSize, this.ScaleSize, this.ScaleSize, this.ScaleSize);
		base.transform.parent = this.parent;
		this.child.parent = base.transform;
		this.PanelScript.clipRange = new Vector4(this.PanelScript.clipRange.x, this.PanelScript.clipRange.y, this.PanelScript.clipRange.z * this.rateX, this.PanelScript.clipRange.w * this.rateY);
	}

	// Token: 0x04001B5F RID: 7007
	public SubPanelPosition.ScreenDirection screenDirection;

	// Token: 0x04001B60 RID: 7008
	public float size;

	// Token: 0x04001B61 RID: 7009
	private Transform parent;

	// Token: 0x04001B62 RID: 7010
	private Transform child;

	// Token: 0x04001B63 RID: 7011
	private float ScaleSize;

	// Token: 0x04001B64 RID: 7012
	private float rateX;

	// Token: 0x04001B65 RID: 7013
	private float rateY;

	// Token: 0x04001B66 RID: 7014
	private UIPanel PanelScript;

	// Token: 0x02001398 RID: 5016
	public enum ScreenDirection
	{
		// Token: 0x040068D8 RID: 26840
		horizontal,
		// Token: 0x040068D9 RID: 26841
		vertical
	}
}
