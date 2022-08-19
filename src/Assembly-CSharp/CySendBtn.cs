using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029D RID: 669
public class CySendBtn : MonoBehaviour
{
	// Token: 0x060017F9 RID: 6137 RVA: 0x000A7432 File Offset: 0x000A5632
	public void Hide()
	{
		this.selectParent.gameObject.SetActive(false);
		base.gameObject.SetActive(false);
		this.image.sprite = this.btnSprites[0];
		this.isShow = false;
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000A7470 File Offset: 0x000A5670
	public void Click()
	{
		this.isShow = !this.isShow;
		if (this.isShow)
		{
			this.image.sprite = this.btnSprites[1];
			this.InitSelect();
			this.selectParent.gameObject.SetActive(true);
			return;
		}
		this.image.sprite = this.btnSprites[0];
		this.selectParent.gameObject.SetActive(false);
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000A74EC File Offset: 0x000A56EC
	private void InitSelect()
	{
		Tools.ClearObj(this.FisrtselectCell.transform);
		if (CyTeShuNpc.DataDict.ContainsKey(CyUIMag.inst.npcList.curSelectFriend.npcId))
		{
			CyTeShuNpc data = CyTeShuNpc.DataDict[CyUIMag.inst.npcList.curSelectFriend.npcId];
			if (data.Type == 1)
			{
				CySelectWord paiMai = this.FisrtselectCell.Inst(this.selectParent).GetComponent<CySelectWord>();
				paiMai.btnCell.mouseUp.AddListener(delegate()
				{
					paiMai.Say(data.PaiMaiID);
					this.Click();
				});
				paiMai.Init(CyPlayeQuestionData.DataDict[3].WenTi, 3);
				return;
			}
		}
		Avatar player = Tools.instance.getPlayer();
		CySelectWord questionWhere = this.FisrtselectCell.Inst(this.selectParent).GetComponent<CySelectWord>();
		questionWhere.btnCell.mouseUp.AddListener(delegate()
		{
			questionWhere.Say(null);
			this.Click();
		});
		questionWhere.Init(CyPlayeQuestionData.DataDict[1].WenTi, 1);
		if (player.DongFuData.Count > 0)
		{
			CySelectWord dongFu = this.AfterselectCell.Inst(this.selectParent).GetComponent<CySelectWord>();
			dongFu.Init(CyPlayeQuestionData.DataDict[2].WenTi.Replace("{DongFuName}", "洞府"), 2);
			dongFu.btnCell.mouseEnter.AddListener(delegate()
			{
				dongFu.ChildPanel.SetActive(true);
				Tools.ClearObj(dongFu.ChildSelect.transform);
				int num = 0;
				int count = player.DongFuData.Count;
				using (List<string>.Enumerator enumerator = player.DongFuData.keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						CyChildSelect component = dongFu.ChildSelect.Inst(dongFu.ChildPanel.transform).GetComponent<CyChildSelect>();
						component.Init(player.DongFuData[key]["DongFuName"].Str);
						if (count > 1 && num == 0)
						{
							component.Line.SetActive(false);
						}
						component.Btn.mouseUpEvent.AddListener(delegate()
						{
							dongFu.Say(int.Parse(key.Replace("DongFu", "")));
							this.Click();
						});
						num++;
					}
				}
			});
			dongFu.btnCell.mouseOut.AddListener(delegate()
			{
				dongFu.ChildPanel.SetActive(false);
			});
			dongFu.transform.SetAsFirstSibling();
		}
	}

	// Token: 0x040012DB RID: 4827
	public bool isShow;

	// Token: 0x040012DC RID: 4828
	public BtnCell btnCell;

	// Token: 0x040012DD RID: 4829
	public List<Sprite> btnSprites;

	// Token: 0x040012DE RID: 4830
	public Image image;

	// Token: 0x040012DF RID: 4831
	[SerializeField]
	private Transform selectParent;

	// Token: 0x040012E0 RID: 4832
	[SerializeField]
	private GameObject FisrtselectCell;

	// Token: 0x040012E1 RID: 4833
	[SerializeField]
	private GameObject AfterselectCell;

	// Token: 0x040012E2 RID: 4834
	private List<int> _paiMaiNpcList = new List<int>
	{
		700,
		917,
		647,
		648,
		646
	};
}
