using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetTaskDisabled", "将传闻置灰", 0)]
[AddComponentMenu("")]
public class SetTaskDisabled : Command
{
	[Tooltip("需要置灰的任务的ID")]
	[SerializeField]
	protected int TaskID;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		if (!player.taskMag._TaskData["Task"].HasField(TaskID.ToString()))
		{
			Continue();
			return;
		}
		player.taskMag._TaskData["Task"][TaskID.ToString()].SetField("disableTask", val: true);
		if (player.taskMag.isNowTask(TaskID))
		{
			player.taskMag.setNowTask(0);
		}
		string name = TaskJsonData.DataDict[TaskID].Name;
		UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 已达成", PopTipIconType.任务完成);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
