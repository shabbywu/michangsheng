using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000DD3 RID: 3539
	public class SuperScrollView : MonoBehaviour
	{
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06005535 RID: 21813 RVA: 0x0003CE4C File Offset: 0x0003B04C
		// (set) Token: 0x06005536 RID: 21814 RVA: 0x0003CE54 File Offset: 0x0003B054
		[HideInInspector]
		public List<Dictionary<int, string>> DataList
		{
			get
			{
				return this._DataList;
			}
			set
			{
				this._DataList = value;
				this.NeedResetToTop = true;
				this.NeedSetScroller = true;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x00237674 File Offset: 0x00235874
		// (set) Token: 0x06005538 RID: 21816 RVA: 0x002376D0 File Offset: 0x002358D0
		public int NowSelectID
		{
			get
			{
				if (this.DataList == null || this.DataList.Count == 0)
				{
					return -1;
				}
				if (this.NowSelectItemIndex >= this.DataList.Count)
				{
					this.NowSelectItemIndex = 0;
				}
				return this.DataList[this.NowSelectItemIndex].Keys.First<int>();
			}
			set
			{
				for (int i = 0; i < this.DataList.Count; i++)
				{
					if (this.DataList[i].Keys.First<int>() == value)
					{
						this.NowSelectItemIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0003CE6B File Offset: 0x0003B06B
		private void Start()
		{
			this.Init();
			this.SetScroller();
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00237714 File Offset: 0x00235914
		public void Init()
		{
			this.itemList = new List<SSVItem>();
			this.mScrollRect = base.transform.GetComponent<ScrollRect>();
			this.mContentRect = this.mScrollRect.content.transform.GetComponent<RectTransform>();
			this.mScrollRect.onValueChanged.AddListener(delegate(Vector2 vec)
			{
				this.OnScrollMove(vec);
			});
			for (int i = 0; i < this.maxItemCount; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemPrefab, this.mContentRect.transform);
				gameObject.GetComponent<SSVItem>().SSV = this;
				this.pool.Recovery(gameObject.GetComponent<SSVItem>());
			}
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0003CE79 File Offset: 0x0003B079
		private void Update()
		{
			if (this.NeedResetToTop)
			{
				this.ResetToTop();
				this.NeedResetToTop = false;
			}
			if (this.NeedSetScroller)
			{
				this.SetScroller();
				this.NeedSetScroller = false;
			}
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0003CEA5 File Offset: 0x0003B0A5
		private void ResetToTop()
		{
			this.NowSelectItemIndex = 0;
			this.firstIndex = 0;
			this.lastIndex = 0;
			this.mContentRect.anchoredPosition = new Vector2(this.mContentRect.anchoredPosition.x, 0f);
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0003CEE1 File Offset: 0x0003B0E1
		public void ResetToTopByHyperlink()
		{
			this.firstIndex = 0;
			this.lastIndex = 0;
			this.mContentRect.anchoredPosition = new Vector2(this.mContentRect.anchoredPosition.x, 0f);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0003CF16 File Offset: 0x0003B116
		private void SetScroller()
		{
			this.SetContentHeight();
			this.InitCountent();
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0003CF24 File Offset: 0x0003B124
		public void SetContentHeight()
		{
			this.mContentRect.sizeDelta = new Vector2(this.mContentRect.sizeDelta.x, this.itemHeight * (float)this.DataList.Count);
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x002377BC File Offset: 0x002359BC
		public void InitCountent()
		{
			int num = Mathf.Clamp(this.DataList.Count, 0, this.maxItemCount);
			if (num < this.itemList.Count)
			{
				int num2 = this.itemList.Count - num;
				for (int i = 0; i < num2; i++)
				{
					this.pool.Recovery(this.itemList[0]);
					this.itemList.RemoveAt(0);
				}
			}
			else if (num > this.itemList.Count)
			{
				for (int j = this.itemList.Count; j < num; j++)
				{
					this.itemList.Add(this.pool.Get());
				}
			}
			for (int k = 0; k < this.itemList.Count; k++)
			{
				this.itemList[k].name = k.ToString();
				this.itemList[k].DataList = this.DataList;
				this.itemList[k].DataIndex = k;
				RectTransform component = this.itemList[k].GetComponent<RectTransform>();
				component.pivot = new Vector2(0.5f, 1f);
				component.anchorMax = new Vector2(0.5f, 1f);
				component.anchorMin = new Vector2(0.5f, 1f);
				component.anchoredPosition = new Vector2(0f, -this.itemHeight * (float)k);
				this.lastIndex = k;
			}
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x00237940 File Offset: 0x00235B40
		private void OnScrollMove(Vector2 pVec)
		{
			if (this.DataList == null || this.DataList.Count == 0)
			{
				return;
			}
			while (this.mContentRect.anchoredPosition.y > (float)(this.firstIndex + 1) * this.itemHeight)
			{
				if (this.lastIndex == this.DataList.Count - 1)
				{
					break;
				}
				SSVItem ssvitem = this.itemList[0];
				RectTransform component = ssvitem.GetComponent<RectTransform>();
				this.itemList.RemoveAt(0);
				this.itemList.Add(ssvitem);
				component.anchoredPosition = new Vector2(0f, (float)(-(float)(this.lastIndex + 1)) * this.itemHeight);
				this.firstIndex++;
				this.lastIndex++;
				ssvitem.name = this.lastIndex.ToString();
				ssvitem.DataIndex = this.lastIndex;
			}
			while (this.mContentRect.anchoredPosition.y < (float)this.firstIndex * this.itemHeight && this.firstIndex != 0)
			{
				SSVItem ssvitem2 = this.itemList[this.itemList.Count - 1];
				RectTransform component2 = ssvitem2.GetComponent<RectTransform>();
				this.itemList.RemoveAt(this.itemList.Count - 1);
				this.itemList.Insert(0, ssvitem2);
				component2.anchoredPosition = new Vector2(0f, (float)(-(float)(this.firstIndex - 1)) * this.itemHeight);
				this.firstIndex--;
				this.lastIndex--;
				ssvitem2.name = this.firstIndex.ToString();
				ssvitem2.DataIndex = this.firstIndex;
			}
		}

		// Token: 0x040054EC RID: 21740
		private ScrollRect mScrollRect;

		// Token: 0x040054ED RID: 21741
		private RectTransform mContentRect;

		// Token: 0x040054EE RID: 21742
		public GameObject itemPrefab;

		// Token: 0x040054EF RID: 21743
		public float itemHeight;

		// Token: 0x040054F0 RID: 21744
		private int maxItemCount = 20;

		// Token: 0x040054F1 RID: 21745
		private List<Dictionary<int, string>> _DataList = new List<Dictionary<int, string>>();

		// Token: 0x040054F2 RID: 21746
		private int firstIndex;

		// Token: 0x040054F3 RID: 21747
		private int lastIndex;

		// Token: 0x040054F4 RID: 21748
		private List<SSVItem> itemList;

		// Token: 0x040054F5 RID: 21749
		private SSVPool pool = new SSVPool();

		// Token: 0x040054F6 RID: 21750
		[HideInInspector]
		public int NowSelectItemIndex;

		// Token: 0x040054F7 RID: 21751
		[HideInInspector]
		public bool NeedResetToTop;

		// Token: 0x040054F8 RID: 21752
		[HideInInspector]
		public bool NeedSetScroller;
	}
}
