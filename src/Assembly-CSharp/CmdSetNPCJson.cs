using System;
using Fungus;
using UnityEngine;

// Token: 0x02000368 RID: 872
[CommandInfo("YSNPCJiaoHu", "设置NPCJson数据", "设置NPCJson数据", 0)]
[AddComponentMenu("")]
public class CmdSetNPCJson : Command
{
	// Token: 0x060018FF RID: 6399 RVA: 0x000DEF64 File Offset: 0x000DD164
	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		switch (this.valueType)
		{
		case SetValueType.Int:
			if (jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].HasField(this.valueName))
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].SetField(this.valueName, this.intValue);
			}
			else
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].AddField(this.valueName, this.intValue);
			}
			break;
		case SetValueType.Bool:
			if (jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].HasField(this.valueName))
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].SetField(this.valueName, this.boolValue);
			}
			else
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].AddField(this.valueName, this.boolValue);
			}
			break;
		case SetValueType.String:
			if (jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].HasField(this.valueName))
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].SetField(this.valueName, this.stringValue);
			}
			else
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].AddField(this.valueName, this.stringValue);
			}
			break;
		}
		this.Continue();
	}

	// Token: 0x040013E4 RID: 5092
	[SerializeField]
	protected string valueName;

	// Token: 0x040013E5 RID: 5093
	[SerializeField]
	protected SetValueType valueType;

	// Token: 0x040013E6 RID: 5094
	[SerializeField]
	protected int intValue;

	// Token: 0x040013E7 RID: 5095
	[SerializeField]
	protected string stringValue;

	// Token: 0x040013E8 RID: 5096
	[SerializeField]
	protected bool boolValue;
}
