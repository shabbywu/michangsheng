using System;
using PaiMai;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200144B RID: 5195
	[CommandInfo("YSTools", "OpenPaiMai", "打开拍卖界面", 0)]
	[AddComponentMenu("")]
	public class OpenPaiMai : Command
	{
		// Token: 0x06007D77 RID: 32119 RVA: 0x00054D73 File Offset: 0x00052F73
		public override void OnEnter()
		{
			ResManager.inst.LoadPrefab("PaiMai/NewPaiMaiUI").Inst(null).GetComponent<NewPaiMaiJoin>().Init(this.PaiMaiID, this.PaiMaiAvatarID);
			this.Continue();
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AE7 RID: 27367
		[Tooltip("拍卖行主持人")]
		[SerializeField]
		protected int PaiMaiAvatarID;

		// Token: 0x04006AE8 RID: 27368
		[Tooltip("拍卖行ID")]
		[SerializeField]
		protected int PaiMaiID;
	}
}
