using PaiMai;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenPaiMai", "打开拍卖界面", 0)]
[AddComponentMenu("")]
public class OpenPaiMai : Command
{
	[Tooltip("拍卖行主持人")]
	[SerializeField]
	protected int PaiMaiAvatarID;

	[Tooltip("拍卖行ID")]
	[SerializeField]
	protected int PaiMaiID;

	public override void OnEnter()
	{
		ResManager.inst.LoadPrefab("PaiMai/NewPaiMaiUI").Inst().GetComponent<NewPaiMaiJoin>()
			.Init(PaiMaiID, PaiMaiAvatarID);
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
