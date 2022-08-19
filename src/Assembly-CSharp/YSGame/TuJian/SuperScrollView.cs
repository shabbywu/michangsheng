using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000A99 RID: 2713
	public class SuperScrollView : MonoBehaviour
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x00205F6F File Offset: 0x0020416F
		// (set) Token: 0x06004BF6 RID: 19446 RVA: 0x00205F77 File Offset: 0x00204177
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

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06004BF7 RID: 19447 RVA: 0x00205F90 File Offset: 0x00204190
		// (set) Token: 0x06004BF8 RID: 19448 RVA: 0x00205FEC File Offset: 0x002041EC
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

		// Token: 0x06004BF9 RID: 19449 RVA: 0x00206030 File Offset: 0x00204230
		private void Start()
		{
			this.Init();
			this.SetScroller();
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x00206040 File Offset: 0x00204240
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

		// Token: 0x06004BFB RID: 19451 RVA: 0x002060E5 File Offset: 0x002042E5
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

		// Token: 0x06004BFC RID: 19452 RVA: 0x00206111 File Offset: 0x00204311
		private void ResetToTop()
		{
			this.NowSelectItemIndex = 0;
			this.firstIndex = 0;
			this.lastIndex = 0;
			this.mContentRect.anchoredPosition = new Vector2(this.mContentRect.anchoredPosition.x, 0f);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0020614D File Offset: 0x0020434D
		public void ResetToTopByHyperlink()
		{
			this.firstIndex = 0;
			this.lastIndex = 0;
			this.mContentRect.anchoredPosition = new Vector2(this.mContentRect.anchoredPosition.x, 0f);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x00206182 File Offset: 0x00204382
		private void SetScroller()
		{
			this.SetContentHeight();
			this.InitCountent();
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x00206190 File Offset: 0x00204390
		public void SetContentHeight()
		{
			this.mContentRect.sizeDelta = new Vector2(this.mContentRect.sizeDelta.x, this.itemHeight * (float)this.DataList.Count);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x002061C8 File Offset: 0x002043C8
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

		// Token: 0x06004C01 RID: 19457 RVA: 0x0020634C File Offset: 0x0020454C
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

		// Token: 0x04004B14 RID: 19220
		private ScrollRect mScrollRect;

		// Token: 0x04004B15 RID: 19221
		private RectTransform mContentRect;

		// Token: 0x04004B16 RID: 19222
		public GameObject itemPrefab;

		// Token: 0x04004B17 RID: 19223
		public float itemHeight;

		// Token: 0x04004B18 RID: 19224
		private int maxItemCount = 20;

		// Token: 0x04004B19 RID: 19225
		private List<Dictionary<int, string>> _DataList = new List<Dictionary<int, string>>();

		// Token: 0x04004B1A RID: 19226
		private int firstIndex;

		// Token: 0x04004B1B RID: 19227
		private int lastIndex;

		// Token: 0x04004B1C RID: 19228
		private List<SSVItem> itemList;

		// Token: 0x04004B1D RID: 19229
		private SSVPool pool = new SSVPool();

		// Token: 0x04004B1E RID: 19230
		[HideInInspector]
		public int NowSelectItemIndex;

		// Token: 0x04004B1F RID: 19231
		[HideInInspector]
		public bool NeedResetToTop;

		// Token: 0x04004B20 RID: 19232
		[HideInInspector]
		public bool NeedSetScroller;
	}
}
