using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000532 RID: 1330
public class NewUICanvas : MonoBehaviour
{
	// Token: 0x060021FB RID: 8699 RVA: 0x00119C9C File Offset: 0x00117E9C
	private void Awake()
	{
		NewUICanvas.Inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
		base.transform.position = new Vector3(-8000f, 0f, 0f);
		this.Canvas = base.GetComponent<Canvas>();
		this.scaler = base.GetComponent<CanvasScaler>();
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x00119CF4 File Offset: 0x00117EF4
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

	// Token: 0x04001D65 RID: 7525
	public static NewUICanvas Inst;

	// Token: 0x04001D66 RID: 7526
	[HideInInspector]
	public Canvas Canvas;

	// Token: 0x04001D67 RID: 7527
	private CanvasScaler scaler;

	// Token: 0x04001D68 RID: 7528
	public Camera Camera;
}
