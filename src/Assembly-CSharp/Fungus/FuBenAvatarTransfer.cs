using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "FuBenAvatarTransfer", "角色传送", 0)]
[AddComponentMenu("")]
public class FuBenAvatarTransfer : Command
{
	[Tooltip("场景名称")]
	[SerializeField]
	protected string ScenceName;

	[Tooltip("传送到的地点的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MapID;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Tools.instance.getPlayer().fubenContorl[ScenceName].NowIndex = MapID.Value;
		if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(MapID.Value))
		{
			AllMapManage.instance.mapIndex[MapID.Value].AvatarMoveToThis();
			WASDMove.Inst.IsMoved = true;
			wait();
			((MonoBehaviour)this).Invoke("removeWait", 0.8f);
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
