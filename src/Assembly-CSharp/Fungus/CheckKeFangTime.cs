using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013CE RID: 5070
	[CommandInfo("YS", "CheckKeFangTime", "检测客房是否有剩余时间", 0)]
	[AddComponentMenu("")]
	public class CheckKeFangTime : Command
	{
		// Token: 0x06007B98 RID: 31640 RVA: 0x002C3F4C File Offset: 0x002C214C
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempBool.Value = player.zulinContorl.HasTime(this.ScenceName);
			this.Continue();
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A13 RID: 27155
		[Tooltip("需要检测时间的客房的场景名称")]
		[SerializeField]
		protected string ScenceName = "";

		// Token: 0x04006A14 RID: 27156
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
