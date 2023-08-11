using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "通过称号获取NPC", "通过称号获取NPC，赋值到TmpValue，如果找不到会赋值0", 0)]
[AddComponentMenu("")]
public class CmdGetNPCIDByTitle : Command
{
	[SerializeField]
	protected string Title;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		int value = 0;
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			int i = item["id"].I;
			if (i >= 20000 && item["Title"].Str == Title)
			{
				value = i;
				break;
			}
		}
		flowchart.SetIntegerVariable("TmpValue", value);
		Continue();
	}
}
