using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CySendBtn : MonoBehaviour
{
	public bool isShow;

	public BtnCell btnCell;

	public List<Sprite> btnSprites;

	public Image image;

	[SerializeField]
	private Transform selectParent;

	[SerializeField]
	private GameObject FisrtselectCell;

	[SerializeField]
	private GameObject AfterselectCell;

	private List<int> _paiMaiNpcList = new List<int> { 700, 917, 647, 648, 646 };

	public void Hide()
	{
		((Component)selectParent).gameObject.SetActive(false);
		((Component)this).gameObject.SetActive(false);
		image.sprite = btnSprites[0];
		isShow = false;
	}

	public void Click()
	{
		isShow = !isShow;
		if (isShow)
		{
			image.sprite = btnSprites[1];
			InitSelect();
			((Component)selectParent).gameObject.SetActive(true);
		}
		else
		{
			image.sprite = btnSprites[0];
			((Component)selectParent).gameObject.SetActive(false);
		}
	}

	private void InitSelect()
	{
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		Tools.ClearObj(FisrtselectCell.transform);
		if (CyTeShuNpc.DataDict.ContainsKey(CyUIMag.inst.npcList.curSelectFriend.npcId))
		{
			CyTeShuNpc data = CyTeShuNpc.DataDict[CyUIMag.inst.npcList.curSelectFriend.npcId];
			if (data.Type == 1)
			{
				CySelectWord paiMai = FisrtselectCell.Inst(selectParent).GetComponent<CySelectWord>();
				paiMai.btnCell.mouseUp.AddListener((UnityAction)delegate
				{
					paiMai.Say(data.PaiMaiID);
					Click();
				});
				paiMai.Init(CyPlayeQuestionData.DataDict[3].WenTi, 3);
				return;
			}
		}
		Avatar player = Tools.instance.getPlayer();
		CySelectWord questionWhere = FisrtselectCell.Inst(selectParent).GetComponent<CySelectWord>();
		questionWhere.btnCell.mouseUp.AddListener((UnityAction)delegate
		{
			questionWhere.Say();
			Click();
		});
		questionWhere.Init(CyPlayeQuestionData.DataDict[1].WenTi, 1);
		if (player.DongFuData.Count <= 0)
		{
			return;
		}
		CySelectWord dongFu = AfterselectCell.Inst(selectParent).GetComponent<CySelectWord>();
		dongFu.Init(CyPlayeQuestionData.DataDict[2].WenTi.Replace("{DongFuName}", "洞府"), 2);
		dongFu.btnCell.mouseEnter.AddListener((UnityAction)delegate
		{
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Expected O, but got Unknown
			dongFu.ChildPanel.SetActive(true);
			Tools.ClearObj(dongFu.ChildSelect.transform);
			int num = 0;
			int count = player.DongFuData.Count;
			foreach (string key in player.DongFuData.keys)
			{
				CyChildSelect component = dongFu.ChildSelect.Inst(dongFu.ChildPanel.transform).GetComponent<CyChildSelect>();
				component.Init(player.DongFuData[key]["DongFuName"].Str);
				if (count > 1 && num == 0)
				{
					component.Line.SetActive(false);
				}
				component.Btn.mouseUpEvent.AddListener((UnityAction)delegate
				{
					dongFu.Say(int.Parse(key.Replace("DongFu", "")));
					Click();
				});
				num++;
			}
		});
		dongFu.btnCell.mouseOut.AddListener((UnityAction)delegate
		{
			dongFu.ChildPanel.SetActive(false);
		});
		((Component)dongFu).transform.SetAsFirstSibling();
	}
}
