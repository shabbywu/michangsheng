using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

[CommandInfo("YSTools", "OpenLunDao", "加载论道场景", 0)]
[AddComponentMenu("")]
public class OpenLunDao : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("是否随机论题")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable isSuiJiLunTi;

	[Tooltip("随机论题数目")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable num;

	public override void OnEnter()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		Tools instance = Tools.instance;
		Scene activeScene = SceneManager.GetActiveScene();
		instance.FinalScene = ((Scene)(ref activeScene)).name;
		Tools.instance.LunDaoNpcId = npcId.Value;
		if (isSuiJiLunTi.Value)
		{
			Tools.instance.IsSuiJiLunTi = isSuiJiLunTi.Value;
			Tools.instance.LunTiNum = num.Value;
		}
		Tools.instance.loadOtherScenes("LunDao");
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
