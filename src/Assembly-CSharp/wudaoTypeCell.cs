using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200079F RID: 1951
public class wudaoTypeCell : MonoBehaviour
{
	// Token: 0x0600319D RID: 12701 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x0018AD84 File Offset: 0x00188F84
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

	// Token: 0x0600319F RID: 12703 RVA: 0x0018AFE0 File Offset: 0x001891E0
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

	// Token: 0x060031A0 RID: 12704 RVA: 0x0018B268 File Offset: 0x00189468
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

	// Token: 0x060031A1 RID: 12705 RVA: 0x0018B3F8 File Offset: 0x001895F8
	private void showCurGanWu(Avatar player)
	{
		this.CurWuDaoJingYan.text = player.wuDaoMag.getWuDaoEx(this.Type).I.ToString();
	}

	// Token: 0x04002DDB RID: 11739
	public int Type;

	// Token: 0x04002DDC RID: 11740
	public Text CurWuDaoJingYan;

	// Token: 0x04002DDD RID: 11741
	public Text TitleName;

	// Token: 0x04002DDE RID: 11742
	public Image typeIcon;

	// Token: 0x04002DDF RID: 11743
	public GameObject ImageBG;

	// Token: 0x04002DE0 RID: 11744
	public GameObject WuDaoCell;
}
