using UnityEngine;

public class UINPCWuDaoPanel : TabPanelBase
{
	private UINPCData npc;

	public RectTransform ContentRT;

	public GameObject SVItemPrefab;

	public override void OnPanelShow()
	{
		base.OnPanelShow();
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		((Transform)(object)ContentRT).DestoryAllChild();
		foreach (UINPCWuDaoData item in npc.WuDao)
		{
			Object.Instantiate<GameObject>(SVItemPrefab, (Transform)(object)ContentRT).GetComponent<UINPCWuDaoSVItem>().SetWuDao(item);
		}
	}
}
