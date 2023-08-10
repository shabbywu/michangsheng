using GUIPackage;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "OpenKeFang", "打开客房界面", 0)]
[AddComponentMenu("")]
public class OpenKeFang : Command
{
	[Tooltip("闭关界面的闭关速度")]
	[SerializeField]
	protected int BiGuanType = 1;

	public override void OnEnter()
	{
		NewUI();
		Continue();
	}

	public void NewUI()
	{
		UIBiGuanPanel.Inst.OpenBiGuan(BiGuanType);
	}

	public void OldUI()
	{
		XiuLian biguan = Singleton.biguan;
		if ((Object)(object)biguan != (Object)null)
		{
			((Component)biguan).GetComponentInChildren<XiuLian>().open();
			((Component)((Component)biguan).transform.Find("Win/Layer/Content3/BiGuan2")).GetComponentInChildren<UIBiGuan>().biguanType = BiGuanType;
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
