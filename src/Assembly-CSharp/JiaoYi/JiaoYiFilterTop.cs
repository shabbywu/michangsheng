using System;
using System.Collections;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JiaoYi
{
	// Token: 0x0200072D RID: 1837
	public class JiaoYiFilterTop : MonoBehaviour, IFilterTop
	{
		// Token: 0x06003A84 RID: 14980 RVA: 0x001921E8 File Offset: 0x001903E8
		public void Clear()
		{
			Tools.ClearChild(this.Select.transform);
			base.gameObject.SetActive(false);
			this.Select.SetActive(false);
			this.Unselect.gameObject.SetActive(true);
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x00192224 File Offset: 0x00190424
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

		// Token: 0x06003A86 RID: 14982 RVA: 0x001922D4 File Offset: 0x001904D4
		public void ClickEvent()
		{
			this.BaseBag2.CloseAllTopFilter();
			this.CreateChild();
			this.Select.SetActive(true);
			this.Unselect.gameObject.SetActive(false);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x00192304 File Offset: 0x00190504
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

		// Token: 0x06003A88 RID: 14984 RVA: 0x001923D0 File Offset: 0x001905D0
		public virtual void CreateQuality()
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

		// Token: 0x06003A89 RID: 14985 RVA: 0x00192510 File Offset: 0x00190710
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

		// Token: 0x06003A8A RID: 14986 RVA: 0x00192650 File Offset: 0x00190850
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

		// Token: 0x06003A8B RID: 14987 RVA: 0x00192790 File Offset: 0x00190990
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

		// Token: 0x06003A8C RID: 14988 RVA: 0x001928D0 File Offset: 0x00190AD0
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

		// Token: 0x06003A8D RID: 14989 RVA: 0x00192A10 File Offset: 0x00190C10
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

		// Token: 0x06003A8E RID: 14990 RVA: 0x00192B50 File Offset: 0x00190D50
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

		// Token: 0x040032C7 RID: 12999
		public FilterType FilterType;

		// Token: 0x040032C8 RID: 13000
		public Text CurType;

		// Token: 0x040032C9 RID: 13001
		public GameObject Select;

		// Token: 0x040032CA RID: 13002
		public FpBtn Unselect;

		// Token: 0x040032CB RID: 13003
		public BaseFilterTopChild ChildUp;

		// Token: 0x040032CC RID: 13004
		public BaseFilterTopChild ChildCenter;

		// Token: 0x040032CD RID: 13005
		public BaseFilterTopChild ChildDown;

		// Token: 0x040032CE RID: 13006
		public BaseBag2 BaseBag2;
	}
}
