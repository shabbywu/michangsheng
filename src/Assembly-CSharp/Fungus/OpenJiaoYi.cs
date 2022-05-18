using System;
using JiaoYi;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus
{
	// Token: 0x02001446 RID: 5190
	[CommandInfo("YSTools", "OpenJiaoYi", "打开交易界面", 0)]
	[AddComponentMenu("")]
	public class OpenJiaoYi : Command, INoCommand
	{
		// Token: 0x06007D66 RID: 32102 RVA: 0x002C66CC File Offset: 0x002C48CC
		public override void OnEnter()
		{
			ResManager.inst.LoadPrefab("JiaoYiUI").Inst(NewUICanvas.Inst.transform);
			JiaoYiUIMag.Inst.Init(this.AvatarID.Value, new UnityAction(this.Continue));
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AE0 RID: 27360
		[Tooltip("进行交易的武将ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;
	}
}
