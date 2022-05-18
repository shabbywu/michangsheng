using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace CaiJi
{
	// Token: 0x02000A95 RID: 2709
	public class CaiJiCell : MonoBehaviour
	{
		// Token: 0x06004573 RID: 17779 RVA: 0x001DBC54 File Offset: 0x001D9E54
		public CaiJiCell Init(int itemId)
		{
			this.Item.SetItem(itemId);
			this.data.valueInt = itemId;
			base.gameObject.SetActive(true);
			this.IsSelected = false;
			this.Btn.mouseUpEvent.AddListener(delegate()
			{
				if (!CaiJiUIMag.inst.IsMax)
				{
					this.PlayerHideEffect();
					MessageMag.Instance.Send("CaiJi_Item_Select", this.data);
					this.IsSelected = true;
					UToolTip.CloseOldTooltip();
					return;
				}
				if (!this.CanClick)
				{
					return;
				}
				this.CanClick = false;
				this.PlayerSharkeEffect();
			});
			return this;
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x001DBCAC File Offset: 0x001D9EAC
		private void PlayerShowEffect()
		{
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, new Vector2(0.94f, 0.94f), 0.05f), delegate()
			{
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, new Vector2(1.05f, 1.05f), 0.05f), delegate()
				{
					TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, new Vector2(0.98f, 0.98f), 0.05f), delegate()
					{
						ShortcutExtensions.DOScale(this.Item.gameObject.transform, 1f, 0.05f);
					});
				});
			});
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x001DBCFC File Offset: 0x001D9EFC
		private void PlayerHideEffect()
		{
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, new Vector2(0.95f, 0.95f), 0.05f), delegate()
			{
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, Vector2.zero, 0f), delegate()
				{
					base.gameObject.SetActive(false);
				});
			});
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x000319DE File Offset: 0x0002FBDE
		private void PlayerSharkeEffect()
		{
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(this.Item.gameObject.transform, -10f, 0.005f, false), 2), delegate()
			{
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(this.Item.gameObject.transform, 9f, 0.1f, false), 2), delegate()
				{
					TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(this.Item.gameObject.transform, -2f, 0.05f, false), 2), delegate()
					{
						TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(this.Item.gameObject.transform, 0f, 0.025f, false), 2), delegate()
						{
							this.CanClick = true;
						});
					});
				});
			});
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00031A18 File Offset: 0x0002FC18
		public void Show()
		{
			base.gameObject.SetActive(true);
			this.PlayerShowEffect();
			this.IsSelected = false;
		}

		// Token: 0x04003D9F RID: 15775
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x04003DA0 RID: 15776
		public UIIconShow Item;

		// Token: 0x04003DA1 RID: 15777
		private bool CanClick = true;

		// Token: 0x04003DA2 RID: 15778
		private Sequence StopQuence;

		// Token: 0x04003DA3 RID: 15779
		private MessageData data = new MessageData(0);

		// Token: 0x04003DA4 RID: 15780
		public bool IsSelected;
	}
}
