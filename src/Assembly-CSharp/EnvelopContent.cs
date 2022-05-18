using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Envelop Content")]
public class EnvelopContent : MonoBehaviour
{
	// Token: 0x06000498 RID: 1176 RVA: 0x0000806A File Offset: 0x0000626A
	private void Start()
	{
		this.mStarted = true;
		this.Execute();
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00008079 File Offset: 0x00006279
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Execute();
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0006F090 File Offset: 0x0006D290
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

	// Token: 0x040002DE RID: 734
	public Transform targetRoot;

	// Token: 0x040002DF RID: 735
	public int padLeft;

	// Token: 0x040002E0 RID: 736
	public int padRight;

	// Token: 0x040002E1 RID: 737
	public int padBottom;

	// Token: 0x040002E2 RID: 738
	public int padTop;

	// Token: 0x040002E3 RID: 739
	private bool mStarted;
}
