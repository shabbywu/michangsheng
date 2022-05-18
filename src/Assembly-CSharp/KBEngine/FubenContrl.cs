using System;

namespace KBEngine
{
	// Token: 0x02001053 RID: 4179
	public class FubenContrl
	{
		// Token: 0x06006455 RID: 25685 RVA: 0x00045060 File Offset: 0x00043260
		public FubenContrl(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x00045074 File Offset: 0x00043274
		public void outFuBen(bool ToLast = true)
		{
			if (ToLast)
			{
				Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastFuBenScence, true);
			}
			this.entity.lastFuBenScence = "";
			this.entity.NowFuBen = "";
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x000450B3 File Offset: 0x000432B3
		public bool isInFuBen()
		{
			return this.entity.NowFuBen != "";
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x000042DD File Offset: 0x000024DD
		public void CreatRandomFuBen()
		{
		}

		// Token: 0x170008C9 RID: 2249
		public MapIndexInfo this[string name]
		{
			get
			{
				if (!this.entity.FuBen.HasField(name))
				{
					this.entity.FuBen.AddField(name, new JSONObject(JSONObject.Type.OBJECT));
				}
				return new MapIndexInfo(this)
				{
					SenceName = name
				};
			}
		}

		// Token: 0x04005DD8 RID: 24024
		public Avatar entity;
	}
}
