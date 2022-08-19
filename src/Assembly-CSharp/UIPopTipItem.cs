using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000365 RID: 869
public class UIPopTipItem : MonoBehaviour
{
	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000CEF5F File Offset: 0x000CD15F
	// (set) Token: 0x06001D2C RID: 7468 RVA: 0x000CEF67 File Offset: 0x000CD167
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

	// Token: 0x06001D2D RID: 7469 RVA: 0x000CEF8C File Offset: 0x000CD18C
	private void Awake()
	{
		this.RT = (base.transform as RectTransform);
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x000CEFA0 File Offset: 0x000CD1A0
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

	// Token: 0x06001D2F RID: 7471 RVA: 0x000CF046 File Offset: 0x000CD246
	public void TweenDestory()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPosX(this.RT, 500f, 1f, false), 14).onComplete = delegate()
		{
			Object.Destroy(base.gameObject);
		};
	}

	// Token: 0x040017C3 RID: 6083
	public Image IconImage;

	// Token: 0x040017C4 RID: 6084
	public Text MsgText;

	// Token: 0x040017C5 RID: 6085
	private int msgIndex;

	// Token: 0x040017C6 RID: 6086
	private RectTransform RT;

	// Token: 0x040017C7 RID: 6087
	private static float tweenDur = 0.5f;

	// Token: 0x040017C8 RID: 6088
	private static float scaleNum = 0.9f;
}
