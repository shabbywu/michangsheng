using System;
using UnityEngine;

namespace script.NpcAction
{
	// Token: 0x02000ABF RID: 2751
	public class NpcData
	{
		// Token: 0x0600464C RID: 17996 RVA: 0x001DF9B4 File Offset: 0x001DDBB4
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

		// Token: 0x0600464D RID: 17997 RVA: 0x001DFA78 File Offset: 0x001DDC78
		public void BackWriter()
		{
			jsonData.instance.AvatarJsonData.SetField(this.NpcId.ToString(), this.NpcBaseJson.Copy());
			jsonData.instance.AvatarRandomJsonData.SetField(this.NpcId.ToString(), this.NpcFaceJso.Copy());
			jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()].SetField("Backpack", this.NpcBagJson.Copy());
		}

		// Token: 0x04003E6E RID: 15982
		public int NpcId;

		// Token: 0x04003E6F RID: 15983
		public JSONObject NpcBaseJson;

		// Token: 0x04003E70 RID: 15984
		public JSONObject NpcFaceJso;

		// Token: 0x04003E71 RID: 15985
		public JSONObject NpcBagJson;

		// Token: 0x04003E72 RID: 15986
		public bool IsInit;
	}
}
