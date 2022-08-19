using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace CaiJi
{
	// Token: 0x02000734 RID: 1844
	public class CaiJiImpSlot : MonoBehaviour
	{
		// Token: 0x06003AC5 RID: 15045 RVA: 0x0019433A File Offset: 0x0019253A
		private void Awake()
		{
			this.Btn.mouseUpEvent.AddListener(new UnityAction(this.CancelSelectItem));
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x00194358 File Offset: 0x00192558
		public void PutItem(int itemId)
		{
			this.Item.SetItem(itemId);
			this.Item.CanDrag = false;
			this.Item.gameObject.SetActive(true);
			this.IsNull = false;
			this.PlayerShowEffect();
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x00194390 File Offset: 0x00192590
		public void CancelSelectItem()
		{
			this.IsNull = true;
			this.Item.CloseTooltip();
			this.PlayerHideEffect();
			this.data.valueInt = this.Item.tmpItem.itemID;
			MessageMag.Instance.Send("CaiJi_Item_Cancel", this.data);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x001943E8 File Offset: 0x001925E8
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

		// Token: 0x06003AC9 RID: 15049 RVA: 0x00194438 File Offset: 0x00192638
		private void PlayerHideEffect()
		{
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, new Vector2(0.95f, 0.95f), 0.05f), delegate()
			{
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.Item.gameObject.transform, Vector2.zero, 0f), delegate()
				{
					this.Item.gameObject.SetActive(false);
				});
			});
		}

		// Token: 0x040032EC RID: 13036
		public bool IsNull = true;

		// Token: 0x040032ED RID: 13037
		[SerializeField]
		private UIIconShow Item;

		// Token: 0x040032EE RID: 13038
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x040032EF RID: 13039
		private MessageData data = new MessageData(0);
	}
}
