using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200051B RID: 1307
public class UIHuaShenBuffShow : MonoBehaviour
{
	// Token: 0x060021A3 RID: 8611 RVA: 0x0001BA61 File Offset: 0x00019C61
	private void Awake()
	{
		this.showHpDamage = base.GetComponent<AvatarShowHpDamage>();
		this.NumText.text = "0";
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x001187E0 File Offset: 0x001169E0
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

	// Token: 0x04001D20 RID: 7456
	public Text NumText;

	// Token: 0x04001D21 RID: 7457
	public Image EffectImage;

	// Token: 0x04001D22 RID: 7458
	public UIHuaShenBuffShow.UIHuaShenBuffShowType ShowType;

	// Token: 0x04001D23 RID: 7459
	private float zeroHeight = -117f;

	// Token: 0x04001D24 RID: 7460
	private int nowNum;

	// Token: 0x04001D25 RID: 7461
	private AvatarShowHpDamage showHpDamage;

	// Token: 0x0200051C RID: 1308
	public enum UIHuaShenBuffShowType
	{
		// Token: 0x04001D27 RID: 7463
		变换高度,
		// Token: 0x04001D28 RID: 7464
		闪烁
	}
}
