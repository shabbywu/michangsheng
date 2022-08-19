using System;
using UnityEngine;

namespace Mod
{
	// Token: 0x0200073C RID: 1852
	public class MadeMod : MonoBehaviour, IESCClose
	{
		// Token: 0x06003B00 RID: 15104 RVA: 0x00195D98 File Offset: 0x00193F98
		private void Awake()
		{
			MadeMod.Inst = this;
			this.Transform = base.transform;
			this.Transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			this.Transform.localPosition = Vector3.zero;
			this.Transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			PanelMamager.CanOpenOrClose = false;
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x00195DF8 File Offset: 0x00193FF8
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			MadeMod.Inst = null;
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00195E0C File Offset: 0x0019400C
		public void Show()
		{
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00195E25 File Offset: 0x00194025
		public void Close()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			base.gameObject.SetActive(false);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00195E4A File Offset: 0x0019404A
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003335 RID: 13109
		public static MadeMod Inst;

		// Token: 0x04003336 RID: 13110
		public Transform Transform;
	}
}
