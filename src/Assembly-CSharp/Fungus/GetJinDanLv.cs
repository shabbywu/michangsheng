using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013FA RID: 5114
	[CommandInfo("YSNew/Get", "GetJinDanLv", "获取金丹等级保存到Value中", 0)]
	[AddComponentMenu("")]
	public class GetJinDanLv : Command
	{
		// Token: 0x06007C3C RID: 31804 RVA: 0x002C49D8 File Offset: 0x002C2BD8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int value = 1;
			try
			{
				value = jsonData.instance.JieDanBiao[player.hasJieDanSkillList[0].itemId.ToString()]["JinDanQuality"].I;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			this.JinDanValue.Value = value;
			this.Continue();
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A68 RID: 27240
		[Tooltip("保存到Value")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable JinDanValue;
	}
}
