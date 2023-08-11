using System;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JiaoYi;

public class JiaoYiFilterTop : MonoBehaviour, IFilterTop
{
	public FilterType FilterType;

	public Text CurType;

	public GameObject Select;

	public FpBtn Unselect;

	public BaseFilterTopChild ChildUp;

	public BaseFilterTopChild ChildCenter;

	public BaseFilterTopChild ChildDown;

	public BaseBag2 BaseBag2;

	public void Clear()
	{
		Tools.ClearChild(Select.transform);
		((Component)this).gameObject.SetActive(false);
		Select.SetActive(false);
		((Component)Unselect).gameObject.SetActive(true);
	}

	public void Init(object data, FilterType type, string title)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		BaseBag2 = (BaseBag2)data;
		FilterType = type;
		((UnityEventBase)Unselect.mouseUpEvent).RemoveAllListeners();
		Unselect.mouseUpEvent.AddListener(new UnityAction(ClickEvent));
		if (title == "全部")
		{
			if (type == FilterType.类型 && BaseBag2.ItemType == Bag.ItemType.材料)
			{
				CurType.SetText("阴阳");
			}
			else
			{
				CurType.SetText(type.ToString());
			}
		}
		else
		{
			CurType.SetText(title);
		}
		((Component)this).gameObject.SetActive(true);
	}

	public void ClickEvent()
	{
		BaseBag2.CloseAllTopFilter();
		CreateChild();
		Select.SetActive(true);
		((Component)Unselect).gameObject.SetActive(false);
	}

	public void CreateChild()
	{
		Tools.ClearChild(Select.transform);
		if (FilterType == FilterType.品阶)
		{
			CreateQuality();
		}
		else if (FilterType == FilterType.类型)
		{
			if (BaseBag2.ItemType == Bag.ItemType.法宝)
			{
				CreateEquipType();
			}
			else if (BaseBag2.ItemType == Bag.ItemType.秘籍)
			{
				CreateSkillType();
			}
			else if (BaseBag2.ItemType == Bag.ItemType.材料)
			{
				CreateCaiLiaoYinYang();
			}
		}
		else
		{
			if (FilterType != FilterType.属性)
			{
				return;
			}
			if (BaseBag2.ItemType == Bag.ItemType.秘籍)
			{
				if (BaseBag2.JiaoYiSkillType == JiaoYiSkillType.神通)
				{
					CreateActiveSkillType();
				}
				else if (BaseBag2.JiaoYiSkillType == JiaoYiSkillType.功法)
				{
					CreatePasstiveSkillType();
				}
			}
			else if (BaseBag2.ItemType == Bag.ItemType.材料)
			{
				CreateCaiLiaoType();
			}
		}
	}

	public virtual void CreateQuality()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(ItemQuality));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.ItemQuality.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (ItemQuality itemQuality in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(itemQuality.ToString(), (UnityAction)delegate
			{
				BaseBag2.ItemQuality = itemQuality;
				CurType.SetText((BaseBag2.ItemQuality == ItemQuality.全部) ? "品质" : itemQuality.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}

	public void CreateEquipType()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(EquipType));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.EquipType.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (EquipType equipType in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(equipType.ToString(), (UnityAction)delegate
			{
				BaseBag2.EquipType = equipType;
				CurType.SetText((BaseBag2.EquipType == EquipType.全部) ? "类型" : equipType.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}

	public void CreateSkillType()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(JiaoYiSkillType));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.JiaoYiSkillType.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (JiaoYiSkillType skillType in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(skillType.ToString(), (UnityAction)delegate
			{
				BaseBag2.JiaoYiSkillType = skillType;
				if (skillType == JiaoYiSkillType.功法 || skillType == JiaoYiSkillType.神通)
				{
					BaseBag2.OpenShuXing();
				}
				else
				{
					BaseBag2.CloseShuXing();
				}
				CurType.SetText((skillType == JiaoYiSkillType.全部) ? "类型" : skillType.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}

	public void CreateCaiLiaoYinYang()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(LianQiCaiLiaoYinYang));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.LianQiCaiLiaoYinYang.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (LianQiCaiLiaoYinYang liaoYinYang in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(liaoYinYang.ToString(), (UnityAction)delegate
			{
				BaseBag2.LianQiCaiLiaoYinYang = liaoYinYang;
				CurType.SetText((liaoYinYang == LianQiCaiLiaoYinYang.全部) ? "阴阳" : liaoYinYang.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}

	public void CreateActiveSkillType()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(SkIllType));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.SkIllType.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (SkIllType skillType in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(skillType.ToString(), (UnityAction)delegate
			{
				BaseBag2.SkIllType = skillType;
				CurType.SetText((skillType == SkIllType.全部) ? "属性" : skillType.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}

	public void CreatePasstiveSkillType()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(StaticSkIllType));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.StaticSkIllType.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (StaticSkIllType skillType in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(skillType.ToString(), (UnityAction)delegate
			{
				BaseBag2.StaticSkIllType = skillType;
				CurType.SetText((skillType == StaticSkIllType.全部) ? "属性" : skillType.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}

	public void CreateCaiLiaoType()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(LianQiCaiLiaoType));
		int num = values.Length - 1;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.LianQiCaiLiaoType.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (LianQiCaiLiaoType caiLiaoType in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(caiLiaoType.ToString(), (UnityAction)delegate
			{
				BaseBag2.LianQiCaiLiaoType = caiLiaoType;
				CurType.SetText((caiLiaoType == LianQiCaiLiaoType.全部) ? "属性" : caiLiaoType.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			num2++;
		}
	}
}
