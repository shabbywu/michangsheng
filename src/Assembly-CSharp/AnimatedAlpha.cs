using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06000825 RID: 2085 RVA: 0x00031ACB File Offset: 0x0002FCCB
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00031AEB File Offset: 0x0002FCEB
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

	// Token: 0x04000507 RID: 1287
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x04000508 RID: 1288
	private UIWidget mWidget;

	// Token: 0x04000509 RID: 1289
	private UIPanel mPanel;
}
