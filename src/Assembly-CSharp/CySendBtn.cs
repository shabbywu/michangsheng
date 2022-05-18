using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D1 RID: 977
public class CySendBtn : MonoBehaviour
{
	// Token: 0x06001AE1 RID: 6881 RVA: 0x00016C8E File Offset: 0x00014E8E
	public void Hide()
	{
		this.selectParent.gameObject.SetActive(false);
		base.gameObject.SetActive(false);
		this.image.sprite = this.btnSprites[0];
		this.isShow = false;
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x000EE56C File Offset: 0x000EC76C
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

	// Token: 0x06001AE3 RID: 6883 RVA: 0x000EE5E8 File Offset: 0x000EC7E8
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

	// Token: 0x0400166C RID: 5740
	public bool isShow;

	// Token: 0x0400166D RID: 5741
	public BtnCell btnCell;

	// Token: 0x0400166E RID: 5742
	public List<Sprite> btnSprites;

	// Token: 0x0400166F RID: 5743
	public Image image;

	// Token: 0x04001670 RID: 5744
	[SerializeField]
	private Transform selectParent;

	// Token: 0x04001671 RID: 5745
	[SerializeField]
	private GameObject FisrtselectCell;

	// Token: 0x04001672 RID: 5746
	[SerializeField]
	private GameObject AfterselectCell;

	// Token: 0x04001673 RID: 5747
	private List<int> _paiMaiNpcList = new List<int>
	{
		700,
		917,
		647,
		648,
		646
	};
}
