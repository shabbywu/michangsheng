using System;
using UnityEngine.UI;

namespace script.MenPaiTask
{
	// Token: 0x02000A0A RID: 2570
	public class CompleteElderTask : BaseElderTask
	{
		// Token: 0x06004737 RID: 18231 RVA: 0x001E2944 File Offset: 0x001E0B44
		protected override void Init()
		{
			base.Init();
			base.Get<FpBtn>("领取按钮/按钮").mouseUpEvent.AddListener(delegate()
			{
				Tools.instance.getPlayer().ElderTaskMag.PlayerGetTaskItem(this.ElderTask);
				this.DestroySelf();
			});
			if (NPCEx.IsDeath(this.ElderTask.NpcId))
			{
				base.Get("头像", true).SetActive(false);
				return;
			}
			base.Get<PlayerSetRandomFace>("头像/Head/Viewport/Content/FaceBase/SkeletonGraphic").SetNPCFace(this.ElderTask.NpcId);
			base.Get<Text>("头像/Title/TitleText").SetText(jsonData.instance.AvatarJsonData[this.ElderTask.NpcId.ToString()]["Title"].Str);
			base.Get<Text>("头像/Name").SetText(jsonData.instance.AvatarRandomJsonData[this.ElderTask.NpcId.ToString()]["Name"].Str);
		}
	}
}
