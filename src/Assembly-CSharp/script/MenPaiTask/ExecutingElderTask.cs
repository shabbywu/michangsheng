using System;
using UnityEngine.UI;

namespace script.MenPaiTask
{
	// Token: 0x02000A0B RID: 2571
	public class ExecutingElderTask : BaseElderTask
	{
		// Token: 0x0600473A RID: 18234 RVA: 0x001E2A60 File Offset: 0x001E0C60
		protected override void Init()
		{
			if (NPCEx.IsDeath(this.ElderTask.NpcId))
			{
				this._go.SetActive(false);
				return;
			}
			base.Init();
			base.Get<PlayerSetRandomFace>("头像/Head/Viewport/Content/FaceBase/SkeletonGraphic").SetNPCFace(this.ElderTask.NpcId);
			base.Get<Text>("头像/Title/TitleText").SetText(jsonData.instance.AvatarJsonData[this.ElderTask.NpcId.ToString()]["Title"].Str);
			base.Get<Text>("头像/Name").SetText(jsonData.instance.AvatarRandomJsonData[this.ElderTask.NpcId.ToString()]["Name"].Str);
		}
	}
}
