using System;
using System.Collections.Generic;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008E2 RID: 2274
	public class MineableObject : MonoBehaviour
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x0002A75C File Offset: 0x0002895C
		public float Health
		{
			get
			{
				return this.m_CurrentHealth;
			}
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x001A80DC File Offset: 0x001A62DC
		public void OnToolHit(FPTool.ToolPurpose[] toolPurposes, float damage, float efficiency)
		{
			bool flag = false;
			for (int i = 0; i < toolPurposes.Length; i++)
			{
				if (toolPurposes[i] == this.m_RequiredToolPurpose)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.ReceiveDamage(damage);
				if (this.m_Loot.Length != 0)
				{
					this.GiveLootToPlayer(efficiency);
				}
			}
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x001A8124 File Offset: 0x001A6324
		private void GiveLootToPlayer(float amountFactor)
		{
			int num = ProbabilityUtils.RandomChoiceFollowingDistribution(this.GetLootProbabilities());
			LootItem lootItem = this.m_Loot[num];
			int num2;
			lootItem.AddToInventory(out num2, amountFactor);
			string arg = ColorUtils.ColorToHex(this.m_LootNameColor);
			if (this.m_ShowGatherMessage && num2 > 0)
			{
				MonoSingleton<MessageDisplayer>.Instance.PushMessage(string.Format("Gathered <color={0}>{1}</color> x {2}", arg, lootItem.ItemName, num2), this.m_MessageColor, 16);
			}
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x0002A764 File Offset: 0x00028964
		private void ReceiveDamage(float damage)
		{
			this.m_CurrentHealth += -damage * (1f - this.m_Resistance);
			if (this.m_CurrentHealth < Mathf.Epsilon)
			{
				this.DestroyObject();
			}
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x001A8198 File Offset: 0x001A6398
		private void DestroyObject()
		{
			if (this.m_DestroyedObject)
			{
				Object.Instantiate<GameObject>(this.m_DestroyedObject, base.transform.position + base.transform.TransformVector(this.m_DestroyedObjectOffset), Quaternion.identity);
			}
			Object.Destroy(base.gameObject);
			if (this.Destroyed != null)
			{
				this.Destroyed.Send();
			}
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x001A8204 File Offset: 0x001A6404
		private List<float> GetLootProbabilities()
		{
			List<float> list = new List<float>();
			for (int i = 0; i < this.m_Loot.Length; i++)
			{
				list.Add(this.m_Loot[i].SpawnChance);
			}
			return list;
		}

		// Token: 0x0400347B RID: 13435
		public Message Destroyed = new Message();

		// Token: 0x0400347C RID: 13436
		[SerializeField]
		private FPTool.ToolPurpose m_RequiredToolPurpose;

		// Token: 0x0400347D RID: 13437
		[Range(0.01f, 1f)]
		[SerializeField]
		private float m_Resistance = 0.5f;

		// Token: 0x0400347E RID: 13438
		[Header("On Destroy")]
		[SerializeField]
		private GameObject m_DestroyedObject;

		// Token: 0x0400347F RID: 13439
		[SerializeField]
		private Vector3 m_DestroyedObjectOffset;

		// Token: 0x04003480 RID: 13440
		[Header("Loot")]
		[SerializeField]
		private LootItem[] m_Loot;

		// Token: 0x04003481 RID: 13441
		[SerializeField]
		private bool m_ShowGatherMessage = true;

		// Token: 0x04003482 RID: 13442
		[SerializeField]
		private Color m_MessageColor = Color.grey;

		// Token: 0x04003483 RID: 13443
		[SerializeField]
		private Color m_LootNameColor = Color.yellow;

		// Token: 0x04003484 RID: 13444
		private float m_CurrentHealth = 100f;
	}
}
