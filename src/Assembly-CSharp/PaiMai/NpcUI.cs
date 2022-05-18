using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A5D RID: 2653
	public class NpcUI : MonoBehaviour
	{
		// Token: 0x06004466 RID: 17510 RVA: 0x00030F24 File Offset: 0x0002F124
		public void Init(int npcId)
		{
			this.NpcFace.SetNPCFace(npcId);
			this.NpcName.text = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].Str;
		}

		// Token: 0x04003C76 RID: 15478
		[SerializeField]
		private PlayerSetRandomFace NpcFace;

		// Token: 0x04003C77 RID: 15479
		[SerializeField]
		private Text NpcName;
	}
}
