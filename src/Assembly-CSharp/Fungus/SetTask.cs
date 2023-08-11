using JSONClass;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetTaskIndex", "设置后续对话", 0)]
[AddComponentMenu("")]
public class SetTask : Command
{
	[Tooltip("任务的ID")]
	[SerializeField]
	protected int TaskID;

	[Tooltip("任务的变量值")]
	[SerializeField]
	protected int TaskIndex;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().taskMag.setTaskIndex(TaskID, TaskIndex);
		string name = TaskJsonData.DataDict[TaskID].Name;
		UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 进度已更新", PopTipIconType.任务进度);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
