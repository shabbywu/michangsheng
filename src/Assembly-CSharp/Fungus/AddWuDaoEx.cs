using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F37 RID: 3895
	[CommandInfo("YSNew/Add", "AddWuDaoEx", "增加悟道经验值", 0)]
	[AddComponentMenu("")]
	public class AddWuDaoEx : Command
	{
		// Token: 0x06006E1F RID: 28191 RVA: 0x002A458C File Offset: 0x002A278C
		public override void OnEnter()
		{
			Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(this.Type.Value, this.AddNum.Value);
			this.Continue();
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E21 RID: 28193 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B82 RID: 23426
		[Tooltip("增加悟道经验值的属性")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Type;

		// Token: 0x04005B83 RID: 23427
		[Tooltip("增加悟道经验值数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AddNum;
	}
}
