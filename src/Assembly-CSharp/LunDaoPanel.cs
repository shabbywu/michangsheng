using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LunDaoPanel : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> lunTiNameSpriteList;

	[SerializeField]
	public List<Sprite> wuDaoQiuSpriteList;

	[SerializeField]
	private GameObject startLunTiDaoCell;

	[SerializeField]
	private GameObject wuDaoQiuCell;

	[SerializeField]
	private GameObject moreLunTiDaoCell;

	[SerializeField]
	private GameObject moreWuDaoQiuCell;

	[SerializeField]
	private GameObject moreLunTiPanel;

	[SerializeField]
	private GameObject lunDaoQiuSlot;

	[SerializeField]
	private BtnCell moreWuDaoBtn;

	private Dictionary<int, List<int>> targetLunTiDictionary;

	public Dictionary<int, StartLunTiCell> lunTiCtrDictionary;

	public void Init()
	{
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		targetLunTiDictionary = LunDaoManager.inst.lunTiMag.targetLunTiDictionary;
		lunTiCtrDictionary = new Dictionary<int, StartLunTiCell>();
		int num = 0;
		foreach (int key in targetLunTiDictionary.Keys)
		{
			num++;
			if (num > 5)
			{
				StartLunTiCell component = Object.Instantiate<GameObject>(moreLunTiDaoCell, moreLunTiDaoCell.transform.parent).GetComponent<StartLunTiCell>();
				component.Init(lunTiNameSpriteList[key - 1], key);
				for (int i = 0; i < targetLunTiDictionary[key].Count; i++)
				{
					Object.Instantiate<GameObject>(moreWuDaoQiuCell, component.wuDaoParent).GetComponent<WuDaoQiu>().Init(wuDaoQiuSpriteList[key], targetLunTiDictionary[key][i]);
				}
				lunTiCtrDictionary.Add(key, component);
			}
			else
			{
				StartLunTiCell component2 = Object.Instantiate<GameObject>(startLunTiDaoCell, startLunTiDaoCell.transform.parent).GetComponent<StartLunTiCell>();
				component2.Init(lunTiNameSpriteList[key - 1], key);
				for (int j = 0; j < targetLunTiDictionary[key].Count; j++)
				{
					Object.Instantiate<GameObject>(wuDaoQiuCell, component2.wuDaoParent).GetComponent<WuDaoQiu>().Init(wuDaoQiuSpriteList[key], targetLunTiDictionary[key][j]);
				}
				lunTiCtrDictionary.Add(key, component2);
			}
		}
		if (num > 5)
		{
			((Component)moreWuDaoBtn).gameObject.SetActive(true);
			moreWuDaoBtn.mouseUp.AddListener((UnityAction)delegate
			{
				moreLunTiPanel.SetActive(true);
			});
		}
	}

	public void Show()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void CloseMoreLunTiPanel()
	{
		moreLunTiPanel.SetActive(false);
	}

	public void AddNullSlot()
	{
		LunDaoQiu component = Object.Instantiate<GameObject>(lunDaoQiuSlot, lunDaoQiuSlot.transform.parent).GetComponent<LunDaoQiu>();
		component.SetNull();
		((Component)component).gameObject.SetActive(true);
		LunDaoManager.inst.lunTiMag.curLunDianList.Add(component);
	}
}
