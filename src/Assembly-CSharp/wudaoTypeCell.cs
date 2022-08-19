using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200050E RID: 1294
public class wudaoTypeCell : MonoBehaviour
{
	// Token: 0x06002998 RID: 10648 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x0013DBAC File Offset: 0x0013BDAC
	public void click()
	{
		Avatar player = Tools.instance.getPlayer();
		WuDaoUIMag.inst.ClearCenten();
		List<JSONObject> list = jsonData.instance.WuDaoJson.list.FindAll((JSONObject aa) => aa["Type"].HasItem(this.Type));
		int i;
		int j;
		for (i = 1; i <= 5; i = j + 1)
		{
			List<JSONObject> list2 = list.FindAll((JSONObject aa) => aa["Lv"].I == i);
			int count = list2.Count;
			this.setBgTuPian(count, i - 1);
			List<GameObject> list3 = new List<GameObject>();
			foreach (JSONObject jsonobject in list2)
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.WuDaoCell, WuDaoUIMag.inst.WuDaoCententList[i - 1].transform);
				gameObject.transform.localPosition = Vector3.zero;
				list3.Add(gameObject);
				wuDaoUICell component = gameObject.GetComponent<wuDaoUICell>();
				component.ID = jsonobject["id"].I;
				component.wuDaoName.text = Tools.Code64(jsonobject["name"].str);
				component.castNum.text = string.Concat(jsonobject["Cast"].I);
				Sprite sprite = Resources.Load<Sprite>("WuDao Icon/" + jsonobject["icon"].str);
				component.icon.sprite = sprite;
			}
			this.setWudaoCell(list3, i - 1);
			j = i;
		}
		WuDaoUIMag.inst.NowType = this.Type;
		WuDaoUIMag.inst.wuDaoHelp1.text = string.Concat(new object[]
		{
			"[24a5d6]大道感悟：[-]通过领悟功法、神通能够提升你对大道的感悟。\n[24a5d6]触类旁通：[-]任意一系悟道达到融会贯通时，获得悟道点*1\n\n[ffe34b]当前进度:[-]",
			player.wuDaoMag.getWuDaoEx(this.Type),
			"/",
			player.wuDaoMag.getNowTypeExMax(this.Type)
		});
		WuDaoUIMag.inst.ResetCellButton();
		WuDaoUIMag.inst.ResetEx(this.Type);
		WuDaoUIMag.inst.upWuDaoDate();
		this.showCurGanWu(player);
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x0013DE08 File Offset: 0x0013C008
	private void setBgTuPian(int count, int index)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(this.Type);
		Image component = WuDaoUIMag.inst.BGs[index].transform.GetChild(0).gameObject.GetComponent<Image>();
		Image component2 = WuDaoUIMag.inst.BGs[index].transform.GetChild(1).gameObject.GetComponent<Image>();
		Text component3 = WuDaoUIMag.inst.BGs[index].transform.GetChild(2).gameObject.GetComponent<Text>();
		Text component4 = WuDaoUIMag.inst.BGs[index].transform.GetChild(3).gameObject.GetComponent<Text>();
		switch (count)
		{
		case 1:
			WuDaoUIMag.inst.BGs[index].sprite2D = WuDaoUIMag.inst.BGList[0];
			component2.sprite = WuDaoUIMag.inst.BGList[0];
			break;
		case 2:
			WuDaoUIMag.inst.BGs[index].sprite2D = WuDaoUIMag.inst.BGList[1];
			component2.sprite = WuDaoUIMag.inst.BGList[1];
			break;
		case 3:
			WuDaoUIMag.inst.BGs[index].sprite2D = WuDaoUIMag.inst.BGList[2];
			component2.sprite = WuDaoUIMag.inst.BGList[2];
			break;
		case 4:
			WuDaoUIMag.inst.BGs[index].sprite2D = WuDaoUIMag.inst.BGList[3];
			component2.sprite = WuDaoUIMag.inst.BGList[3];
			break;
		}
		if (wuDaoLevelByType < index + 1)
		{
			component2.gameObject.SetActive(true);
			component.sprite = WuDaoUIMag.inst.Sprites_Lv[1];
			component3.color = new Color(0.7411765f, 0.7254902f, 0.52156866f);
			component4.color = new Color(0.7411765f, 0.7254902f, 0.52156866f);
			return;
		}
		component2.gameObject.SetActive(false);
		component.sprite = WuDaoUIMag.inst.Sprites_Lv[0];
		component3.color = new Color(0.654902f, 0.4117647f, 0.09411765f);
		component4.color = new Color(1f, 0.8784314f, 0.4745098f);
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x0013E090 File Offset: 0x0013C290
	private void setWudaoCell(List<GameObject> list, int index)
	{
		float num = 0f;
		switch (index)
		{
		case 0:
			num = -0.4164159f;
			break;
		case 1:
			num = 0.003446271f;
			break;
		case 2:
			num = 0.3977272f;
			break;
		case 3:
			num = 0.7774349f;
			break;
		case 4:
			num = 1.15562f;
			break;
		}
		switch (list.Count)
		{
		case 1:
			list[0].transform.position = new Vector3(num, 3999.978f);
			return;
		case 2:
			list[0].transform.position = new Vector3(num, 4000.166f);
			list[1].transform.position = new Vector3(num, 3999.808f);
			return;
		case 3:
			list[0].transform.position = new Vector3(num, 4000.293f);
			list[1].transform.position = new Vector3(num, 4000.005f);
			list[2].transform.position = new Vector3(num, 3999.713f);
			return;
		case 4:
			list[0].transform.position = new Vector3(num, 4000.418f);
			list[1].transform.position = new Vector3(num, 4000.149f);
			list[2].transform.position = new Vector3(num, 3999.875f);
			list[3].transform.position = new Vector3(num, 3999.605f);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x0013E220 File Offset: 0x0013C420
	private void showCurGanWu(Avatar player)
	{
		this.CurWuDaoJingYan.text = player.wuDaoMag.getWuDaoEx(this.Type).I.ToString();
	}

	// Token: 0x040025F3 RID: 9715
	public int Type;

	// Token: 0x040025F4 RID: 9716
	public Text CurWuDaoJingYan;

	// Token: 0x040025F5 RID: 9717
	public Text TitleName;

	// Token: 0x040025F6 RID: 9718
	public Image typeIcon;

	// Token: 0x040025F7 RID: 9719
	public GameObject ImageBG;

	// Token: 0x040025F8 RID: 9720
	public GameObject WuDaoCell;
}
