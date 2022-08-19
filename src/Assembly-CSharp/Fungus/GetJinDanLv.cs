using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F44 RID: 3908
	[CommandInfo("YSNew/Get", "GetJinDanLv", "获取金丹等级保存到Value中", 0)]
	[AddComponentMenu("")]
	public class GetJinDanLv : Command
	{
		// Token: 0x06006E51 RID: 28241 RVA: 0x002A4A40 File Offset: 0x002A2C40
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

		// Token: 0x06006E52 RID: 28242 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B96 RID: 23446
		[Tooltip("保存到Value")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable JinDanValue;
	}
}
