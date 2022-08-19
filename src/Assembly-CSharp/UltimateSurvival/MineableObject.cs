using System;
using System.Collections.Generic;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000604 RID: 1540
	public class MineableObject : MonoBehaviour
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x0015ECA4 File Offset: 0x0015CEA4
		public float Health
		{
			get
			{
				return this.m_CurrentHealth;
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x0015ECAC File Offset: 0x0015CEAC
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

		// Token: 0x06003162 RID: 12642 RVA: 0x0015ECF4 File Offset: 0x0015CEF4
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

		// Token: 0x06003163 RID: 12643 RVA: 0x0015ED65 File Offset: 0x0015CF65
		private void ReceiveDamage(float damage)
		{
			this.m_CurrentHealth += -damage * (1f - this.m_Resistance);
			if (this.m_CurrentHealth < Mathf.Epsilon)
			{
				this.DestroyObject();
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x0015ED98 File Offset: 0x0015CF98
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

		// Token: 0x06003165 RID: 12645 RVA: 0x0015EE04 File Offset: 0x0015D004
		private List<float> GetLootProbabilities()
		{
			List<float> list = new List<float>();
			for (int i = 0; i < this.m_Loot.Length; i++)
			{
				list.Add(this.m_Loot[i].SpawnChance);
			}
			return list;
		}

		// Token: 0x04002B7D RID: 11133
		public Message Destroyed = new Message();

		// Token: 0x04002B7E RID: 11134
		[SerializeField]
		private FPTool.ToolPurpose m_RequiredToolPurpose;

		// Token: 0x04002B7F RID: 11135
		[Range(0.01f, 1f)]
		[SerializeField]
		private float m_Resistance = 0.5f;

		// Token: 0x04002B80 RID: 11136
		[Header("On Destroy")]
		[SerializeField]
		private GameObject m_DestroyedObject;

		// Token: 0x04002B81 RID: 11137
		[SerializeField]
		private Vector3 m_DestroyedObjectOffset;

		// Token: 0x04002B82 RID: 11138
		[Header("Loot")]
		[SerializeField]
		private LootItem[] m_Loot;

		// Token: 0x04002B83 RID: 11139
		[SerializeField]
		private bool m_ShowGatherMessage = true;

		// Token: 0x04002B84 RID: 11140
		[SerializeField]
		private Color m_MessageColor = Color.grey;

		// Token: 0x04002B85 RID: 11141
		[SerializeField]
		private Color m_LootNameColor = Color.yellow;

		// Token: 0x04002B86 RID: 11142
		private float m_CurrentHealth = 100f;
	}
}
