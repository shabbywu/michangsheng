using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F68 RID: 3944
	[CommandInfo("YSNew/Set", "SetTaskDisabled", "将传闻置灰", 0)]
	[AddComponentMenu("")]
	public class SetTaskDisabled : Command
	{
		// Token: 0x06006EDA RID: 28378 RVA: 0x002A58F8 File Offset: 0x002A3AF8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (!player.taskMag._TaskData["Task"].HasField(this.TaskID.ToString()))
			{
				this.Continue();
				return;
			}
			player.taskMag._TaskData["Task"][this.TaskID.ToString()].SetField("disableTask", true);
			if (player.taskMag.isNowTask(this.TaskID))
			{
				player.taskMag.setNowTask(0);
			}
			string name = TaskJsonData.DataDict[this.TaskID].Name;
			UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 已达成", PopTipIconType.任务完成);
			this.Continue();
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BD2 RID: 23506
		[Tooltip("需要置灰的任务的ID")]
		[SerializeField]
		protected int TaskID;
	}
}
