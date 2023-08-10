using CaiJi;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenCaiJi", "打开采集界面", 0)]
[AddComponentMenu("")]
public class OpenCaiJi : Command
{
	[Tooltip("采集界面对应流水号")]
	[SerializeField]
	protected int ID = 1;

	public override void OnEnter()
	{
		ResManager.inst.LoadPrefab("CaiJiPanel").Inst();
		CaiJiUIMag.inst.OpenCaiJi(ID);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
