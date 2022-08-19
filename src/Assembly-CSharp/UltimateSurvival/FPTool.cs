using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F4 RID: 1524
	public class FPTool : FPMelee
	{
		// Token: 0x060030FE RID: 12542 RVA: 0x0015DB80 File Offset: 0x0015BD80
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

		// Token: 0x04002B35 RID: 11061
		[Header("Tool Settings")]
		[SerializeField]
		[Tooltip("Useful for making the tools gather specific resources (eg. an axe should gather only wood, pickaxe - only stone)")]
		private FPTool.ToolPurpose[] m_ToolPurposes;

		// Token: 0x04002B36 RID: 11062
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Efficiency = 0.5f;

		// Token: 0x020014BF RID: 5311
		public enum ToolPurpose
		{
			// Token: 0x04006D1E RID: 27934
			CutWood,
			// Token: 0x04006D1F RID: 27935
			BreakRocks
		}
	}
}
