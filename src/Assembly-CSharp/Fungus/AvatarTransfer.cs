using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001430 RID: 5168
	[CommandInfo("YSTools", "AvatarTransfer", "角色传送", 0)]
	[AddComponentMenu("")]
	public class AvatarTransfer : Command
	{
		// Token: 0x06007D0A RID: 32010 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x000549C8 File Offset: 0x00052BC8
		public override void OnEnter()
		{
			AvatarTransfer.Do(this.MapID);
			this.Continue();
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x002C60F4 File Offset: 0x002C42F4
		public static void Do(int mapID)
		{
			Avatar player = Tools.instance.getPlayer();
			if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(mapID))
			{
				if (AllMapManage.instance.mapIndex[mapID] is MapComponent)
				{
					(AllMapManage.instance.mapIndex[mapID] as MapComponent).AvatarMoveToThis();
				}
				else
				{
					AllMapManage.instance.mapIndex[mapID].AvatarMoveToThis();
				}
			}
			player.NowMapIndex = mapID;
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006ABF RID: 27327
		[Tooltip("传送到的大地图ID")]
		[SerializeField]
		protected int MapID;
	}
}
