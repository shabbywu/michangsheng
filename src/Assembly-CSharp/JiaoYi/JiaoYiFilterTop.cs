using System;
using System.Collections;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JiaoYi
{
	// Token: 0x02000A86 RID: 2694
	public class JiaoYiFilterTop : MonoBehaviour, IFilterTop
	{
		// Token: 0x06004530 RID: 17712 RVA: 0x0003180D File Offset: 0x0002FA0D
		public void Clear()
		{
			Tools.ClearChild(this.Select.transform);
			base.gameObject.SetActive(false);
			this.Select.SetActive(false);
			this.Unselect.gameObject.SetActive(true);
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x001D99A4 File Offset: 0x001D7BA4
		public void Init(object data, FilterType type, string title)
		{
			this.BaseBag2 = (BaseBag2)data;
			this.FilterType = type;
			this.Unselect.mouseUpEvent.RemoveAllListeners();
			this.Unselect.mouseUpEvent.AddListener(new UnityAction(this.ClickEvent));
			if (title == "全部")
			{
				if (type == FilterType.类型 && this.BaseBag2.ItemType == Bag.ItemType.材料)
				{
					this.CurType.SetText("阴阳");
				}
				else
				{
					this.CurType.SetText(type.ToString());
				}
			}
			else
			{
				this.CurType.SetText(title);
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00031848 File Offset: 0x0002FA48
		public void ClickEvent()
		{
			this.BaseBag2.CloseAllTopFilter();
			this.CreateChild();
			this.Select.SetActive(true);
			this.Unselect.gameObject.SetActive(false);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x001D9A54 File Offset: 0x001D7C54
		public void CreateChild()
		{
			Tools.ClearChild(this.Select.transform);
			if (this.FilterType == FilterType.品阶)
			{
				this.CreateQuality();
				return;
			}
			if (this.FilterType == FilterType.类型)
			{
				if (this.BaseBag2.ItemType == Bag.ItemType.法宝)
				{
					this.CreateEquipType();
					return;
				}
				if (this.BaseBag2.ItemType == Bag.ItemType.秘籍)
				{
					this.CreateSkillType();
					return;
				}
				if (this.BaseBag2.ItemType == Bag.ItemType.材料)
				{
					this.CreateCaiLiaoYinYang();
					return;
				}
			}
			else if (this.FilterType == FilterType.属性)
			{
				if (this.BaseBag2.ItemType == Bag.ItemType.秘籍)
				{
					if (this.BaseBag2.JiaoYiSkillType == JiaoYiSkillType.神通)
					{
						this.CreateActiveSkillType();
						return;
					}
					if (this.BaseBag2.JiaoYiSkillType == JiaoYiSkillType.功法)
					{
						this.CreatePasstiveSkillType();
						return;
					}
				}
				else if (this.BaseBag2.ItemType == Bag.ItemType.材料)
				{
					this.CreateCaiLiaoType();
				}
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x001D9B20 File Offset: 0x001D7D20
		public void CreateQuality()
		{
			Array values = Enum.GetValues(typeof(ItemQuality));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.ItemQuality.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ItemQuality itemQuality = (ItemQuality)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(itemQuality.ToString(), delegate
					{
						this.BaseBag2.ItemQuality = itemQuality;
						this.CurType.SetText((this.BaseBag2.ItemQuality == ItemQuality.全部) ? "品质" : itemQuality.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x001D9C60 File Offset: 0x001D7E60
		public void CreateEquipType()
		{
			Array values = Enum.GetValues(typeof(EquipType));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.EquipType.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EquipType equipType = (EquipType)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(equipType.ToString(), delegate
					{
						this.BaseBag2.EquipType = equipType;
						this.CurType.SetText((this.BaseBag2.EquipType == EquipType.全部) ? "类型" : equipType.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x001D9DA0 File Offset: 0x001D7FA0
		public void CreateSkillType()
		{
			Array values = Enum.GetValues(typeof(JiaoYiSkillType));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.JiaoYiSkillType.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JiaoYiSkillType skillType = (JiaoYiSkillType)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(skillType.ToString(), delegate
					{
						this.BaseBag2.JiaoYiSkillType = skillType;
						if (skillType == JiaoYiSkillType.功法 || skillType == JiaoYiSkillType.神通)
						{
							this.BaseBag2.OpenShuXing();
						}
						else
						{
							this.BaseBag2.CloseShuXing();
						}
						this.CurType.SetText((skillType == JiaoYiSkillType.全部) ? "类型" : skillType.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x001D9EE0 File Offset: 0x001D80E0
		public void CreateCaiLiaoYinYang()
		{
			Array values = Enum.GetValues(typeof(LianQiCaiLiaoYinYang));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.LianQiCaiLiaoYinYang.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LianQiCaiLiaoYinYang liaoYinYang = (LianQiCaiLiaoYinYang)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(liaoYinYang.ToString(), delegate
					{
						this.BaseBag2.LianQiCaiLiaoYinYang = liaoYinYang;
						this.CurType.SetText((liaoYinYang == LianQiCaiLiaoYinYang.全部) ? "阴阳" : liaoYinYang.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x001DA020 File Offset: 0x001D8220
		public void CreateActiveSkillType()
		{
			Array values = Enum.GetValues(typeof(SkIllType));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.SkIllType.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SkIllType skillType = (SkIllType)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(skillType.ToString(), delegate
					{
						this.BaseBag2.SkIllType = skillType;
						this.CurType.SetText((skillType == SkIllType.全部) ? "属性" : skillType.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x001DA160 File Offset: 0x001D8360
		public void CreatePasstiveSkillType()
		{
			Array values = Enum.GetValues(typeof(StaticSkIllType));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.StaticSkIllType.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StaticSkIllType skillType = (StaticSkIllType)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(skillType.ToString(), delegate
					{
						this.BaseBag2.StaticSkIllType = skillType;
						this.CurType.SetText((skillType == StaticSkIllType.全部) ? "属性" : skillType.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x001DA2A0 File Offset: 0x001D84A0
		public void CreateCaiLiaoType()
		{
			Array values = Enum.GetValues(typeof(LianQiCaiLiaoType));
			int num = values.Length - 1;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.LianQiCaiLiaoType.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LianQiCaiLiaoType caiLiaoType = (LianQiCaiLiaoType)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(caiLiaoType.ToString(), delegate
					{
						this.BaseBag2.LianQiCaiLiaoType = caiLiaoType;
						this.CurType.SetText((caiLiaoType == LianQiCaiLiaoType.全部) ? "属性" : caiLiaoType.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
					num2++;
				}
			}
		}

		// Token: 0x04003D6A RID: 15722
		public FilterType FilterType;

		// Token: 0x04003D6B RID: 15723
		public Text CurType;

		// Token: 0x04003D6C RID: 15724
		public GameObject Select;

		// Token: 0x04003D6D RID: 15725
		public FpBtn Unselect;

		// Token: 0x04003D6E RID: 15726
		public BaseFilterTopChild ChildUp;

		// Token: 0x04003D6F RID: 15727
		public BaseFilterTopChild ChildCenter;

		// Token: 0x04003D70 RID: 15728
		public BaseFilterTopChild ChildDown;

		// Token: 0x04003D71 RID: 15729
		public BaseBag2 BaseBag2;
	}
}
