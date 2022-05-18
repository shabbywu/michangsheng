using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x060008D9 RID: 2265 RVA: 0x0000B31B File Offset: 0x0000951B
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0000B33B File Offset: 0x0000953B
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x0400062A RID: 1578
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x0400062B RID: 1579
	private UIWidget mWidget;

	// Token: 0x0400062C RID: 1580
	private UIPanel mPanel;
}
