using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200039F RID: 927
public class UINPCShuangXiuSelect : MonoBehaviour, IESCClose
{
	// Token: 0x060019DD RID: 6621 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x000E52DC File Offset: 0x000E34DC
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

	// Token: 0x060019E0 RID: 6624 RVA: 0x000E53B0 File Offset: 0x000E35B0
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

	// Token: 0x060019E1 RID: 6625 RVA: 0x000E5450 File Offset: 0x000E3650
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

	// Token: 0x060019E2 RID: 6626 RVA: 0x0001641B File Offset: 0x0001461B
	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCShuangXiuSelect();
		return true;
	}

	// Token: 0x0400152C RID: 5420
	private UINPCData npc;

	// Token: 0x0400152D RID: 5421
	public RectTransform ContentRT;

	// Token: 0x0400152E RID: 5422
	public Text MiShuDescText;

	// Token: 0x0400152F RID: 5423
	public GameObject ShuangXiuSkillPrefab;

	// Token: 0x04001530 RID: 5424
	private int selectedSkillID;
}
