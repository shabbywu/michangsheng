using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x060008DC RID: 2268 RVA: 0x0000B38E File Offset: 0x0000958E
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0000B3A2 File Offset: 0x000095A2
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x0400062D RID: 1581
	public Color color = Color.white;

	// Token: 0x0400062E RID: 1582
	private UIWidget mWidget;
}
