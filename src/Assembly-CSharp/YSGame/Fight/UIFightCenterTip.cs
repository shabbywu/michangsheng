using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000ABF RID: 2751
	public class UIFightCenterTip : MonoBehaviour
	{
		// Token: 0x06004D15 RID: 19733 RVA: 0x0020FE64 File Offset: 0x0020E064
		public void ShowYiSan(int count)
		{
			this.YiSanCountText.text = count.ToString();
			if (!this.isYiSan)
			{
				this.isYiSan = true;
				this.NormalTipText.gameObject.SetActive(false);
				this.YiSanObj.SetActive(true);
				this.ShowTip();
				return;
			}
			if (!this.isShow)
			{
				this.ShowTip();
			}
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0020FEC4 File Offset: 0x0020E0C4
		public void ShowNormalTip(string tip)
		{
			this.NormalTipText.text = tip;
			if (this.isYiSan)
			{
				this.isYiSan = false;
				this.NormalTipText.gameObject.SetActive(true);
				this.YiSanObj.SetActive(false);
			}
			this.ShowTip();
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0020FF04 File Offset: 0x0020E104
		private void ShowTip()
		{
			this.isShow = true;
			this.Move.position = this.StartPoint.position;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.Move, Vector2.zero, 0.5f, false), 18);
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x0020FF41 File Offset: 0x0020E141
		public void HideTip()
		{
			if (this.isShow)
			{
				this.isShow = false;
				TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(this.Move, this.EndPoint.position, 0.5f, false), 18);
			}
		}

		// Token: 0x04004C2F RID: 19503
		public RectTransform Move;

		// Token: 0x04004C30 RID: 19504
		public RectTransform StartPoint;

		// Token: 0x04004C31 RID: 19505
		public RectTransform EndPoint;

		// Token: 0x04004C32 RID: 19506
		public GameObject YiSanObj;

		// Token: 0x04004C33 RID: 19507
		public Text YiSanCountText;

		// Token: 0x04004C34 RID: 19508
		public Text NormalTipText;

		// Token: 0x04004C35 RID: 19509
		private bool isYiSan;

		// Token: 0x04004C36 RID: 19510
		private bool isShow;
	}
}
