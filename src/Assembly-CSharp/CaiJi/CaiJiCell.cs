using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace CaiJi
{
	// Token: 0x02000733 RID: 1843
	public class CaiJiCell : MonoBehaviour
	{
		// Token: 0x06003AB5 RID: 15029 RVA: 0x00193FCC File Offset: 0x001921CC
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

		// Token: 0x06003AB6 RID: 15030 RVA: 0x00194024 File Offset: 0x00192224
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

		// Token: 0x06003AB7 RID: 15031 RVA: 0x00194074 File Offset: 0x00192274
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

		// Token: 0x06003AB8 RID: 15032 RVA: 0x001940C1 File Offset: 0x001922C1
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

		// Token: 0x06003AB9 RID: 15033 RVA: 0x001940FB File Offset: 0x001922FB
		public void Show()
		{
			base.gameObject.SetActive(true);
			this.PlayerShowEffect();
			this.IsSelected = false;
		}

		// Token: 0x040032E6 RID: 13030
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x040032E7 RID: 13031
		public UIIconShow Item;

		// Token: 0x040032E8 RID: 13032
		private bool CanClick = true;

		// Token: 0x040032E9 RID: 13033
		private Sequence StopQuence;

		// Token: 0x040032EA RID: 13034
		private MessageData data = new MessageData(0);

		// Token: 0x040032EB RID: 13035
		public bool IsSelected;
	}
}
