using Bag;
using JiaoYi;
using PaiMai;
using Sirenix.Utilities;
using Tab;
using UnityEngine;
using UnityEngine.UI;
using script.Submit;

public class DragMag
{
	private class TempSlot
	{
		private Image Image;

		private Transform transform;

		private RectTransform rect;

		private GameObject gameObject;

		public static TempSlot Create(Transform parent)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			TempSlot obj = new TempSlot
			{
				Image = new GameObject("TempImag").AddComponent<Image>()
			};
			((Graphic)obj.Image).raycastTarget = false;
			obj.gameObject = ((Component)obj.Image).gameObject;
			obj.transform = obj.gameObject.transform;
			obj.rect = obj.gameObject.GetComponent<RectTransform>();
			obj.transform.parent = parent;
			obj.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			return obj;
		}

		public void Show(Sprite sprite)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			gameObject.SetActive(true);
			transform.SetAsLastSibling();
			Image.sprite = sprite;
			RectExtensions.SetWidth(rect.rect, 128f);
			RectExtensions.SetWidth(rect.rect, 128f);
		}

		public void UpdatePostion(Vector3 vector3)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			transform.position = vector3;
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}

	private static DragMag _inst;

	public SlotBase DragSlot;

	public SlotBase ToSlot;

	public bool IsDraging;

	private TempSlot _tempSlot;

	public static DragMag Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new DragMag();
			}
			return _inst;
		}
	}

	private TempSlot tempSlot
	{
		get
		{
			if (_tempSlot == null)
			{
				_tempSlot = TempSlot.Create(((Component)NewUICanvas.Inst).transform);
			}
			return _tempSlot;
		}
	}

	public void Clear()
	{
		DragSlot = null;
		ToSlot = null;
		IsDraging = false;
		tempSlot.Hide();
	}

	public void StartDrag(SlotBase slot)
	{
		DragSlot = slot;
		IsDraging = true;
		tempSlot.Show(slot.GetIcon());
	}

	public bool EndDrag()
	{
		bool result = false;
		if ((Object)(object)ToSlot != (Object)null && (Object)(object)ToSlot != (Object)(object)DragSlot && ToSlot.Group == DragSlot.Group)
		{
			if (ToSlot is EquipSlot)
			{
				if (DragSlot.Item != null && DragSlot.Item.CanPutSlotType == ToSlot.AcceptType)
				{
					if (DragSlot is EquipSlot && Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
					{
						EquipSlot equipSlot = (EquipSlot)DragSlot;
						EquipSlot equipSlot2 = (EquipSlot)ToSlot;
						SingletonMono<TabUIMag>.Instance.WuPingPanel.ExEquip(equipSlot.EquipSlotType, equipSlot2.EquipSlotType);
					}
					else
					{
						EquipSlot equipSlot3 = (EquipSlot)ToSlot;
						EquipItem equipItem = (EquipItem)DragSlot.Item;
						SingletonMono<TabUIMag>.Instance.WuPingPanel.AddEquip((int)equipSlot3.EquipSlotType, (EquipItem)equipItem.Clone());
					}
					result = true;
				}
			}
			else if (ToSlot is PasstiveSkillSlot)
			{
				if (DragSlot.Skill != null && DragSlot.Skill.CanPutSlotType == ToSlot.AcceptType)
				{
					if (DragSlot is PasstiveSkillSlot)
					{
						PasstiveSkillSlot passtiveSkillSlot = (PasstiveSkillSlot)DragSlot;
						PasstiveSkillSlot passtiveSkillSlot2 = (PasstiveSkillSlot)ToSlot;
						SingletonMono<TabUIMag>.Instance.GongFaPanel.ExSkill(passtiveSkillSlot.SkillSlotType, passtiveSkillSlot2.SkillSlotType);
						result = true;
					}
					else
					{
						PasstiveSkillSlot passtiveSkillSlot3 = (PasstiveSkillSlot)ToSlot;
						SingletonMono<TabUIMag>.Instance.GongFaPanel.AddSkill(passtiveSkillSlot3.SkillSlotType, DragSlot.Skill.Clone(), SingletonMono<TabUIMag>.Instance.GongFaPanel.GetSameSkillIndex(DragSlot.Skill));
						result = true;
					}
				}
			}
			else if (ToSlot is ActiveSkillSlot)
			{
				if (DragSlot.Skill != null && DragSlot.Skill.CanPutSlotType == ToSlot.AcceptType)
				{
					if (DragSlot is ActiveSkillSlot)
					{
						ActiveSkillSlot activeSkillSlot = (ActiveSkillSlot)DragSlot;
						ActiveSkillSlot activeSkillSlot2 = (ActiveSkillSlot)ToSlot;
						SingletonMono<TabUIMag>.Instance.ShenTongPanel.ExSkill(activeSkillSlot.index, activeSkillSlot2.index);
						result = true;
					}
					else if (SingletonMono<TabUIMag>.Instance.ShenTongPanel.CanAddSkill(DragSlot.Skill))
					{
						ActiveSkillSlot activeSkillSlot3 = (ActiveSkillSlot)ToSlot;
						SingletonMono<TabUIMag>.Instance.ShenTongPanel.AddSkill(activeSkillSlot3.index, DragSlot.Skill.Clone());
						result = true;
					}
				}
			}
			else if (ToSlot is JiaoYiSlot)
			{
				JiaoYiSlot jiaoYiSlot = (JiaoYiSlot)DragSlot;
				JiaoYiSlot jiaoYiSlot2 = (JiaoYiSlot)ToSlot;
				if (jiaoYiSlot.IsInBag && !jiaoYiSlot2.IsInBag && jiaoYiSlot.IsPlayer == jiaoYiSlot2.IsPlayer)
				{
					JiaoYiUIMag.Inst.SellItem(jiaoYiSlot, jiaoYiSlot2);
				}
				else if (!jiaoYiSlot.IsInBag && jiaoYiSlot2.IsInBag && jiaoYiSlot.IsPlayer == jiaoYiSlot2.IsPlayer)
				{
					JiaoYiUIMag.Inst.BackItem(jiaoYiSlot, jiaoYiSlot2);
				}
			}
			else if (ToSlot is PaiMaiSlot)
			{
				PaiMaiSlot paiMaiSlot = (PaiMaiSlot)DragSlot;
				PaiMaiSlot paiMaiSlot2 = (PaiMaiSlot)ToSlot;
				if (paiMaiSlot.IsInBag && !paiMaiSlot2.IsInBag && paiMaiSlot.IsPlayer == paiMaiSlot2.IsPlayer)
				{
					NewPaiMaiJoin.Inst.PutItem(paiMaiSlot, paiMaiSlot2);
				}
				else if (!paiMaiSlot.IsInBag && paiMaiSlot2.IsInBag && paiMaiSlot.IsPlayer == paiMaiSlot2.IsPlayer)
				{
					NewPaiMaiJoin.Inst.BackItem(paiMaiSlot, paiMaiSlot2);
				}
			}
			else if (ToSlot is UITianJieSkillSlot)
			{
				UITianJieSkillSlot uITianJieSkillSlot = (UITianJieSkillSlot)DragSlot;
				UITianJieSkillSlot uITianJieSkillSlot2 = (UITianJieSkillSlot)ToSlot;
				if (uITianJieSkillSlot2.IsEquipSlot)
				{
					BagTianJieSkill tianJieSkill = uITianJieSkillSlot.TianJieSkill;
					UIDuJieZhunBei.Inst.ClearTiaoZhengSlotByID(tianJieSkill.MiShu.id);
					uITianJieSkillSlot2.SetSlotData(tianJieSkill);
					UIDuJieZhunBei.Inst.SaveTiaoZheng();
					result = true;
				}
			}
			else if (ToSlot is SubmitSlot)
			{
				SubmitSlot submitSlot = (SubmitSlot)DragSlot;
				SubmitSlot submitSlot2 = (SubmitSlot)ToSlot;
				if (submitSlot.IsInBag && !submitSlot2.IsInBag)
				{
					SubmitUIMag.Inst.PutItem(submitSlot, submitSlot2);
				}
				else if (!submitSlot.IsInBag && submitSlot2.IsInBag)
				{
					SubmitUIMag.Inst.BackItem(submitSlot, submitSlot2);
				}
			}
		}
		Clear();
		return result;
	}

	public void UpdatePostion(Vector3 vector3)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		tempSlot.UpdatePostion(NewUICanvas.Inst.Camera.ScreenToWorldPoint(vector3));
	}
}
