using System;
using JiaoYi;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus
{
	// Token: 0x02000F93 RID: 3987
	[CommandInfo("YSTools", "OpenJiaoYi", "打开交易界面", 0)]
	[AddComponentMenu("")]
	public class OpenJiaoYi : Command, INoCommand
	{
		// Token: 0x06006F7F RID: 28543 RVA: 0x002A7024 File Offset: 0x002A5224
		public override void OnEnter()
		{
			ResManager.inst.LoadPrefab("JiaoYiUI").Inst(NewUICanvas.Inst.transform);
			JiaoYiUIMag.Inst.Init(this.AvatarID.Value, new UnityAction(this.Continue));
		}

		// Token: 0x06006F80 RID: 28544 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C11 RID: 23569
		[Tooltip("进行交易的武将ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;
	}
}
