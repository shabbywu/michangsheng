using System;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "GetFuBenShuXin", "获取随机副本属性", 0)]
[AddComponentMenu("")]
public class GetFuBenShuXin : Command
{
	[Tooltip("获取副本属性")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	public IntegerVariable ShuXin;

	[Tooltip("获取副本类型")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	public IntegerVariable LeiXin;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = Tools.instance.getPlayer().NowRandomFuBenID;
		JToken val = player.RandomFuBenList[nowRandomFuBenID.ToString()];
		if ((Object)(object)ShuXin != (Object)null)
		{
			ShuXin.Value = (int)val[(object)"ShuXin"];
		}
		if ((Object)(object)LeiXin != (Object)null)
		{
			LeiXin.Value = (int)val[(object)"type"];
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
