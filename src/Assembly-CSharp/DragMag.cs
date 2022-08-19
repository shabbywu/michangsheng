using System;
using Bag;
using JiaoYi;
using PaiMai;
using script.Submit;
using Sirenix.Utilities;
using Tab;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AE RID: 430
public class DragMag
{
	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06001224 RID: 4644 RVA: 0x0006E10D File Offset: 0x0006C30D
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

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06001225 RID: 4645 RVA: 0x0006E125 File Offset: 0x0006C325
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

	// Token: 0x06001226 RID: 4646 RVA: 0x0006E14A File Offset: 0x0006C34A
	public void Clear()
	{
		this.DragSlot = null;
		this.ToSlot = null;
		this.IsDraging = false;
		this.tempSlot.Hide();
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0006E16C File Offset: 0x0006C36C
	public void StartDrag(SlotBase slot)
	{
		this.DragSlot = slot;
		this.IsDraging = true;
		this.tempSlot.Show(slot.GetIcon());
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x0006E190 File Offset: 0x0006C390
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

	// Token: 0x06001229 RID: 4649 RVA: 0x0006E695 File Offset: 0x0006C895
	public void UpdatePostion(Vector3 vector3)
	{
		this.tempSlot.UpdatePostion(NewUICanvas.Inst.Camera.ScreenToWorldPoint(vector3));
	}

	// Token: 0x04000CD7 RID: 3287
	private static DragMag _inst;

	// Token: 0x04000CD8 RID: 3288
	public SlotBase DragSlot;

	// Token: 0x04000CD9 RID: 3289
	public SlotBase ToSlot;

	// Token: 0x04000CDA RID: 3290
	public bool IsDraging;

	// Token: 0x04000CDB RID: 3291
	private DragMag.TempSlot _tempSlot;

	// Token: 0x020012BD RID: 4797
	private class TempSlot
	{
		// Token: 0x06007A64 RID: 31332 RVA: 0x002BCCF0 File Offset: 0x002BAEF0
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

		// Token: 0x06007A65 RID: 31333 RVA: 0x002BCD84 File Offset: 0x002BAF84
		public void Show(Sprite sprite)
		{
			this.gameObject.SetActive(true);
			this.transform.SetAsLastSibling();
			this.Image.sprite = sprite;
			RectExtensions.SetWidth(this.rect.rect, 128f);
			RectExtensions.SetWidth(this.rect.rect, 128f);
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x002BCDE0 File Offset: 0x002BAFE0
		public void UpdatePostion(Vector3 vector3)
		{
			this.transform.position = vector3;
			this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x002BCE2E File Offset: 0x002BB02E
		public void Hide()
		{
			this.gameObject.SetActive(false);
		}

		// Token: 0x04006684 RID: 26244
		private Image Image;

		// Token: 0x04006685 RID: 26245
		private Transform transform;

		// Token: 0x04006686 RID: 26246
		private RectTransform rect;

		// Token: 0x04006687 RID: 26247
		private GameObject gameObject;
	}
}
