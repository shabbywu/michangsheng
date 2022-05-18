using System;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000E07 RID: 3591
	public class UIFightSelectLingQi : MonoBehaviour
	{
		// Token: 0x060056B9 RID: 22201 RVA: 0x00241AC0 File Offset: 0x0023FCC0
		private void Awake()
		{
			this.Jin.mouseUpEvent.AddListener(delegate()
			{
				this.OnSelectLingQi(LingQiType.金);
			});
			this.Mu.mouseUpEvent.AddListener(delegate()
			{
				this.OnSelectLingQi(LingQiType.木);
			});
			this.Shui.mouseUpEvent.AddListener(delegate()
			{
				this.OnSelectLingQi(LingQiType.水);
			});
			this.Huo.mouseUpEvent.AddListener(delegate()
			{
				this.OnSelectLingQi(LingQiType.火);
			});
			this.Tu.mouseUpEvent.AddListener(delegate()
			{
				this.OnSelectLingQi(LingQiType.土);
			});
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x0003DFE6 File Offset: 0x0003C1E6
		public void SetSelectAction(Action<LingQiType> callback)
		{
			this.onSelectLingQi = callback;
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x0003DFEF File Offset: 0x0003C1EF
		private void OnSelectLingQi(LingQiType lingQiType)
		{
			base.gameObject.SetActive(false);
			if (this.onSelectLingQi != null)
			{
				this.onSelectLingQi(lingQiType);
			}
		}

		// Token: 0x04005664 RID: 22116
		public FpBtn Jin;

		// Token: 0x04005665 RID: 22117
		public FpBtn Mu;

		// Token: 0x04005666 RID: 22118
		public FpBtn Shui;

		// Token: 0x04005667 RID: 22119
		public FpBtn Huo;

		// Token: 0x04005668 RID: 22120
		public FpBtn Tu;

		// Token: 0x04005669 RID: 22121
		private Action<LingQiType> onSelectLingQi;
	}
}
