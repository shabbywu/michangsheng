using System;
using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000714 RID: 1812
	public class PaiMaiHost : MonoBehaviour
	{
		// Token: 0x060039FE RID: 14846 RVA: 0x0018D5C0 File Offset: 0x0018B7C0
		public void Init()
		{
			this.NaiXin = 100;
			this._face.sprite = SingletonMono<PaiMaiUiMag>.Instance.HostSprites[Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.HostSprites.Count - 1)];
			this._naiXin.text = this.NaiXin.ToString();
			this._hostLevel = PaiMaiBiao.DataDict[SingletonMono<PaiMaiUiMag>.Instance.PaiMaiId].level;
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x0018D640 File Offset: 0x0018B840
		public void SayWord(string msg, UnityAction complete = null, float time = 1f)
		{
			this.Say.SayWord(msg, complete, time);
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0018D650 File Offset: 0x0018B850
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

		// Token: 0x06003A01 RID: 14849 RVA: 0x0018D740 File Offset: 0x0018B940
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

		// Token: 0x04003205 RID: 12805
		[SerializeField]
		private Image _face;

		// Token: 0x04003206 RID: 12806
		[SerializeField]
		private Text _naiXin;

		// Token: 0x04003207 RID: 12807
		[SerializeField]
		private Image _fill;

		// Token: 0x04003208 RID: 12808
		[SerializeField]
		private PaiMaiSay Say;

		// Token: 0x04003209 RID: 12809
		public int NaiXin;

		// Token: 0x0400320A RID: 12810
		private int _hostLevel;

		// Token: 0x0400320B RID: 12811
		private int _reduceShengWang = 5;
	}
}
