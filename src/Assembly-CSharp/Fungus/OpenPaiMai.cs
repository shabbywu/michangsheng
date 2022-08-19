using System;
using PaiMai;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F98 RID: 3992
	[CommandInfo("YSTools", "OpenPaiMai", "打开拍卖界面", 0)]
	[AddComponentMenu("")]
	public class OpenPaiMai : Command
	{
		// Token: 0x06006F90 RID: 28560 RVA: 0x002A7189 File Offset: 0x002A5389
		public override void OnEnter()
		{
			ResManager.inst.LoadPrefab("PaiMai/NewPaiMaiUI").Inst(null).GetComponent<NewPaiMaiJoin>().Init(this.PaiMaiID, this.PaiMaiAvatarID);
			this.Continue();
		}

		// Token: 0x06006F91 RID: 28561 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F92 RID: 28562 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C18 RID: 23576
		[Tooltip("拍卖行主持人")]
		[SerializeField]
		protected int PaiMaiAvatarID;

		// Token: 0x04005C19 RID: 23577
		[Tooltip("拍卖行ID")]
		[SerializeField]
		protected int PaiMaiID;
	}
}
