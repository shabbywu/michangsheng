using System;
using UnityEngine;
using UnityEngine.Events;

namespace script.Steam.Utils
{
	// Token: 0x020009DE RID: 2526
	public class UIToggleA : UIBase
	{
		// Token: 0x06004602 RID: 17922 RVA: 0x001DA21C File Offset: 0x001D841C
		public UIToggleA(GameObject gameObject, UIToggleGroup uiToggleGroup, UnityAction selectAction, UnityAction unSelectAction)
		{
			UIToggleA <>4__this = this;
			this._go = gameObject;
			this.已选中 = base.Get("已选中", true);
			this.未选中 = base.Get<FpBtn>("未选中");
			this.select = selectAction;
			this.unSelect = unSelectAction;
			this.未选中.mouseUpEvent.AddListener(delegate()
			{
				uiToggleGroup.Select(<>4__this);
			});
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x001DA298 File Offset: 0x001D8498
		public void SetCanClick(bool flag)
		{
			this.未选中.SetCanClick(flag);
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x001DA2A6 File Offset: 0x001D84A6
		public void Select()
		{
			this.已选中.SetActive(true);
			this.未选中.gameObject.SetActive(false);
			UnityAction unityAction = this.select;
			if (unityAction == null)
			{
				return;
			}
			unityAction.Invoke();
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x001DA2D5 File Offset: 0x001D84D5
		public void CanCelSelect()
		{
			this.已选中.SetActive(false);
			this.未选中.gameObject.SetActive(true);
			UnityAction unityAction = this.unSelect;
			if (unityAction == null)
			{
				return;
			}
			unityAction.Invoke();
		}

		// Token: 0x04004794 RID: 18324
		private GameObject 已选中;

		// Token: 0x04004795 RID: 18325
		private FpBtn 未选中;

		// Token: 0x04004796 RID: 18326
		private readonly UnityAction select;

		// Token: 0x04004797 RID: 18327
		private readonly UnityAction unSelect;
	}
}
