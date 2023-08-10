using Bag;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace script.MenPaiTask.ZhangLao.UI.Base;

public class ElderTaskSlot : BaseSlot
{
	public Sprite nomalSprite;

	public Sprite mouseEnterSprite;

	public Sprite mouseDownSprite;

	private Image bg;

	public bool IsInBag;

	public override void InitUI()
	{
		if (!IsInBag)
		{
			bg = Get<Image>("Null/BG");
		}
		base.InitUI();
	}

	public override bool CanDrag()
	{
		return false;
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!IsInBag)
		{
			bg.sprite = mouseDownSprite;
		}
		base.OnPointerDown(eventData);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		IsIn = true;
		if (!IsInBag)
		{
			bg.sprite = mouseDownSprite;
		}
		if (!IsNull())
		{
			if (IsInBag)
			{
				_selectPanel.SetActive(true);
			}
			if ((Object)(object)ToolTipsMag.Inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
			}
			ToolTipsMag.Inst.Show(Item);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Invalid comparison between Unknown and I4
		if (IsInBag)
		{
			if (!IsNull())
			{
				ElderTaskUIMag.Inst.CreateElderTaskUI.Ctr.PutItem(this);
				_selectPanel.SetActive(false);
			}
			return;
		}
		if ((int)eventData.button == 0)
		{
			ElderTaskUIMag.Inst.Bag.ToSlot = this;
			ElderTaskUIMag.Inst.Bag.Open();
		}
		else if ((int)eventData.button == 1 && !IsNull())
		{
			ElderTaskUIMag.Inst.CreateElderTaskUI.Ctr.BackItem(this);
		}
		bg.sprite = nomalSprite;
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		if (!IsInBag)
		{
			bg.sprite = nomalSprite;
		}
		base.OnPointerExit(eventData);
	}
}
