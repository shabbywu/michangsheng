using System;
using Fungus;
using UnityEngine;

// Token: 0x02000238 RID: 568
[CommandInfo("YSNPCJiaoHu", "通过称号获取NPC", "通过称号获取NPC，赋值到TmpValue，如果找不到会赋值0", 0)]
[AddComponentMenu("")]
public class CmdGetNPCIDByTitle : Command
{
	// Token: 0x06001616 RID: 5654 RVA: 0x000958C0 File Offset: 0x00093AC0
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int value = 0;
		foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
		{
			int i = jsonobject["id"].I;
			if (i >= 20000 && jsonobject["Title"].Str == this.Title)
			{
				value = i;
				break;
			}
		}
		flowchart.SetIntegerVariable("TmpValue", value);
		this.Continue();
	}

	// Token: 0x04001069 RID: 4201
	[SerializeField]
	protected string Title;
}
