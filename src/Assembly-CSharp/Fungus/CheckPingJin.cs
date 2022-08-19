using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F1C RID: 3868
	[CommandInfo("YS", "CheckPingJin", "检测是否是瓶颈期数量", 0)]
	[AddComponentMenu("")]
	public class CheckPingJin : Command
	{
		// Token: 0x06006DB5 RID: 28085 RVA: 0x002A3C64 File Offset: 0x002A1E64
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (player.level % 3 == 0 && player.exp >= (ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n);
			this.Continue();
		}

		// Token: 0x06006DB6 RID: 28086 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DB7 RID: 28087 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B4C RID: 23372
		[Tooltip("获取到的瓶颈值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
