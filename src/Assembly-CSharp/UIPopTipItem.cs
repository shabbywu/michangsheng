using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004E2 RID: 1250
public class UIPopTipItem : MonoBehaviour
{
	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06002095 RID: 8341 RVA: 0x0001AC9F File Offset: 0x00018E9F
	// (set) Token: 0x06002096 RID: 8342 RVA: 0x0001ACA7 File Offset: 0x00018EA7
	public int MsgIndex
	{
		get
		{
			return this.msgIndex;
		}
		set
		{
			this.msgIndex = value;
			if (this.msgIndex >= 9)
			{
				Object.Destroy(base.gameObject);
				return;
			}
			this.MovePos();
		}
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x0001ACCC File Offset: 0x00018ECC
	private void Awake()
	{
		this.RT = (base.transform as RectTransform);
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x001138F0 File Offset: 0x00111AF0
	private void MovePos()
	{
		if (this.MsgIndex == 0)
		{
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.RT, Vector3.one, UIPopTipItem.tweenDur), 18);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.RT, 0f, UIPopTipItem.tweenDur, false), 18);
			return;
		}
		float num = -(57f + (float)(this.MsgIndex - 1) * (57f * UIPopTipItem.scaleNum));
		ShortcutExtensions.DOScale(this.RT, new Vector3(UIPopTipItem.scaleNum, UIPopTipItem.scaleNum, 1f), UIPopTipItem.tweenDur);
		DOTweenModuleUI.DOAnchorPosY(this.RT, num, UIPopTipItem.tweenDur, false);
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x0001ACDF File Offset: 0x00018EDF
	public void TweenDestory()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.RT, 500f, 1f, false), 14).onComplete = delegate()
		{
			Object.Destroy(base.gameObject);
		};
	}

	// Token: 0x04001C15 RID: 7189
	public Image IconImage;

	// Token: 0x04001C16 RID: 7190
	public Text MsgText;

	// Token: 0x04001C17 RID: 7191
	private int msgIndex;

	// Token: 0x04001C18 RID: 7192
	private RectTransform RT;

	// Token: 0x04001C19 RID: 7193
	private static float tweenDur = 0.5f;

	// Token: 0x04001C1A RID: 7194
	private static float scaleNum = 0.9f;
}
