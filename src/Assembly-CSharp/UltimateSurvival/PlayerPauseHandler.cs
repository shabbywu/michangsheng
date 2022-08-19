using System;
using UltimateSurvival.StandardAssets;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000613 RID: 1555
	public class PlayerPauseHandler : MonoBehaviour
	{
		// Token: 0x060031B2 RID: 12722 RVA: 0x00160D74 File Offset: 0x0015EF74
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnInventoryToggled));
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00160D94 File Offset: 0x0015EF94
		private void OnInventoryToggled()
		{
			bool isClosed = MonoSingleton<InventoryController>.Instance.IsClosed;
			if (this.m_DOF)
			{
				this.m_DOF.enabled = !isClosed;
			}
			if (this.m_ColorCorrectionCurves)
			{
				this.m_ColorCorrectionCurves.enabled = !isClosed;
			}
		}

		// Token: 0x04002C08 RID: 11272
		[SerializeField]
		private DOF m_DOF;

		// Token: 0x04002C09 RID: 11273
		[SerializeField]
		private ColorCorrection m_ColorCorrectionCurves;
	}
}
