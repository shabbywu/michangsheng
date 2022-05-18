using System;
using Bag;
using JiaoYi;
using PaiMai;
using script.Submit;
using Sirenix.Utilities;
using Tab;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AB RID: 683
public class DragMag
{
	// Token: 0x17000268 RID: 616
	// (get) Token: 0x060014CA RID: 5322 RVA: 0x00013143 File Offset: 0x00011343
	public static DragMag Inst
	{
		get
		{
			if (DragMag._inst == null)
			{
				DragMag._inst = new DragMag();
			}
			return DragMag._inst;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x060014CB RID: 5323 RVA: 0x0001315B File Offset: 0x0001135B
	private DragMag.TempSlot tempSlot
	{
		get
		{
			if (this._tempSlot == null)
			{
				this._tempSlot = DragMag.TempSlot.Create(NewUICanvas.Inst.transform);
			}
			return this._tempSlot;
		}
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x00013180 File Offset: 0x00011380
	public void Clear()
	{
		this.DragSlot = null;
		this.ToSlot = null;
		this.IsDraging = false;
		this.tempSlot.Hide();
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000131A2 File Offset: 0x000113A2
	public void StartDrag(SlotBase slot)
	{
		this.DragSlot = slot;
		this.IsDraging = true;
		this.tempSlot.Show(slot.GetIcon());
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x000BBDD8 File Offset: 0x000B9FD8
	public bool EndDrag()
	{
		bool result = false;
		if (this.ToSlot != null && this.ToSlot != this.DragSlot && this.ToSlot.Group == this.DragSlot.Group)
		{
			if (this.ToSlot is EquipSlot)
			{
				if (this.DragSlot.Item != null && this.DragSlot.Item.CanPutSlotType == this.ToSlot.AcceptType)
				{
					if (this.DragSlot is EquipSlot && Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
					{
						EquipSlot equipSlot = (EquipSlot)this.DragSlot;
						EquipSlot equipSlot2 = (EquipSlot)this.ToSlot;
						SingletonMono<TabUIMag>.Instance.WuPingPanel.ExEquip(equipSlot.EquipSlotType, equipSlot2.EquipSlotType);
					}
					else
					{
						EquipSlot equipSlot3 = (EquipSlot)this.ToSlot;
						EquipItem equipItem = (EquipItem)this.DragSlot.Item;
						SingletonMono<TabUIMag>.Instance.WuPingPanel.AddEquip((int)equipSlot3.EquipSlotType, (EquipItem)equipItem.Clone());
					}
					result = true;
				}
			}
			else if (this.ToSlot is PasstiveSkillSlot)
			{
				if (this.DragSlot.Skill != null && this.DragSlot.Skill.CanPutSlotType == this.ToSlot.AcceptType)
				{
					if (this.DragSlot is PasstiveSkillSlot)
					{
						PasstiveSkillSlot passtiveSkillSlot = (PasstiveSkillSlot)this.DragSlot;
						PasstiveSkillSlot passtiveSkillSlot2 = (PasstiveSkillSlot)this.ToSlot;
						SingletonMono<TabUIMag>.Instance.GongFaPanel.ExSkill(passtiveSkillSlot.SkillSlotType, passtiveSkillSlot2.SkillSlotType);
						result = true;
					}
					else
					{
						PasstiveSkillSlot passtiveSkillSlot3 = (PasstiveSkillSlot)this.ToSlot;
						SingletonMono<TabUIMag>.Instance.GongFaPanel.AddSkill(passtiveSkillSlot3.SkillSlotType, this.DragSlot.Skill.Clone(), SingletonMono<TabUIMag>.Instance.GongFaPanel.GetSameSkillIndex(this.DragSlot.Skill));
						result = true;
					}
				}
			}
			else if (this.ToSlot is ActiveSkillSlot)
			{
				if (this.DragSlot.Skill != null && this.DragSlot.Skill.CanPutSlotType == this.ToSlot.AcceptType)
				{
					if (this.DragSlot is ActiveSkillSlot)
					{
						ActiveSkillSlot activeSkillSlot = (ActiveSkillSlot)this.DragSlot;
						ActiveSkillSlot activeSkillSlot2 = (ActiveSkillSlot)this.ToSlot;
						SingletonMono<TabUIMag>.Instance.ShenTongPanel.ExSkill(activeSkillSlot.index, activeSkillSlot2.index);
						result = true;
					}
					else if (SingletonMono<TabUIMag>.Instance.ShenTongPanel.CanAddSkill(this.DragSlot.Skill))
					{
						ActiveSkillSlot activeSkillSlot3 = (ActiveSkillSlot)this.ToSlot;
						SingletonMono<TabUIMag>.Instance.ShenTongPanel.AddSkill(activeSkillSlot3.index, this.DragSlot.Skill.Clone());
						result = true;
					}
				}
			}
			else if (this.ToSlot is JiaoYiSlot)
			{
				JiaoYiSlot jiaoYiSlot = (JiaoYiSlot)this.DragSlot;
				JiaoYiSlot jiaoYiSlot2 = (JiaoYiSlot)this.ToSlot;
				if (jiaoYiSlot.IsInBag && !jiaoYiSlot2.IsInBag && jiaoYiSlot.IsPlayer == jiaoYiSlot2.IsPlayer)
				{
					JiaoYiUIMag.Inst.SellItem(jiaoYiSlot, jiaoYiSlot2);
				}
				else if (!jiaoYiSlot.IsInBag && jiaoYiSlot2.IsInBag && jiaoYiSlot.IsPlayer == jiaoYiSlot2.IsPlayer)
				{
					JiaoYiUIMag.Inst.BackItem(jiaoYiSlot, jiaoYiSlot2);
				}
			}
			else if (this.ToSlot is PaiMaiSlot)
			{
				PaiMaiSlot paiMaiSlot = (PaiMaiSlot)this.DragSlot;
				PaiMaiSlot paiMaiSlot2 = (PaiMaiSlot)this.ToSlot;
				if (paiMaiSlot.IsInBag && !paiMaiSlot2.IsInBag && paiMaiSlot.IsPlayer == paiMaiSlot2.IsPlayer)
				{
					NewPaiMaiJoin.Inst.PutItem(paiMaiSlot, paiMaiSlot2);
				}
				else if (!paiMaiSlot.IsInBag && paiMaiSlot2.IsInBag && paiMaiSlot.IsPlayer == paiMaiSlot2.IsPlayer)
				{
					NewPaiMaiJoin.Inst.BackItem(paiMaiSlot, paiMaiSlot2);
				}
			}
			else if (this.ToSlot is UITianJieSkillSlot)
			{
				UITianJieSkillSlot uitianJieSkillSlot = (UITianJieSkillSlot)this.DragSlot;
				UITianJieSkillSlot uitianJieSkillSlot2 = (UITianJieSkillSlot)this.ToSlot;
				if (uitianJieSkillSlot2.IsEquipSlot)
				{
					BagTianJieSkill tianJieSkill = uitianJieSkillSlot.TianJieSkill;
					UIDuJieZhunBei.Inst.ClearTiaoZhengSlotByID(tianJieSkill.MiShu.id);
					uitianJieSkillSlot2.SetSlotData(tianJieSkill);
					UIDuJieZhunBei.Inst.SaveTiaoZheng();
					result = true;
				}
			}
			else if (this.ToSlot is SubmitSlot)
			{
				SubmitSlot submitSlot = (SubmitSlot)this.DragSlot;
				SubmitSlot submitSlot2 = (SubmitSlot)this.ToSlot;
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
		this.Clear();
		return result;
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000131C3 File Offset: 0x000113C3
	public void UpdatePostion(Vector3 vector3)
	{
		this.tempSlot.UpdatePostion(NewUICanvas.Inst.Camera.ScreenToWorldPoint(vector3));
	}

	// Token: 0x04000FFE RID: 4094
	private static DragMag _inst;

	// Token: 0x04000FFF RID: 4095
	public SlotBase DragSlot;

	// Token: 0x04001000 RID: 4096
	public SlotBase ToSlot;

	// Token: 0x04001001 RID: 4097
	public bool IsDraging;

	// Token: 0x04001002 RID: 4098
	private DragMag.TempSlot _tempSlot;

	// Token: 0x020002AC RID: 684
	private class TempSlot
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x000BC2E0 File Offset: 0x000BA4E0
		public static DragMag.TempSlot Create(Transform parent)
		{
			DragMag.TempSlot tempSlot = new DragMag.TempSlot();
			tempSlot.Image = new GameObject("TempImag").AddComponent<Image>();
			tempSlot.Image.raycastTarget = false;
			tempSlot.gameObject = tempSlot.Image.gameObject;
			tempSlot.transform = tempSlot.gameObject.transform;
			tempSlot.rect = tempSlot.gameObject.GetComponent<RectTransform>();
			tempSlot.transform.parent = parent;
			tempSlot.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			return tempSlot;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000BC374 File Offset: 0x000BA574
		public void Show(Sprite sprite)
		{
			this.gameObject.SetActive(true);
			this.transform.SetAsLastSibling();
			this.Image.sprite = sprite;
			RectExtensions.SetWidth(this.rect.rect, 128f);
			RectExtensions.SetWidth(this.rect.rect, 128f);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000BC3D0 File Offset: 0x000BA5D0
		public void UpdatePostion(Vector3 vector3)
		{
			this.transform.position = vector3;
			this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000131E0 File Offset: 0x000113E0
		public void Hide()
		{
			this.gameObject.SetActive(false);
		}

		// Token: 0x04001003 RID: 4099
		private Image Image;

		// Token: 0x04001004 RID: 4100
		private Transform transform;

		// Token: 0x04001005 RID: 4101
		private RectTransform rect;

		// Token: 0x04001006 RID: 4102
		private GameObject gameObject;
	}
}
