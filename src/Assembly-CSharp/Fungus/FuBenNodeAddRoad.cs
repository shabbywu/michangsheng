using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "FuBenNodeAddRoad", "给当前副本中的一个节点新增可行走路线", 0)]
[AddComponentMenu("")]
public class FuBenNodeAddRoad : Command
{
	[Tooltip("要设置路线的节点")]
	[SerializeField]
	protected int NodeID;

	[Tooltip("可以走的路线的路径")]
	[SerializeField]
	protected List<int> NextNodeList;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int nextNode in NextNodeList)
		{
			player.fubenContorl[Tools.getScreenName()].AddNodeRoad(NodeID, nextNode);
		}
		Continue();
	}

	public void removeWait()
	{
		CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
		if ((Object)(object)component != (Object)null)
		{
			component.follwPlayer = false;
		}
	}

	public void wait()
	{
		CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
		if ((Object)(object)component != (Object)null)
		{
			component.follwPlayer = true;
		}
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
