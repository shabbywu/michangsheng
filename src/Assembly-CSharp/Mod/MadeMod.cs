using System;
using UnityEngine;

namespace Mod
{
	// Token: 0x02000AA9 RID: 2729
	public class MadeMod : MonoBehaviour, IESCClose
	{
		// Token: 0x060045E0 RID: 17888 RVA: 0x001DDAB4 File Offset: 0x001DBCB4
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

		// Token: 0x060045E1 RID: 17889 RVA: 0x00031F59 File Offset: 0x00030159
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			MadeMod.Inst = null;
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x00031F6D File Offset: 0x0003016D
		public void Show()
		{
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x00031F86 File Offset: 0x00030186
		public void Close()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			base.gameObject.SetActive(false);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x00031FAB File Offset: 0x000301AB
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003E1A RID: 15898
		public static MadeMod Inst;

		// Token: 0x04003E1B RID: 15899
		public Transform Transform;
	}
}
