using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013ED RID: 5101
	[CommandInfo("YSNew/Add", "AddWuDaoEx", "增加悟道经验值", 0)]
	[AddComponentMenu("")]
	public class AddWuDaoEx : Command
	{
		// Token: 0x06007C0A RID: 31754 RVA: 0x000545DA File Offset: 0x000527DA
		public override void OnEnter()
		{
			Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(this.Type.Value, this.AddNum.Value);
			this.Continue();
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A54 RID: 27220
		[Tooltip("增加悟道经验值的属性")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Type;

		// Token: 0x04006A55 RID: 27221
		[Tooltip("增加悟道经验值数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AddNum;
	}
}
