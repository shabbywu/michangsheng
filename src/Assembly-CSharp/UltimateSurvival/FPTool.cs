using UnityEngine;

namespace UltimateSurvival;

public class FPTool : FPMelee
{
	public enum ToolPurpose
	{
		CutWood,
		BreakRocks
	}

	[Header("Tool Settings")]
	[SerializeField]
	[Tooltip("Useful for making the tools gather specific resources (eg. an axe should gather only wood, pickaxe - only stone)")]
	private ToolPurpose[] m_ToolPurposes;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_Efficiency = 0.5f;

	protected override void On_Hit()
	{
		base.On_Hit();
		RaycastData raycastData = base.Player.RaycastData.Get();
		if ((bool)raycastData)
		{
			MineableObject component = raycastData.GameObject.GetComponent<MineableObject>();
			if (Object.op_Implicit((Object)(object)component))
			{
				component.OnToolHit(m_ToolPurposes, base.DamagePerHit, m_Efficiency);
			}
		}
	}
}
