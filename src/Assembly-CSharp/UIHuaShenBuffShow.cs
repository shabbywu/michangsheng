using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000394 RID: 916
public class UIHuaShenBuffShow : MonoBehaviour
{
	// Token: 0x06001E24 RID: 7716 RVA: 0x000D4C8F File Offset: 0x000D2E8F
	private void Awake()
	{
		this.showHpDamage = base.GetComponent<AvatarShowHpDamage>();
		this.NumText.text = "0";
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x000D4CB0 File Offset: 0x000D2EB0
	public void SetNumber(int num)
	{
		if (num != this.nowNum)
		{
			int num2 = num - this.nowNum;
			this.showHpDamage.show(-num2, 0);
			this.nowNum = num;
			this.NumText.text = num.ToString();
			if (this.ShowType == UIHuaShenBuffShow.UIHuaShenBuffShowType.变换高度)
			{
				float num3 = (0f - this.zeroHeight) / 100f * (float)num + this.zeroHeight;
				DOTweenModuleUI.DOAnchorPosY(this.EffectImage.rectTransform, num3, 1f, false);
				return;
			}
			TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(this.EffectImage, Color.white, 1f), 18).onComplete = delegate()
			{
				TweenSettingsExtensions.SetEase<TweenerCore<Color, Color, ColorOptions>>(DOTweenModuleUI.DOColor(this.EffectImage, new Color(1f, 1f, 1f, 0f), 1f), 20);
			};
		}
	}

	// Token: 0x040018BC RID: 6332
	public Text NumText;

	// Token: 0x040018BD RID: 6333
	public Image EffectImage;

	// Token: 0x040018BE RID: 6334
	public UIHuaShenBuffShow.UIHuaShenBuffShowType ShowType;

	// Token: 0x040018BF RID: 6335
	private float zeroHeight = -117f;

	// Token: 0x040018C0 RID: 6336
	private int nowNum;

	// Token: 0x040018C1 RID: 6337
	private AvatarShowHpDamage showHpDamage;

	// Token: 0x02001361 RID: 4961
	public enum UIHuaShenBuffShowType
	{
		// Token: 0x0400683C RID: 26684
		变换高度,
		// Token: 0x0400683D RID: 26685
		闪烁
	}
}
