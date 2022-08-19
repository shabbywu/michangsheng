using System;
using Fungus;
using UnityEngine;

// Token: 0x0200024E RID: 590
[CommandInfo("YSNPCJiaoHu", "设置NPCJson数据", "设置NPCJson数据", 0)]
[AddComponentMenu("")]
public class CmdSetNPCJson : Command
{
	// Token: 0x06001649 RID: 5705 RVA: 0x00096B24 File Offset: 0x00094D24
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

	// Token: 0x04001092 RID: 4242
	[SerializeField]
	protected string valueName;

	// Token: 0x04001093 RID: 4243
	[SerializeField]
	protected SetValueType valueType;

	// Token: 0x04001094 RID: 4244
	[SerializeField]
	protected int intValue;

	// Token: 0x04001095 RID: 4245
	[SerializeField]
	protected string stringValue;

	// Token: 0x04001096 RID: 4246
	[SerializeField]
	protected bool boolValue;
}
