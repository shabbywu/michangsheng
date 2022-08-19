using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000279 RID: 633
public class UINPCShuangXiuSelect : MonoBehaviour, IESCClose
{
	// Token: 0x0600170F RID: 5903 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x0009D9B4 File Offset: 0x0009BBB4
	public void RefreshUI()
	{
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		this.selectedSkillID = -1;
		this.MiShuDescText.text = "请选择一种双修功法";
		this.ContentRT.DestoryAllChild();
		Avatar player = PlayerEx.Player;
		this.AddSkillItem(ShuangXiuMiShu.DataDict[1]);
		if (player.ShuangXiuData.HasField("HasSkillList"))
		{
			foreach (int key in player.ShuangXiuData["HasSkillList"].ToList())
			{
				this.AddSkillItem(ShuangXiuMiShu.DataDict[key]);
			}
		}
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x0009DA88 File Offset: 0x0009BC88
	public void AddSkillItem(ShuangXiuMiShu skill)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.ShuangXiuSkillPrefab, this.ContentRT);
		gameObject.GetComponentInChildren<Text>().text = skill.name;
		gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ResManager.inst.LoadSprite(string.Format("NewUI/NPCJiaoHu/ShuangXiuMiShu/ShuangXiuMiShuIcon{0}", skill.id));
		gameObject.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.selectedSkillID = skill.id;
			this.MiShuDescText.text = skill.desc;
		});
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x0009DB28 File Offset: 0x0009BD28
	public void OnOkBtnClick()
	{
		if (this.selectedSkillID < 1)
		{
			UIPopTip.Inst.Pop("没有选择双修功法", PopTipIconType.叹号);
			return;
		}
		Debug.Log("选择了双修功法:" + ShuangXiuMiShu.DataDict[this.selectedSkillID].name);
		PlayerEx.DoShuangXiu(this.selectedSkillID, this.npc);
		UINPCJiaoHu.Inst.HideNPCShuangXiuSelect();
		UINPCJiaoHu.Inst.ShowNPCShuangXiuAnim();
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x0009DB98 File Offset: 0x0009BD98
	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCShuangXiuSelect();
		return true;
	}

	// Token: 0x040011BB RID: 4539
	private UINPCData npc;

	// Token: 0x040011BC RID: 4540
	public RectTransform ContentRT;

	// Token: 0x040011BD RID: 4541
	public Text MiShuDescText;

	// Token: 0x040011BE RID: 4542
	public GameObject ShuangXiuSkillPrefab;

	// Token: 0x040011BF RID: 4543
	private int selectedSkillID;
}
