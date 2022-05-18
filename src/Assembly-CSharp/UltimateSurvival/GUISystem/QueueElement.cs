using System;
using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000942 RID: 2370
	public class QueueElement : MonoBehaviour
	{
		// Token: 0x06003C97 RID: 15511 RVA: 0x001B13EC File Offset: 0x001AF5EC
		public void Initialize(CraftData craftData, ItemContainer inventory)
		{
			if (this.m_Initialized)
			{
				return;
			}
			foreach (RequiredItem requiredItem in craftData.Result.Recipe.RequiredItems)
			{
			}
			this.m_Result = craftData.Result;
			this.m_Icon.sprite = this.m_Result.Icon;
			this.m_RemainedTime.text = string.Format("{0}s (x{1})", craftData.Result.Recipe.Duration, craftData.Amount);
			this.m_ProgressBar.fillAmount = 0f;
			this.m_AmountToCraftRemained = craftData.Amount;
			this.m_CraftData = craftData;
			this.m_Inventory = inventory;
			this.m_Initialized = true;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x0002BBD7 File Offset: 0x00029DD7
		public void StartCrafting()
		{
			if (!this.m_Initialized)
			{
				return;
			}
			base.StartCoroutine(this.C_Update(this.m_CraftData, this.m_Inventory));
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x001B14B0 File Offset: 0x001AF6B0
		public void CancelCrafting()
		{
			if (this.m_AmountToCraftRemained > 0)
			{
				foreach (RequiredItem requiredItem in this.m_CraftData.Result.Recipe.RequiredItems)
				{
					ItemData itemData;
					MonoSingleton<InventoryController>.Instance.Database.FindItemByName(requiredItem.Name, out itemData);
				}
			}
			this.Cancel.Send(this);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x0002BBFB File Offset: 0x00029DFB
		private IEnumerator C_Update(CraftData craftData, ItemContainer inventory)
		{
			int oldm_AmountToCraftRemained = this.m_AmountToCraftRemained;
			while (this.m_AmountToCraftRemained > 0)
			{
				float remainedTime = (float)craftData.Result.Recipe.Duration;
				while (remainedTime > 0f)
				{
					yield return null;
					remainedTime = Mathf.Clamp(remainedTime - Time.deltaTime, 0f, float.PositiveInfinity);
					this.m_RemainedTime.text = string.Format("{0}s (x{1})", Mathf.Ceil(remainedTime), this.m_AmountToCraftRemained);
					this.m_ProgressBar.fillAmount = 1f - remainedTime / (float)craftData.Result.Recipe.Duration;
				}
				this.m_AmountToCraftRemained--;
				this.m_ProgressBar.fillAmount = 0f;
				this.m_RemainedTime.text = string.Format("{0}s (x{1})", 0, this.m_AmountToCraftRemained);
			}
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar != null)
			{
				avatar.StartCrafting(this.m_Result.Id, oldm_AmountToCraftRemained);
			}
			this.Complete.Send();
			Object.Destroy(base.gameObject);
			yield break;
		}

		// Token: 0x040036D8 RID: 14040
		public Message<QueueElement> Cancel = new Message<QueueElement>();

		// Token: 0x040036D9 RID: 14041
		public Message Complete = new Message();

		// Token: 0x040036DA RID: 14042
		[SerializeField]
		private Image m_Icon;

		// Token: 0x040036DB RID: 14043
		[SerializeField]
		private Text m_RemainedTime;

		// Token: 0x040036DC RID: 14044
		[SerializeField]
		private Image m_ProgressBar;

		// Token: 0x040036DD RID: 14045
		private CraftData m_CraftData;

		// Token: 0x040036DE RID: 14046
		private ItemData m_Result;

		// Token: 0x040036DF RID: 14047
		private ItemContainer m_Inventory;

		// Token: 0x040036E0 RID: 14048
		private int m_AmountToCraftRemained;

		// Token: 0x040036E1 RID: 14049
		private bool m_Initialized;
	}
}
