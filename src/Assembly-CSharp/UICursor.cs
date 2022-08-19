using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x06000406 RID: 1030 RVA: 0x0001677B File Offset: 0x0001497B
	private void Awake()
	{
		UICursor.instance = this;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00016783 File Offset: 0x00014983
	private void OnDestroy()
	{
		UICursor.instance = null;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0001678C File Offset: 0x0001498C
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

	// Token: 0x06000409 RID: 1033 RVA: 0x00016824 File Offset: 0x00014A24
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

	// Token: 0x0600040A RID: 1034 RVA: 0x0001693C File Offset: 0x00014B3C
	public static void Clear()
	{
		if (UICursor.instance != null && UICursor.instance.mSprite != null)
		{
			UICursor.Set(UICursor.instance.mAtlas, UICursor.instance.mSpriteName);
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00016978 File Offset: 0x00014B78
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

	// Token: 0x04000232 RID: 562
	public static UICursor instance;

	// Token: 0x04000233 RID: 563
	public Camera uiCamera;

	// Token: 0x04000234 RID: 564
	private Transform mTrans;

	// Token: 0x04000235 RID: 565
	private UISprite mSprite;

	// Token: 0x04000236 RID: 566
	private UIAtlas mAtlas;

	// Token: 0x04000237 RID: 567
	private string mSpriteName;
}
