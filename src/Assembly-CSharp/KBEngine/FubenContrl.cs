using System;

namespace KBEngine
{
	// Token: 0x02000C8F RID: 3215
	public class FubenContrl
	{
		// Token: 0x06005997 RID: 22935 RVA: 0x002565DF File Offset: 0x002547DF
		public FubenContrl(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x002565F3 File Offset: 0x002547F3
		public void outFuBen(bool ToLast = true)
		{
			if (ToLast)
			{
				Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastFuBenScence, true);
			}
			this.entity.lastFuBenScence = "";
			this.entity.NowFuBen = "";
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00256632 File Offset: 0x00254832
		public bool isInFuBen()
		{
			return this.entity.NowFuBen != "";
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x00004095 File Offset: 0x00002295
		public void CreatRandomFuBen()
		{
		}

		// Token: 0x17000676 RID: 1654
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

		// Token: 0x0400522B RID: 21035
		public Avatar entity;
	}
}
