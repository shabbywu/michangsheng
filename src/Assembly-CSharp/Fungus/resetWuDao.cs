using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "resetWuDao", "重置悟道", 0)]
[AddComponentMenu("")]
public class resetWuDao : Command
{
	[Tooltip("描述")]
	[SerializeField]
	protected string desc = "重置所有悟道点数";

	public override void OnEnter()
	{
		foreach (JSONObject item in Tools.instance.getPlayer().WuDaoJson.list)
		{
			item["study"] = new JSONObject(JSONObject.Type.ARRAY);
		}
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
