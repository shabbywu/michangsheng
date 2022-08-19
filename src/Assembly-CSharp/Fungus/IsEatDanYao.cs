using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F85 RID: 3973
	[CommandInfo("YSTools", "是否吃过丹药", "是否吃过丹药", 0)]
	[AddComponentMenu("")]
	public class IsEatDanYao : Command
	{
		// Token: 0x06006F47 RID: 28487 RVA: 0x002A6AC8 File Offset: 0x002A4CC8
		public override void OnEnter()
		{
			int num;
			if (this.DanYao == 0)
			{
				num = this.DanYaoValue.Value;
			}
			else
			{
				num = this.DanYao;
			}
			if (num == 0)
			{
				Debug.LogError("物品Id不能为空");
				this.result.Value = false;
				this.Continue();
				return;
			}
			int jsonobject = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(num));
			this.result.Value = (jsonobject > 0);
			this.Continue();
		}

		// Token: 0x04005BFE RID: 23550
		[Tooltip("丹药Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable DanYaoValue;

		// Token: 0x04005BFF RID: 23551
		[Tooltip("丹药Id")]
		[SerializeField]
		protected int DanYao;

		// Token: 0x04005C00 RID: 23552
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable result;
	}
}
