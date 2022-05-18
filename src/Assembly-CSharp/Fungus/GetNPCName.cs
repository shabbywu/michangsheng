using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001404 RID: 5124
	[CommandInfo("YSNew/Get", "GetNPCName", "获取NPC姓和名", 0)]
	[AddComponentMenu("")]
	public class GetNPCName : Command
	{
		// Token: 0x06007C65 RID: 31845 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x002C4E74 File Offset: 0x002C3074
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(this.NPCID.Value);
			string value = (wuJiangBangDing == null) ? Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(this.NPCID.Value)]["Name"].str) : Tools.Code64(wuJiangBangDing["Name"].str);
			this.Name.Value = value;
			this.Continue();
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A7A RID: 27258
		[Tooltip("NPC编号")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NPCID;

		// Token: 0x04006A7B RID: 27259
		[Tooltip("姓名存放的位置")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable Name;
	}
}
