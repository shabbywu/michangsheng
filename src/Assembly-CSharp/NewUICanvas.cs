using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A8 RID: 936
public class NewUICanvas : MonoBehaviour
{
	// Token: 0x06001E78 RID: 7800 RVA: 0x000D6540 File Offset: 0x000D4740
	private void Awake()
	{
		NewUICanvas.Inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
		base.transform.position = new Vector3(-8000f, 0f, 0f);
		this.Canvas = base.GetComponent<Canvas>();
		this.scaler = base.GetComponent<CanvasScaler>();
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x000D6598 File Offset: 0x000D4798
	private void Update()
	{
		float num = (float)Screen.height / (float)Screen.width;
		this.scaler.referenceResolution = new Vector2(1080f / num, 1080f);
		if (PlayerEx.Player == null)
		{
			this.Canvas.worldCamera = null;
			return;
		}
		this.Canvas.worldCamera = this.Camera;
	}

	// Token: 0x040018F8 RID: 6392
	public static NewUICanvas Inst;

	// Token: 0x040018F9 RID: 6393
	[HideInInspector]
	public Canvas Canvas;

	// Token: 0x040018FA RID: 6394
	private CanvasScaler scaler;

	// Token: 0x040018FB RID: 6395
	public Camera Camera;
}
