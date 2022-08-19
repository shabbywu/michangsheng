using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F45 RID: 3909
	[CommandInfo("YSNew/Get", "GetLunDaoTargetNum", "获取完成论道数目", 0)]
	[AddComponentMenu("")]
	public class GetLunDaoTargetNum : Command
	{
		// Token: 0x06006E55 RID: 28245 RVA: 0x002A4ABC File Offset: 0x002A2CBC
		public override void OnEnter()
		{
			this.num.Value = Tools.instance.TargetLunTiNum;
			this.Continue();
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E57 RID: 28247 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B97 RID: 23447
		[Tooltip("完成论题数目")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable num;
	}
}
