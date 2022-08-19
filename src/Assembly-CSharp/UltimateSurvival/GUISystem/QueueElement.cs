using System;
using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000648 RID: 1608
	public class QueueElement : MonoBehaviour
	{
		// Token: 0x0600333B RID: 13115 RVA: 0x00168510 File Offset: 0x00166710
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

		// Token: 0x0600333C RID: 13116 RVA: 0x001685D1 File Offset: 0x001667D1
		public void StartCrafting()
		{
			if (!this.m_Initialized)
			{
				return;
			}
			base.StartCoroutine(this.C_Update(this.m_CraftData, this.m_Inventory));
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x001685F8 File Offset: 0x001667F8
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

		// Token: 0x0600333E RID: 13118 RVA: 0x00168665 File Offset: 0x00166865
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

		// Token: 0x04002D6F RID: 11631
		public Message<QueueElement> Cancel = new Message<QueueElement>();

		// Token: 0x04002D70 RID: 11632
		public Message Complete = new Message();

		// Token: 0x04002D71 RID: 11633
		[SerializeField]
		private Image m_Icon;

		// Token: 0x04002D72 RID: 11634
		[SerializeField]
		private Text m_RemainedTime;

		// Token: 0x04002D73 RID: 11635
		[SerializeField]
		private Image m_ProgressBar;

		// Token: 0x04002D74 RID: 11636
		private CraftData m_CraftData;

		// Token: 0x04002D75 RID: 11637
		private ItemData m_Result;

		// Token: 0x04002D76 RID: 11638
		private ItemContainer m_Inventory;

		// Token: 0x04002D77 RID: 11639
		private int m_AmountToCraftRemained;

		// Token: 0x04002D78 RID: 11640
		private bool m_Initialized;
	}
}
