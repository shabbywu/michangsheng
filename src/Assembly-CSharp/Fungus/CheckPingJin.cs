using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D0 RID: 5072
	[CommandInfo("YS", "CheckPingJin", "检测是否是瓶颈期数量", 0)]
	[AddComponentMenu("")]
	public class CheckPingJin : Command
	{
		// Token: 0x06007BA0 RID: 31648 RVA: 0x002C3FB8 File Offset: 0x002C21B8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (player.level % 3 == 0 && player.exp >= (ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n);
			this.Continue();
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A16 RID: 27158
		[Tooltip("获取到的瓶颈值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
