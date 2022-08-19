using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x0200070D RID: 1805
	public class NpcUI : MonoBehaviour
	{
		// Token: 0x060039DA RID: 14810 RVA: 0x0018C3DF File Offset: 0x0018A5DF
		public void Init(int npcId)
		{
			this.NpcFace.SetNPCFace(npcId);
			this.NpcName.text = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].Str;
		}

		// Token: 0x040031EF RID: 12783
		[SerializeField]
		private PlayerSetRandomFace NpcFace;

		// Token: 0x040031F0 RID: 12784
		[SerializeField]
		private Text NpcName;
	}
}
