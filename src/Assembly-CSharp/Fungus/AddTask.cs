using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013EB RID: 5099
	[CommandInfo("YSNew/Add", "AddTask", "增加任務", 0)]
	[AddComponentMenu("")]
	public class AddTask : Command
	{
		// Token: 0x06007C03 RID: 31747 RVA: 0x002C456C File Offset: 0x002C276C
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			player.taskMag.addTask(this.TaskID);
			string str = Tools.instance.Code64ToString(jsonData.instance.TaskJsonData[this.TaskID.ToString()]["Name"].str);
			string msg = (jsonData.instance.TaskJsonData[this.TaskID.ToString()]["Type"].n == 0f) ? "获得一条新的传闻" : ("<color=#FF0000>" + str + "</color>任务已开启");
			if (!player.taskMag.isHasTask(this.TaskID) && this.showInfo)
			{
				UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
			}
			this.Continue();
		}

		// Token: 0x06007C04 RID: 31748 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A4E RID: 27214
		[Tooltip("增加的任务ID")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04006A4F RID: 27215
		[Tooltip("是否条提示获得传闻弹框")]
		[SerializeField]
		protected bool showInfo = true;
	}
}
