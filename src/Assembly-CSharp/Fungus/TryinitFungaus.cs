using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "TryinitFungaus", "初始化Fungaus", 0)]
[AddComponentMenu("")]
public class TryinitFungaus : Command, INoCommand
{
	[Tooltip("说明")]
	[SerializeField]
	protected string init = "初始化";

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		Flowchart flowchart = GetFlowchart();
		setHasVariable("ShenShi", player.shengShi, flowchart);
		setHasVariable("JinJie", player.level, flowchart);
		setHasVariable("DunSu", player.dunSu, flowchart);
		setHasVariable("ZiZhi", player.ZiZhi, flowchart);
		setHasVariable("WuXin", (int)player.wuXin, flowchart);
		setHasVariable("ShaQi", (int)player.shaQi, flowchart);
		setHasVariable("MenPai", player.menPai, flowchart);
		setHasVariable("ChengHao", player.chengHao, flowchart);
		setHasVariable("Sex", player.Sex, flowchart);
		Continue();
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
