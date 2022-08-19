using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	// Token: 0x060004D6 RID: 1238 RVA: 0x0001A78C File Offset: 0x0001898C
	private void Awake()
	{
		if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0001A7AD File Offset: 0x000189AD
	private void OnPress(bool isPressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0001A7DE File Offset: 0x000189DE
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0001A80F File Offset: 0x00018A0F
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x040002F2 RID: 754
	public UIDraggableCamera draggableCamera;
}
