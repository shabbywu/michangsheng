using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddTask", "增加任務", 0)]
[AddComponentMenu("")]
public class AddTask : Command
{
	[Tooltip("增加的任务ID")]
	[SerializeField]
	protected int TaskID;

	[Tooltip("是否条提示获得传闻弹框")]
	[SerializeField]
	protected bool showInfo = true;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		player.taskMag.addTask(TaskID);
		string text = Tools.instance.Code64ToString(jsonData.instance.TaskJsonData[TaskID.ToString()]["Name"].str);
		string msg = ((jsonData.instance.TaskJsonData[TaskID.ToString()]["Type"].n == 0f) ? "获得一条新的传闻" : ("<color=#FF0000>" + text + "</color>任务已开启"));
		if (!player.taskMag.isHasTask(TaskID) && showInfo)
		{
			UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
