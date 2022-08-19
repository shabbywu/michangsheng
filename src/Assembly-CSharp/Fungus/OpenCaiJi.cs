using System;
using CaiJi;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F8F RID: 3983
	[CommandInfo("YSTools", "OpenCaiJi", "打开采集界面", 0)]
	[AddComponentMenu("")]
	public class OpenCaiJi : Command
	{
		// Token: 0x06006F72 RID: 28530 RVA: 0x002A6F80 File Offset: 0x002A5180
		public override void OnEnter()
		{
			ResManager.inst.LoadPrefab("CaiJiPanel").Inst(null);
			CaiJiUIMag.inst.OpenCaiJi(this.ID);
			this.Continue();
		}

		// Token: 0x06006F73 RID: 28531 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C0E RID: 23566
		[Tooltip("采集界面对应流水号")]
		[SerializeField]
		protected int ID = 1;
	}
}
