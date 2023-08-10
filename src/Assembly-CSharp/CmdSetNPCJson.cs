using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "设置NPCJson数据", "设置NPCJson数据", 0)]
[AddComponentMenu("")]
public class CmdSetNPCJson : Command
{
	[SerializeField]
	protected string valueName;

	[SerializeField]
	protected SetValueType valueType;

	[SerializeField]
	protected int intValue;

	[SerializeField]
	protected string stringValue;

	[SerializeField]
	protected bool boolValue;

	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		switch (valueType)
		{
		case SetValueType.Int:
			if (jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].HasField(valueName))
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].SetField(valueName, intValue);
			}
			else
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].AddField(valueName, intValue);
			}
			break;
		case SetValueType.String:
			if (jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].HasField(valueName))
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].SetField(valueName, stringValue);
			}
			else
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].AddField(valueName, stringValue);
			}
			break;
		case SetValueType.Bool:
			if (jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].HasField(valueName))
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].SetField(valueName, boolValue);
			}
			else
			{
				jsonData.instance.AvatarJsonData[nowJiaoHuNPC.ID.ToString()].AddField(valueName, boolValue);
			}
			break;
		}
		Continue();
	}
}
