using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetTaskFaild", "指定任务失败", 0)]
[AddComponentMenu("")]
public class SetTaskFaild : Command
{
	[Tooltip("任务的ID")]
	[SerializeField]
	protected int TaskID;

	public override void OnEnter()
	{
		if (!Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(TaskID.ToString()))
		{
			Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(TaskID)].SetField("disableTask", val: true);
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
