using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F35 RID: 3893
	[CommandInfo("YSNew/Add", "AddTask", "增加任務", 0)]
	[AddComponentMenu("")]
	public class AddTask : Command
	{
		// Token: 0x06006E18 RID: 28184 RVA: 0x002A4474 File Offset: 0x002A2674
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

		// Token: 0x06006E19 RID: 28185 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B7C RID: 23420
		[Tooltip("增加的任务ID")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04005B7D RID: 23421
		[Tooltip("是否条提示获得传闻弹框")]
		[SerializeField]
		protected bool showInfo = true;
	}
}
