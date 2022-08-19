using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200031C RID: 796
public class SelectLunTi : MonoBehaviour
{
	// Token: 0x06001B96 RID: 7062 RVA: 0x000C4450 File Offset: 0x000C2650
	private void Awake()
	{
		foreach (JSONObject jsonobject in jsonData.instance.WuDaoAllTypeJson.list)
		{
			if (jsonobject["id"].I == 21)
			{
				break;
			}
			this.lunTiDictionary.Add(jsonobject["id"].I, jsonobject["name1"].Str);
		}
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x000C44E8 File Offset: 0x000C26E8
	public void Init()
	{
		base.gameObject.SetActive(true);
		Transform parent = this.lunTiCell.transform.parent;
		foreach (int num in this.lunTiDictionary.Keys)
		{
			LunTiCell component = Object.Instantiate<GameObject>(this.lunTiCell, parent).gameObject.GetComponent<LunTiCell>();
			component.InitLunTiCell(this.selectSprites[num], this.selectSprites[0], num, this.lunTiDictionary[num], new UnityAction<int>(this.AddLunTiToList), new UnityAction<int>(this.RemoveLunTiByList));
			component.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x000C45BC File Offset: 0x000C27BC
	public void AddLunTiToList(int id)
	{
		if (this.selectLunTiList.Contains(id))
		{
			return;
		}
		this.selectLunTiList.Add(id);
		this.startLunDaoCell.sanJiaoImage.sprite = this.startBtnSprites[0];
		this.startLunDaoCell.wenZi.sprite = this.startBtnSprites[2];
		this.startLunDaoCell.CanClick = true;
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x000C4628 File Offset: 0x000C2828
	public void RemoveLunTiByList(int id)
	{
		if (!this.selectLunTiList.Contains(id))
		{
			return;
		}
		this.selectLunTiList.Remove(id);
		if (this.selectLunTiList.Count < 1)
		{
			this.startLunDaoCell.sanJiaoImage.sprite = this.startBtnSprites[1];
			this.startLunDaoCell.wenZi.sprite = this.startBtnSprites[3];
			this.startLunDaoCell.CanClick = false;
		}
	}

	// Token: 0x04001618 RID: 5656
	[SerializeField]
	private GameObject lunTiCell;

	// Token: 0x04001619 RID: 5657
	[SerializeField]
	private List<Sprite> selectSprites;

	// Token: 0x0400161A RID: 5658
	public List<int> selectLunTiList;

	// Token: 0x0400161B RID: 5659
	[SerializeField]
	private List<Sprite> startBtnSprites;

	// Token: 0x0400161C RID: 5660
	[SerializeField]
	private StartLunDaoCell startLunDaoCell;

	// Token: 0x0400161D RID: 5661
	public Dictionary<int, string> lunTiDictionary = new Dictionary<int, string>();
}
