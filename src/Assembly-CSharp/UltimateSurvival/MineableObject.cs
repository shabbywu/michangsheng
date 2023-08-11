using System.Collections.Generic;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival;

public class MineableObject : MonoBehaviour
{
	public Message Destroyed = new Message();

	[SerializeField]
	private FPTool.ToolPurpose m_RequiredToolPurpose;

	[Range(0.01f, 1f)]
	[SerializeField]
	private float m_Resistance = 0.5f;

	[Header("On Destroy")]
	[SerializeField]
	private GameObject m_DestroyedObject;

	[SerializeField]
	private Vector3 m_DestroyedObjectOffset;

	[Header("Loot")]
	[SerializeField]
	private LootItem[] m_Loot;

	[SerializeField]
	private bool m_ShowGatherMessage = true;

	[SerializeField]
	private Color m_MessageColor = Color.grey;

	[SerializeField]
	private Color m_LootNameColor = Color.yellow;

	private float m_CurrentHealth = 100f;

	public float Health => m_CurrentHealth;

	public void OnToolHit(FPTool.ToolPurpose[] toolPurposes, float damage, float efficiency)
	{
		bool flag = false;
		for (int i = 0; i < toolPurposes.Length; i++)
		{
			if (toolPurposes[i] == m_RequiredToolPurpose)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			ReceiveDamage(damage);
			if (m_Loot.Length != 0)
			{
				GiveLootToPlayer(efficiency);
			}
		}
	}

	private void GiveLootToPlayer(float amountFactor)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		int num = ProbabilityUtils.RandomChoiceFollowingDistribution(GetLootProbabilities());
		LootItem lootItem = m_Loot[num];
		lootItem.AddToInventory(out var added, amountFactor);
		string arg = ColorUtils.ColorToHex(Color32.op_Implicit(m_LootNameColor));
		if (m_ShowGatherMessage && added > 0)
		{
			MonoSingleton<MessageDisplayer>.Instance.PushMessage($"Gathered <color={arg}>{lootItem.ItemName}</color> x {added}", m_MessageColor);
		}
	}

	private void ReceiveDamage(float damage)
	{
		m_CurrentHealth += (0f - damage) * (1f - m_Resistance);
		if (m_CurrentHealth < Mathf.Epsilon)
		{
			DestroyObject();
		}
	}

	private void DestroyObject()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)m_DestroyedObject))
		{
			Object.Instantiate<GameObject>(m_DestroyedObject, ((Component)this).transform.position + ((Component)this).transform.TransformVector(m_DestroyedObjectOffset), Quaternion.identity);
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
		if (Destroyed != null)
		{
			Destroyed.Send();
		}
	}

	private List<float> GetLootProbabilities()
	{
		List<float> list = new List<float>();
		for (int i = 0; i < m_Loot.Length; i++)
		{
			list.Add(m_Loot[i].SpawnChance);
		}
		return list;
	}
}
