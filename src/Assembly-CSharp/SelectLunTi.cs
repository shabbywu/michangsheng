using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200047F RID: 1151
public class SelectLunTi : MonoBehaviour
{
	// Token: 0x06001EC9 RID: 7881 RVA: 0x00109678 File Offset: 0x00107878
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

	// Token: 0x06001ECA RID: 7882 RVA: 0x00109710 File Offset: 0x00107910
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

	// Token: 0x06001ECB RID: 7883 RVA: 0x001097E4 File Offset: 0x001079E4
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

	// Token: 0x06001ECC RID: 7884 RVA: 0x00109850 File Offset: 0x00107A50
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

	// Token: 0x04001A33 RID: 6707
	[SerializeField]
	private GameObject lunTiCell;

	// Token: 0x04001A34 RID: 6708
	[SerializeField]
	private List<Sprite> selectSprites;

	// Token: 0x04001A35 RID: 6709
	public List<int> selectLunTiList;

	// Token: 0x04001A36 RID: 6710
	[SerializeField]
	private List<Sprite> startBtnSprites;

	// Token: 0x04001A37 RID: 6711
	[SerializeField]
	private StartLunDaoCell startLunDaoCell;

	// Token: 0x04001A38 RID: 6712
	public Dictionary<int, string> lunTiDictionary = new Dictionary<int, string>();
}
