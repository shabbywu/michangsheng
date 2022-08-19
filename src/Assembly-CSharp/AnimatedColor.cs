using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x06000828 RID: 2088 RVA: 0x00031B3E File Offset: 0x0002FD3E
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00031B52 File Offset: 0x0002FD52
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x0400050A RID: 1290
	public Color color = Color.white;

	// Token: 0x0400050B RID: 1291
	private UIWidget mWidget;
}
