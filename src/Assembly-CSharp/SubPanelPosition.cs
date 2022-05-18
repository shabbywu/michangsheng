using System;
using UnityEngine;

// Token: 0x020005DB RID: 1499
public class SubPanelPosition : MonoBehaviour
{
	// Token: 0x060025B3 RID: 9651 RVA: 0x0001E2F2 File Offset: 0x0001C4F2
	private void Start()
	{
		base.Invoke("SetPanel", 0.5f);
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x0012B3A8 File Offset: 0x001295A8
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

	// Token: 0x04002025 RID: 8229
	public SubPanelPosition.ScreenDirection screenDirection;

	// Token: 0x04002026 RID: 8230
	public float size;

	// Token: 0x04002027 RID: 8231
	private Transform parent;

	// Token: 0x04002028 RID: 8232
	private Transform child;

	// Token: 0x04002029 RID: 8233
	private float ScaleSize;

	// Token: 0x0400202A RID: 8234
	private float rateX;

	// Token: 0x0400202B RID: 8235
	private float rateY;

	// Token: 0x0400202C RID: 8236
	private UIPanel PanelScript;

	// Token: 0x020005DC RID: 1500
	public enum ScreenDirection
	{
		// Token: 0x0400202E RID: 8238
		horizontal,
		// Token: 0x0400202F RID: 8239
		vertical
	}
}
