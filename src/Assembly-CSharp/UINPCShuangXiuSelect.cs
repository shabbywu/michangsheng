using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UINPCShuangXiuSelect : MonoBehaviour, IESCClose
{
	private UINPCData npc;

	public RectTransform ContentRT;

	public Text MiShuDescText;

	public GameObject ShuangXiuSkillPrefab;

	private int selectedSkillID;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void RefreshUI()
	{
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		npc.RefreshData();
		selectedSkillID = -1;
		MiShuDescText.text = "请选择一种双修功法";
		((Transform)(object)ContentRT).DestoryAllChild();
		Avatar player = PlayerEx.Player;
		AddSkillItem(ShuangXiuMiShu.DataDict[1]);
		if (!player.ShuangXiuData.HasField("HasSkillList"))
		{
			return;
		}
		foreach (int item in player.ShuangXiuData["HasSkillList"].ToList())
		{
			AddSkillItem(ShuangXiuMiShu.DataDict[item]);
		}
	}

	public void AddSkillItem(ShuangXiuMiShu skill)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		GameObject obj = Object.Instantiate<GameObject>(ShuangXiuSkillPrefab, (Transform)(object)ContentRT);
		obj.GetComponentInChildren<Text>().text = skill.name;
		((Component)obj.transform.GetChild(0)).GetComponent<Image>().sprite = ResManager.inst.LoadSprite($"NewUI/NPCJiaoHu/ShuangXiuMiShu/ShuangXiuMiShuIcon{skill.id}");
		((UnityEvent)obj.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
		{
			selectedSkillID = skill.id;
			MiShuDescText.text = skill.desc;
		});
	}

	public void OnOkBtnClick()
	{
		if (selectedSkillID < 1)
		{
			UIPopTip.Inst.Pop("没有选择双修功法");
			return;
		}
		Debug.Log((object)("选择了双修功法:" + ShuangXiuMiShu.DataDict[selectedSkillID].name));
		PlayerEx.DoShuangXiu(selectedSkillID, npc);
		UINPCJiaoHu.Inst.HideNPCShuangXiuSelect();
		UINPCJiaoHu.Inst.ShowNPCShuangXiuAnim();
	}

	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCShuangXiuSelect();
		return true;
	}
}
