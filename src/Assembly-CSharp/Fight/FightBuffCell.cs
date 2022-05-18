using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
	// Token: 0x02000A7E RID: 2686
	public class FightBuffCell : MonoBehaviour
	{
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x00031661 File Offset: 0x0002F861
		// (set) Token: 0x06004508 RID: 17672 RVA: 0x00031642 File Offset: 0x0002F842
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

		// Token: 0x0600450A RID: 17674 RVA: 0x00031669 File Offset: 0x0002F869
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

		// Token: 0x04003D26 RID: 15654
		[SerializeField]
		private Text NumText;

		// Token: 0x04003D27 RID: 15655
		[SerializeField]
		private Image Icon;

		// Token: 0x04003D28 RID: 15656
		[SerializeField]
		private int buffCount;

		// Token: 0x04003D29 RID: 15657
		public int Id;

		// Token: 0x04003D2A RID: 15658
		public string Desc;
	}
}
