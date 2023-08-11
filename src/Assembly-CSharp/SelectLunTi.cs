using System.Collections.Generic;
using UnityEngine;

public class SelectLunTi : MonoBehaviour
{
	[SerializeField]
	private GameObject lunTiCell;

	[SerializeField]
	private List<Sprite> selectSprites;

	public List<int> selectLunTiList;

	[SerializeField]
	private List<Sprite> startBtnSprites;

	[SerializeField]
	private StartLunDaoCell startLunDaoCell;

	public Dictionary<int, string> lunTiDictionary = new Dictionary<int, string>();

	private void Awake()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoAllTypeJson.list)
		{
			if (item["id"].I == 21)
			{
				break;
			}
			lunTiDictionary.Add(item["id"].I, item["name1"].Str);
		}
	}

	public void Init()
	{
		((Component)this).gameObject.SetActive(true);
		Transform parent = lunTiCell.transform.parent;
		foreach (int key in lunTiDictionary.Keys)
		{
			LunTiCell component = Object.Instantiate<GameObject>(lunTiCell, parent).gameObject.GetComponent<LunTiCell>();
			component.InitLunTiCell(selectSprites[key], selectSprites[0], key, lunTiDictionary[key], AddLunTiToList, RemoveLunTiByList);
			((Component)component).gameObject.SetActive(true);
		}
	}

	public void AddLunTiToList(int id)
	{
		if (!selectLunTiList.Contains(id))
		{
			selectLunTiList.Add(id);
			startLunDaoCell.sanJiaoImage.sprite = startBtnSprites[0];
			startLunDaoCell.wenZi.sprite = startBtnSprites[2];
			startLunDaoCell.CanClick = true;
		}
	}

	public void RemoveLunTiByList(int id)
	{
		if (selectLunTiList.Contains(id))
		{
			selectLunTiList.Remove(id);
			if (selectLunTiList.Count < 1)
			{
				startLunDaoCell.sanJiaoImage.sprite = startBtnSprites[1];
				startLunDaoCell.wenZi.sprite = startBtnSprites[3];
				startLunDaoCell.CanClick = false;
			}
		}
	}
}
