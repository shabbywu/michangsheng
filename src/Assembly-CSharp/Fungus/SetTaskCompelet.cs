using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetTaskCompelet", "指定任务完成", 0)]
[AddComponentMenu("")]
public class SetTaskCompelet : Command
{
	[Tooltip("任务的ID")]
	[SerializeField]
	protected int TaskID;

	public override void OnEnter()
	{
		Do(TaskID);
		Continue();
	}

	public static void Do(int TaskID)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.taskMag._TaskData["Task"].HasField(TaskID.ToString()))
		{
			player.taskMag._TaskData["Task"][string.Concat(TaskID)].SetField("isComplete", val: true);
			player.taskMag._TaskData["Task"][string.Concat(TaskID)].SetField("disableTask", val: true);
			player.StreamData.TaskMag.CheckHasOut();
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
