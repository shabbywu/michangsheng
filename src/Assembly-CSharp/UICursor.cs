using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x0600044E RID: 1102 RVA: 0x00007C97 File Offset: 0x00005E97
	private void Awake()
	{
		UICursor.instance = this;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00007C9F File Offset: 0x00005E9F
	private void OnDestroy()
	{
		UICursor.instance = null;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mSprite != null)
		{
			this.mAtlas = this.mSprite.atlas;
			this.mSpriteName = this.mSprite.spriteName;
			if (this.mSprite.depth < 100)
			{
				this.mSprite.depth = 100;
			}
		}
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0006E03C File Offset: 0x0006C23C
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.orthographic)
			{
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
				return;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			this.mTrans.localPosition = mousePosition;
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00007CA7 File Offset: 0x00005EA7
	public static void Clear()
	{
		if (UICursor.instance != null && UICursor.instance.mSprite != null)
		{
			UICursor.Set(UICursor.instance.mAtlas, UICursor.instance.mSpriteName);
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0006E154 File Offset: 0x0006C354
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.instance != null && UICursor.instance.mSprite)
		{
			UICursor.instance.mSprite.atlas = atlas;
			UICursor.instance.mSprite.spriteName = sprite;
			UICursor.instance.mSprite.MakePixelPerfect();
			UICursor.instance.Update();
		}
	}

	// Token: 0x04000278 RID: 632
	public static UICursor instance;

	// Token: 0x04000279 RID: 633
	public Camera uiCamera;

	// Token: 0x0400027A RID: 634
	private Transform mTrans;

	// Token: 0x0400027B RID: 635
	private UISprite mSprite;

	// Token: 0x0400027C RID: 636
	private UIAtlas mAtlas;

	// Token: 0x0400027D RID: 637
	private string mSpriteName;
}
