using UnityEngine;
using script.ExchangeMeeting.UI.Interface;

namespace Fungus;

[CommandInfo("YSTools", "打开交易会", "打开交易会", 0)]
[AddComponentMenu("")]
public class OpenExchangeUI : Command, INoCommand
{
	public override void OnEnter()
	{
		IExchangeUIMag.Open();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
