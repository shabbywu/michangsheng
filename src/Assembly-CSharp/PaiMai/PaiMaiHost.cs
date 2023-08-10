using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai;

public class PaiMaiHost : MonoBehaviour
{
	[SerializeField]
	private Image _face;

	[SerializeField]
	private Text _naiXin;

	[SerializeField]
	private Image _fill;

	[SerializeField]
	private PaiMaiSay Say;

	public int NaiXin;

	private int _hostLevel;

	private int _reduceShengWang = 5;

	public void Init()
	{
		NaiXin = 100;
		_face.sprite = SingletonMono<PaiMaiUiMag>.Instance.HostSprites[Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.HostSprites.Count - 1)];
		_naiXin.text = NaiXin.ToString();
		_hostLevel = PaiMaiBiao.DataDict[SingletonMono<PaiMaiUiMag>.Instance.PaiMaiId].level;
	}

	public void SayWord(string msg, UnityAction complete = null, float time = 1f)
	{
		Say.SayWord(msg, complete, time);
	}

	public bool ReduceNaiXin(CeLueType type)
	{
		bool result = false;
		int num = 0;
		switch (type)
		{
		case CeLueType.出言挑衅:
			num = 80;
			break;
		case CeLueType.神识威慑:
			num = 40;
			break;
		case CeLueType.言语恐吓:
			num = 50;
			break;
		}
		NaiXin -= num;
		if (NaiXin < 0)
		{
			NaiXin = 0;
			if (Tools.instance.getPlayer().level > _hostLevel)
			{
				if (PaiMaiBiao.DataDict[SingletonMono<PaiMaiUiMag>.Instance.PaiMaiId].paimaifenzu == 2)
				{
					PlayerEx.AddShengWang(19, -_reduceShengWang, show: true);
				}
				else
				{
					PlayerEx.AddShengWang(0, -_reduceShengWang, show: true);
				}
				_reduceShengWang += 5;
				result = true;
			}
		}
		else
		{
			result = true;
		}
		_naiXin.text = NaiXin.ToString();
		DOTweenModuleUI.DOFillAmount(_fill, (float)NaiXin / 100f, 0.5f);
		return result;
	}

	public void AddNaiXin()
	{
		NaiXin += 10;
		if (NaiXin > 100)
		{
			NaiXin = 100;
		}
		_naiXin.text = NaiXin.ToString();
		DOTweenModuleUI.DOFillAmount(_fill, (float)NaiXin / 100f, 0.5f);
	}
}
