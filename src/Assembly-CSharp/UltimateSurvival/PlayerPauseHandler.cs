using System;
using UltimateSurvival.StandardAssets;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008F7 RID: 2295
	public class PlayerPauseHandler : MonoBehaviour
	{
		// Token: 0x06003AD5 RID: 15061 RVA: 0x0002AB4D File Offset: 0x00028D4D
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnInventoryToggled));
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x001AA4BC File Offset: 0x001A86BC
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

		// Token: 0x0400351F RID: 13599
		[SerializeField]
		private DOF m_DOF;

		// Token: 0x04003520 RID: 13600
		[SerializeField]
		private ColorCorrection m_ColorCorrectionCurves;
	}
}
