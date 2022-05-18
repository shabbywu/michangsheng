using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C9 RID: 2249
	public class FPTool : FPMelee
	{
		// Token: 0x060039E2 RID: 14818 RVA: 0x001A70CC File Offset: 0x001A52CC
		protected override void On_Hit()
		{
			base.On_Hit();
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (!raycastData)
			{
				return;
			}
			MineableObject component = raycastData.GameObject.GetComponent<MineableObject>();
			if (component)
			{
				component.OnToolHit(this.m_ToolPurposes, base.DamagePerHit, this.m_Efficiency);
			}
		}

		// Token: 0x04003410 RID: 13328
		[Header("Tool Settings")]
		[SerializeField]
		[Tooltip("Useful for making the tools gather specific resources (eg. an axe should gather only wood, pickaxe - only stone)")]
		private FPTool.ToolPurpose[] m_ToolPurposes;

		// Token: 0x04003411 RID: 13329
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Efficiency = 0.5f;

		// Token: 0x020008CA RID: 2250
		public enum ToolPurpose
		{
			// Token: 0x04003413 RID: 13331
			CutWood,
			// Token: 0x04003414 RID: 13332
			BreakRocks
		}
	}
}
