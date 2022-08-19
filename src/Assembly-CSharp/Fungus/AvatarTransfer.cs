using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F79 RID: 3961
	[CommandInfo("YSTools", "AvatarTransfer", "角色传送", 0)]
	[AddComponentMenu("")]
	public class AvatarTransfer : Command
	{
		// Token: 0x06006F1A RID: 28442 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x002A6478 File Offset: 0x002A4678
		public override void OnEnter()
		{
			AvatarTransfer.Do(this.MapID);
			this.Continue();
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x002A648C File Offset: 0x002A468C
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

		// Token: 0x06006F1D RID: 28445 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BEB RID: 23531
		[Tooltip("传送到的大地图ID")]
		[SerializeField]
		protected int MapID;
	}
}
