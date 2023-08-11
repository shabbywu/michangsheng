using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "HideNPC", "隐藏NPC界面", 0)]
[AddComponentMenu("")]
public class HideNPC : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string init = "隐藏NPC界面";

	public override void OnEnter()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = GameObject.Find("Canvas/Scroll View/Viewport/Content");
		if ((Object)(object)val != (Object)null)
		{
			val.transform.localScale = Vector3.zero;
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
