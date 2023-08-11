using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class wudaoTypeCell : MonoBehaviour
{
	public int Type;

	public Text CurWuDaoJingYan;

	public Text TitleName;

	public Image typeIcon;

	public GameObject ImageBG;

	public GameObject WuDaoCell;

	private void Start()
	{
	}

	public void click()
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		WuDaoUIMag.inst.ClearCenten();
		List<JSONObject> list = jsonData.instance.WuDaoJson.list.FindAll((JSONObject aa) => aa["Type"].HasItem(Type));
		int i;
		for (i = 1; i <= 5; i++)
		{
			List<JSONObject> list2 = list.FindAll((JSONObject aa) => aa["Lv"].I == i);
			int count = list2.Count;
			setBgTuPian(count, i - 1);
			List<GameObject> list3 = new List<GameObject>();
			foreach (JSONObject item in list2)
			{
				GameObject val = Tools.InstantiateGameObject(WuDaoCell, WuDaoUIMag.inst.WuDaoCententList[i - 1].transform);
				val.transform.localPosition = Vector3.zero;
				list3.Add(val);
				wuDaoUICell component = val.GetComponent<wuDaoUICell>();
				component.ID = item["id"].I;
				component.wuDaoName.text = Tools.Code64(item["name"].str);
				component.castNum.text = string.Concat(item["Cast"].I);
				Sprite sprite = Resources.Load<Sprite>("WuDao Icon/" + item["icon"].str);
				component.icon.sprite = sprite;
			}
			setWudaoCell(list3, i - 1);
		}
		WuDaoUIMag.inst.NowType = Type;
		WuDaoUIMag.inst.wuDaoHelp1.text = string.Concat("[24a5d6]大道感悟：[-]通过领悟功法、神通能够提升你对大道的感悟。\n[24a5d6]触类旁通：[-]任意一系悟道达到融会贯通时，获得悟道点*1\n\n[ffe34b]当前进度:[-]", player.wuDaoMag.getWuDaoEx(Type), "/", player.wuDaoMag.getNowTypeExMax(Type));
		WuDaoUIMag.inst.ResetCellButton();
		WuDaoUIMag.inst.ResetEx(Type);
		WuDaoUIMag.inst.upWuDaoDate();
		showCurGanWu(player);
	}

	private void setBgTuPian(int count, int index)
	{
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(Type);
		Image component = ((Component)((Component)WuDaoUIMag.inst.BGs[index]).transform.GetChild(0)).gameObject.GetComponent<Image>();
		Image component2 = ((Component)((Component)WuDaoUIMag.inst.BGs[index]).transform.GetChild(1)).gameObject.GetComponent<Image>();
		Text component3 = ((Component)((Component)WuDaoUIMag.inst.BGs[index]).transform.GetChild(2)).gameObject.GetComponent<Text>();
		Text component4 = ((Component)((Component)WuDaoUIMag.inst.BGs[index]).transform.GetChild(3)).gameObject.GetComponent<Text>();
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
			((Component)component2).gameObject.SetActive(true);
			component.sprite = WuDaoUIMag.inst.Sprites_Lv[1];
			((Graphic)component3).color = new Color(63f / 85f, 37f / 51f, 0.52156866f);
			((Graphic)component4).color = new Color(63f / 85f, 37f / 51f, 0.52156866f);
		}
		else
		{
			((Component)component2).gameObject.SetActive(false);
			component.sprite = WuDaoUIMag.inst.Sprites_Lv[0];
			((Graphic)component3).color = new Color(0.654902f, 0.4117647f, 8f / 85f);
			((Graphic)component4).color = new Color(1f, 0.8784314f, 0.4745098f);
		}
	}

	private void setWudaoCell(List<GameObject> list, int index)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
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
			break;
		case 2:
			list[0].transform.position = new Vector3(num, 4000.166f);
			list[1].transform.position = new Vector3(num, 3999.808f);
			break;
		case 3:
			list[0].transform.position = new Vector3(num, 4000.293f);
			list[1].transform.position = new Vector3(num, 4000.005f);
			list[2].transform.position = new Vector3(num, 3999.713f);
			break;
		case 4:
			list[0].transform.position = new Vector3(num, 4000.418f);
			list[1].transform.position = new Vector3(num, 4000.149f);
			list[2].transform.position = new Vector3(num, 3999.875f);
			list[3].transform.position = new Vector3(num, 3999.605f);
			break;
		}
	}

	private void showCurGanWu(Avatar player)
	{
		CurWuDaoJingYan.text = player.wuDaoMag.getWuDaoEx(Type).I.ToString();
	}
}
