using System;
using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A64 RID: 2660
	public class PaiMaiHost : MonoBehaviour
	{
		// Token: 0x0600448A RID: 17546 RVA: 0x001D4CBC File Offset: 0x001D2EBC
		public void Init()
		{
			this.NaiXin = 100;
			this._face.sprite = SingletonMono<PaiMaiUiMag>.Instance.HostSprites[Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.HostSprites.Count - 1)];
			this._naiXin.text = this.NaiXin.ToString();
			this._hostLevel = PaiMaiBiao.DataDict[SingletonMono<PaiMaiUiMag>.Instance.PaiMaiId].level;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00031057 File Offset: 0x0002F257
		public void SayWord(string msg, UnityAction complete = null, float time = 1f)
		{
			this.Say.SayWord(msg, complete, time);
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x001D4D3C File Offset: 0x001D2F3C
		public bool ReduceNaiXin(CeLueType type)
		{
			bool result = false;
			int num = 0;
			switch (type)
			{
			case CeLueType.神识威慑:
				num = 40;
				break;
			case CeLueType.言语恐吓:
				num = 50;
				break;
			case CeLueType.出言挑衅:
				num = 80;
				break;
			}
			this.NaiXin -= num;
			if (this.NaiXin < 0)
			{
				this.NaiXin = 0;
				if ((int)Tools.instance.getPlayer().level > this._hostLevel)
				{
					if (PaiMaiBiao.DataDict[SingletonMono<PaiMaiUiMag>.Instance.PaiMaiId].paimaifenzu == 2)
					{
						PlayerEx.AddShengWang(19, -this._reduceShengWang, true);
					}
					else
					{
						PlayerEx.AddShengWang(0, -this._reduceShengWang, true);
					}
					this._reduceShengWang += 5;
					result = true;
				}
			}
			else
			{
				result = true;
			}
			this._naiXin.text = this.NaiXin.ToString();
			DOTweenModuleUI.DOFillAmount(this._fill, (float)this.NaiXin / 100f, 0.5f);
			return result;
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x001D4E2C File Offset: 0x001D302C
		public void AddNaiXin()
		{
			this.NaiXin += 10;
			if (this.NaiXin > 100)
			{
				this.NaiXin = 100;
			}
			this._naiXin.text = this.NaiXin.ToString();
			DOTweenModuleUI.DOFillAmount(this._fill, (float)this.NaiXin / 100f, 0.5f);
		}

		// Token: 0x04003C8C RID: 15500
		[SerializeField]
		private Image _face;

		// Token: 0x04003C8D RID: 15501
		[SerializeField]
		private Text _naiXin;

		// Token: 0x04003C8E RID: 15502
		[SerializeField]
		private Image _fill;

		// Token: 0x04003C8F RID: 15503
		[SerializeField]
		private PaiMaiSay Say;

		// Token: 0x04003C90 RID: 15504
		public int NaiXin;

		// Token: 0x04003C91 RID: 15505
		private int _hostLevel;

		// Token: 0x04003C92 RID: 15506
		private int _reduceShengWang = 5;
	}
}
