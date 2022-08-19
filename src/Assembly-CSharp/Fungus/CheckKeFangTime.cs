using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F1A RID: 3866
	[CommandInfo("YS", "CheckKeFangTime", "检测客房是否有剩余时间", 0)]
	[AddComponentMenu("")]
	public class CheckKeFangTime : Command
	{
		// Token: 0x06006DAD RID: 28077 RVA: 0x002A3BE4 File Offset: 0x002A1DE4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempBool.Value = player.zulinContorl.HasTime(this.ScenceName);
			this.Continue();
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B49 RID: 23369
		[Tooltip("需要检测时间的客房的场景名称")]
		[SerializeField]
		protected string ScenceName = "";

		// Token: 0x04005B4A RID: 23370
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
