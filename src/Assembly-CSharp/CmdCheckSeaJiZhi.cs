using Fungus;
using UnityEngine;

[CommandInfo("YSSea", "检查海域机制", "检查海域机制", 0)]
[AddComponentMenu("")]
public class CmdCheckSeaJiZhi : Command
{
	public override void OnEnter()
	{
		MapSeaCompent component = ((Component)((Component)GetFlowchart()).transform.parent).GetComponent<MapSeaCompent>();
		if ((Object)(object)component != (Object)null)
		{
			if (component.WhetherHasJiZhi)
			{
				if (component.jiZhiType == 0)
				{
					GlobalValue.Set(948, component.jiZhiChuFaID, GetCommandSourceDesc() ?? "");
					Object.Instantiate(Resources.Load($"talkPrefab/TalkPrefab/Talk{component.jiZhiTalkID}"));
				}
				else if (component.jiZhiType == 1)
				{
					Debug.Log((object)$"海上点{component.NodeIndex}的海域机制加载特殊随机副本类型:{component.jiZhiChuFaID}");
					PlayerEx.Player.randomFuBenMag.GetInRandomFuBen(component.NodeIndex, component.jiZhiChuFaID);
				}
			}
		}
		else
		{
			Debug.LogError((object)("海域机制检查时没有找到MapSeaCompent组件，请检查，当前物体" + ((Object)((Component)this).gameObject).name));
		}
		Continue();
	}
}
