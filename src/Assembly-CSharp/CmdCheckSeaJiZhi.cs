using System;
using Fungus;
using UnityEngine;

// Token: 0x02000634 RID: 1588
[CommandInfo("YSSea", "检查海域机制", "检查海域机制", 0)]
[AddComponentMenu("")]
public class CmdCheckSeaJiZhi : Command
{
	// Token: 0x06002778 RID: 10104 RVA: 0x00134844 File Offset: 0x00132A44
	public override void OnEnter()
	{
		MapSeaCompent component = this.GetFlowchart().transform.parent.GetComponent<MapSeaCompent>();
		if (component != null)
		{
			if (component.WhetherHasJiZhi)
			{
				if (component.jiZhiType == 0)
				{
					GlobalValue.Set(948, component.jiZhiChuFaID, base.GetCommandSourceDesc() ?? "");
					Object.Instantiate(Resources.Load(string.Format("talkPrefab/TalkPrefab/Talk{0}", component.jiZhiTalkID)));
				}
				else if (component.jiZhiType == 1)
				{
					Debug.Log(string.Format("海上点{0}的海域机制加载特殊随机副本类型:{1}", component.NodeIndex, component.jiZhiChuFaID));
					PlayerEx.Player.randomFuBenMag.GetInRandomFuBen(component.NodeIndex, component.jiZhiChuFaID);
				}
			}
		}
		else
		{
			Debug.LogError("海域机制检查时没有找到MapSeaCompent组件，请检查，当前物体" + base.gameObject.name);
		}
		this.Continue();
	}
}
