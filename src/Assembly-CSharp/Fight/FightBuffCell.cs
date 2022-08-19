using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
	// Token: 0x02000727 RID: 1831
	public class FightBuffCell : MonoBehaviour
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x00191322 File Offset: 0x0018F522
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x00191303 File Offset: 0x0018F503
		public int BuffCount
		{
			get
			{
				return this.buffCount;
			}
			set
			{
				this.buffCount = value;
				this.NumText.text = this.buffCount.ToString();
			}
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x0019132A File Offset: 0x0018F52A
		public void Init(int id, int count, string desc, Sprite sprite = null)
		{
			this.Id = id;
			this.BuffCount = count;
			this.Desc = desc;
			if (sprite != null)
			{
				this.Icon.sprite = sprite;
			}
		}

		// Token: 0x0400328B RID: 12939
		[SerializeField]
		private Text NumText;

		// Token: 0x0400328C RID: 12940
		[SerializeField]
		private Image Icon;

		// Token: 0x0400328D RID: 12941
		[SerializeField]
		private int buffCount;

		// Token: 0x0400328E RID: 12942
		public int Id;

		// Token: 0x0400328F RID: 12943
		public string Desc;
	}
}
