using UnityEngine;

public class UINPCEventPanel : TabPanelBase
{
	private UINPCData npc;

	public RectTransform ContentRT;

	public GameObject SVItemPrefab;

	public override void OnPanelShow()
	{
		base.OnPanelShow();
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		((Transform)(object)ContentRT).DestoryAllChild();
		foreach (UINPCEventData @event in npc.Events)
		{
			Object.Instantiate<GameObject>(SVItemPrefab, (Transform)(object)ContentRT).GetComponent<UINPCEventSVItem>().SetEvent($"第{@event.EventTime.Year}年{@event.EventTime.Month}月", @event.EventDesc);
		}
	}
}
