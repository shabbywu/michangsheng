using System;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000AC9 RID: 2761
	public class UIFightSelectLingQi : MonoBehaviour
	{
		// Token: 0x06004D6A RID: 19818 RVA: 0x00211790 File Offset: 0x0020F990
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

		// Token: 0x06004D6B RID: 19819 RVA: 0x00211829 File Offset: 0x0020FA29
		public void SetSelectAction(Action<LingQiType> callback)
		{
			this.onSelectLingQi = callback;
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x00211832 File Offset: 0x0020FA32
		private void OnSelectLingQi(LingQiType lingQiType)
		{
			base.gameObject.SetActive(false);
			if (this.onSelectLingQi != null)
			{
				this.onSelectLingQi(lingQiType);
			}
		}

		// Token: 0x04004C8A RID: 19594
		public FpBtn Jin;

		// Token: 0x04004C8B RID: 19595
		public FpBtn Mu;

		// Token: 0x04004C8C RID: 19596
		public FpBtn Shui;

		// Token: 0x04004C8D RID: 19597
		public FpBtn Huo;

		// Token: 0x04004C8E RID: 19598
		public FpBtn Tu;

		// Token: 0x04004C8F RID: 19599
		private Action<LingQiType> onSelectLingQi;
	}
}
