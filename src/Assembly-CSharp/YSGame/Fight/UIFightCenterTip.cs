using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000DFD RID: 3581
	public class UIFightCenterTip : MonoBehaviour
	{
		// Token: 0x06005664 RID: 22116 RVA: 0x00240634 File Offset: 0x0023E834
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

		// Token: 0x06005665 RID: 22117 RVA: 0x0003DBD6 File Offset: 0x0003BDD6
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

		// Token: 0x06005666 RID: 22118 RVA: 0x0003DC16 File Offset: 0x0003BE16
		private void ShowTip()
		{
			this.isShow = true;
			this.Move.position = this.StartPoint.position;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTweenModuleUI.DOAnchorPos(this.Move, Vector2.zero, 0.5f, false), 18);
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0003DC53 File Offset: 0x0003BE53
		public void HideTip()
		{
			if (this.isShow)
			{
				this.isShow = false;
				TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(this.Move, this.EndPoint.position, 0.5f, false), 18);
			}
		}

		// Token: 0x04005609 RID: 22025
		public RectTransform Move;

		// Token: 0x0400560A RID: 22026
		public RectTransform StartPoint;

		// Token: 0x0400560B RID: 22027
		public RectTransform EndPoint;

		// Token: 0x0400560C RID: 22028
		public GameObject YiSanObj;

		// Token: 0x0400560D RID: 22029
		public Text YiSanCountText;

		// Token: 0x0400560E RID: 22030
		public Text NormalTipText;

		// Token: 0x0400560F RID: 22031
		private bool isYiSan;

		// Token: 0x04005610 RID: 22032
		private bool isShow;
	}
}
