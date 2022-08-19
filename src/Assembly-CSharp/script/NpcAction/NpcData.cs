using System;
using UnityEngine;
using UnityEngine.Events;

namespace script.NpcAction
{
	// Token: 0x020009F2 RID: 2546
	public class NpcData
	{
		// Token: 0x0600469E RID: 18078 RVA: 0x001DDD6C File Offset: 0x001DBF6C
		public NpcData(int npcId)
		{
			this.IsInit = false;
			try
			{
				this.NpcId = npcId;
				this.NpcBaseJson = jsonData.instance.AvatarJsonData[npcId.ToString()].Copy();
				this.NpcFaceJso = jsonData.instance.AvatarRandomJsonData[npcId.ToString()].Copy();
				this.NpcBagJson = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"].Copy();
				this.IsInit = true;
			}
			catch (Exception ex)
			{
				Debug.LogError("初始化Npc数据错误,npcId为:" + npcId);
				Debug.LogError(ex);
			}
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x001DDE30 File Offset: 0x001DC030
		public void BackWriter()
		{
			jsonData.instance.AvatarJsonData.SetField(this.NpcId.ToString(), this.NpcBaseJson.Copy());
			jsonData.instance.AvatarRandomJsonData.SetField(this.NpcId.ToString(), this.NpcFaceJso.Copy());
			jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()].SetField("Backpack", this.NpcBagJson.Copy());
		}

		// Token: 0x040047FB RID: 18427
		public int NpcId;

		// Token: 0x040047FC RID: 18428
		public JSONObject NpcBaseJson;

		// Token: 0x040047FD RID: 18429
		public JSONObject NpcFaceJso;

		// Token: 0x040047FE RID: 18430
		public JSONObject NpcBagJson;

		// Token: 0x040047FF RID: 18431
		public UnityAction SetPlace;

		// Token: 0x04004800 RID: 18432
		public bool IsInit;
	}
}
