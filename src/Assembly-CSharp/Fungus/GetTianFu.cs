using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F54 RID: 3924
	[CommandInfo("YSNew/Get", "GetTianFu", "获取是否选择该天赋", 0)]
	[AddComponentMenu("")]
	public class GetTianFu : Command
	{
		// Token: 0x06006E95 RID: 28309 RVA: 0x002A523E File Offset: 0x002A343E
		public override void OnEnter()
		{
			this.TempBool.Value = PlayerEx.HasTianFu(this.TianFuID);
			this.Continue();
		}

		// Token: 0x06006E96 RID: 28310 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BB4 RID: 23476
		[Tooltip("天赋的ID")]
		[SerializeField]
		protected int TianFuID;

		// Token: 0x04005BB5 RID: 23477
		[Tooltip("返回是否拥有的值")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
