using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Lua If", "If the test expression is true, execute the following command block.", 0)]
[AddComponentMenu("")]
public class LuaIf : LuaCondition
{
	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
