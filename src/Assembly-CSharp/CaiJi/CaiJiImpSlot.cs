using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace CaiJi
{
	// Token: 0x02000A96 RID: 2710
	public class CaiJiImpSlot : MonoBehaviour
	{
		// Token: 0x06004583 RID: 17795 RVA: 0x00031B5F File Offset: 0x0002FD5F
		private void Awake()
		{
			this.Btn.mouseUpEvent.AddListener(new UnityAction(this.CancelSelectItem));
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00031B7D File Offset: 0x0002FD7D
		public void PutItem(int itemId)
		{
			this.Item.SetItem(itemId);
			this.Item.CanDrag = false;
			this.Item.gameObject.SetActive(true);
			this.IsNull = false;
			this.PlayerShowEffect();
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x001DBE44 File Offset: 0x001DA044
		public void CancelSelectItem()
		{
			this.IsNull = true;
			this.Item.CloseTooltip();
			this.PlayerHideEffect();
			this.data.valueInt = this.Item.tmpItem.itemID;
			MessageMag.Instance.Send("CaiJi_Item_Cancel", this.data);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x001DBE9C File Offset: 0x001DA09C
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

		// Token: 0x06004587 RID: 17799 RVA: 0x001DBEEC File Offset: 0x001DA0EC
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

		// Token: 0x04003DA5 RID: 15781
		public bool IsNull = true;

		// Token: 0x04003DA6 RID: 15782
		[SerializeField]
		private UIIconShow Item;

		// Token: 0x04003DA7 RID: 15783
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x04003DA8 RID: 15784
		private MessageData data = new MessageData(0);
	}
}
