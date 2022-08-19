using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F4E RID: 3918
	[CommandInfo("YSNew/Get", "GetNPCName", "获取NPC姓和名", 0)]
	[AddComponentMenu("")]
	public class GetNPCName : Command
	{
		// Token: 0x06006E7A RID: 28282 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x002A4F80 File Offset: 0x002A3180
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			JSONObject wuJiangBangDing = Tools.instance.getWuJiangBangDing(this.NPCID.Value);
			string value = (wuJiangBangDing == null) ? Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(this.NPCID.Value)]["Name"].str) : Tools.Code64(wuJiangBangDing["Name"].str);
			this.Name.Value = value;
			this.Continue();
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BA9 RID: 23465
		[Tooltip("NPC编号")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NPCID;

		// Token: 0x04005BAA RID: 23466
		[Tooltip("姓名存放的位置")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable Name;
	}
}
