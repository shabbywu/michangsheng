using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Envelop Content")]
public class EnvelopContent : MonoBehaviour
{
	// Token: 0x0600044A RID: 1098 RVA: 0x00017B89 File Offset: 0x00015D89
	private void Start()
	{
		this.mStarted = true;
		this.Execute();
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00017B98 File Offset: 0x00015D98
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Execute();
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00017BA8 File Offset: 0x00015DA8
	[ContextMenu("Execute")]
	public void Execute()
	{
		if (this.targetRoot == base.transform)
		{
			Debug.LogError("Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", this);
			return;
		}
		if (NGUITools.IsChild(this.targetRoot, base.transform))
		{
			Debug.LogError("Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", this);
			return;
		}
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.transform.parent, this.targetRoot, false);
		float num = bounds.min.x + (float)this.padLeft;
		float num2 = bounds.min.y + (float)this.padBottom;
		float num3 = bounds.max.x + (float)this.padRight;
		float num4 = bounds.max.y + (float)this.padTop;
		base.GetComponent<UIWidget>().SetRect(num, num2, num3 - num, num4 - num2);
		base.BroadcastMessage("UpdateAnchors", 1);
	}

	// Token: 0x0400026E RID: 622
	public Transform targetRoot;

	// Token: 0x0400026F RID: 623
	public int padLeft;

	// Token: 0x04000270 RID: 624
	public int padRight;

	// Token: 0x04000271 RID: 625
	public int padBottom;

	// Token: 0x04000272 RID: 626
	public int padTop;

	// Token: 0x04000273 RID: 627
	private bool mStarted;
}
