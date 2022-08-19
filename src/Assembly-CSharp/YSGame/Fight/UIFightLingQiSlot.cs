using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000AC3 RID: 2755
	public class UIFightLingQiSlot : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x00210841 File Offset: 0x0020EA41
		// (set) Token: 0x06004D3B RID: 19771 RVA: 0x00210849 File Offset: 0x0020EA49
		public LingQiType LingQiType
		{
			get
			{
				return this.lingQiType;
			}
			set
			{
				this.lingQiType = value;
				if (this.lingQiType != LingQiType.Count)
				{
					this.SetLingQiSprite(0);
					return;
				}
				this.LingQiImage.sprite = null;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x0021086F File Offset: 0x0020EA6F
		// (set) Token: 0x06004D3D RID: 19773 RVA: 0x00210878 File Offset: 0x0020EA78
		public int LingQiCount
		{
			get
			{
				return this.lingQiCount;
			}
			set
			{
				if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
				{
					return;
				}
				int num = value;
				if (num < 0)
				{
					if (this is UIFightLingQiPlayerSlot)
					{
						Debug.LogError(string.Format("{0}被设置为{1}", this.LingQiType, num));
					}
					num = 0;
				}
				int change = num - this.lingQiCount;
				this.lingQiCount = num;
				this.OnLingQiCountChanged(change);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x002108E8 File Offset: 0x0020EAE8
		public int SpriteIndex
		{
			get
			{
				if (this.LingQiType == LingQiType.Count)
				{
					return -1;
				}
				if (this.LingQiCount > 0 && this.LingQiCount <= 10)
				{
					return 0;
				}
				if (this.LingQiCount > 10 && this.LingQiCount <= 30)
				{
					return 1;
				}
				if (this.LingQiCount > 30)
				{
					return 2;
				}
				return -1;
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00210938 File Offset: 0x0020EB38
		public void SetLingQiSprite(int state)
		{
			int spriteIndex = this.SpriteIndex;
			if (spriteIndex < 0)
			{
				this.LingQiImage.sprite = null;
				this.LingQiImage.color = new Color(1f, 1f, 1f, 0f);
				return;
			}
			this.LingQiImage.color = Color.white;
			Sprite sprite = null;
			if (spriteIndex == 0)
			{
				if (state == 0)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Normal;
				}
				else if (state == 1)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Lock;
				}
				else if (state == 2)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Press;
				}
				else if (state == 3)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Highlight;
				}
			}
			else if (spriteIndex == 1)
			{
				if (state == 0)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Normal2;
				}
				else if (state == 1)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Lock2;
				}
				else if (state == 2)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Press2;
				}
				else if (state == 3)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Highlight2;
				}
			}
			else if (spriteIndex == 2)
			{
				if (state == 0)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Normal3;
				}
				else if (state == 1)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Lock3;
				}
				else if (state == 2)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Press3;
				}
				else if (state == 3)
				{
					sprite = UIFightPanel.Inst.LingQiImageDatas[(int)this.LingQiType].Highlight3;
				}
			}
			this.LingQiImage.sprite = sprite;
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00210B58 File Offset: 0x0020ED58
		protected virtual void OnLingQiCountChanged(int change)
		{
			if (change != 0)
			{
				if (change > 0)
				{
					this.PlayAddLingQiAnim(change);
				}
				else
				{
					this.PlayRemoveLingQiAnim(Mathf.Abs(change));
				}
			}
			if (this.IsLock)
			{
				this.SetLingQiSprite(1);
				return;
			}
			this.SetLingQiSprite(0);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void PlayAddLingQiAnim(int count)
		{
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void PlayRemoveLingQiAnim(int count)
		{
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x00210B8D File Offset: 0x0020ED8D
		// (set) Token: 0x06004D44 RID: 19780 RVA: 0x00210B98 File Offset: 0x0020ED98
		public bool IsLock
		{
			get
			{
				return this.isLock;
			}
			set
			{
				this.isLock = value;
				if (this.LingQiType == LingQiType.Count)
				{
					this.LingQiImage.sprite = null;
					this.LingQiImage.color = new Color(1f, 1f, 1f, 0f);
					return;
				}
				if (this.isLock)
				{
					this.SetLingQiSprite(1);
					return;
				}
				this.SetLingQiSprite(0);
			}
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x00210BFD File Offset: 0x0020EDFD
		public bool CanInteractive()
		{
			return this.LingQiType != LingQiType.Count && !this.IsLock;
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00210C13 File Offset: 0x0020EE13
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(3);
			}
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00210C24 File Offset: 0x0020EE24
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(0);
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x00210C38 File Offset: 0x0020EE38
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(0);
				bool mouseButtonUp = Input.GetMouseButtonUp(0);
				bool mouseButtonUp2 = Input.GetMouseButtonUp(1);
				if (mouseButtonUp || mouseButtonUp2)
				{
					this.OnClick();
				}
				if (mouseButtonUp)
				{
					this.OnLeftClick();
				}
				if (mouseButtonUp2)
				{
					this.OnRightClick();
				}
			}
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x00210C7D File Offset: 0x0020EE7D
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(2);
			}
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void OnClick()
		{
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void OnLeftClick()
		{
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void OnRightClick()
		{
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00210C8E File Offset: 0x0020EE8E
		public virtual void SetNull()
		{
			this.LingQiType = LingQiType.Count;
			this.lingQiCount = 0;
			this.IsLock = false;
			this.LingQiCountText.text = "";
			this.SetLingQiCountShow(false);
			this.HighlightObj.SetActive(false);
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00210CC8 File Offset: 0x0020EEC8
		public void SetLingQiCountShow(bool show)
		{
			if (this.nowShowCount != show)
			{
				this.nowShowCount = show;
				if (this.nowShowCount)
				{
					this.LingQiCountObj.transform.localScale = Vector3.zero;
					ShortcutExtensions.DOScale(this.LingQiCountObj.transform, Vector3.one, 0.1f);
					return;
				}
				this.LingQiCountObj.transform.localScale = Vector3.one;
				ShortcutExtensions.DOScale(this.LingQiCountObj.transform, Vector3.zero, 0.1f);
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x00210D4E File Offset: 0x0020EF4E
		public void PlayLingQiSound()
		{
			UIFightPanel.Inst.NeedPlayLingQiSound = true;
		}

		// Token: 0x04004C3D RID: 19517
		public static bool IgnoreEffect;

		// Token: 0x04004C3E RID: 19518
		public GameObject LingQiCountObj;

		// Token: 0x04004C3F RID: 19519
		public Text LingQiCountText;

		// Token: 0x04004C40 RID: 19520
		public Image LingQiImage;

		// Token: 0x04004C41 RID: 19521
		public GameObject HighlightObj;

		// Token: 0x04004C42 RID: 19522
		private bool nowShowCount = true;

		// Token: 0x04004C43 RID: 19523
		[SerializeField]
		private LingQiType lingQiType = LingQiType.Count;

		// Token: 0x04004C44 RID: 19524
		protected float lingQiTweenTime = 1f;

		// Token: 0x04004C45 RID: 19525
		private int lingQiCount;

		// Token: 0x04004C46 RID: 19526
		private bool isLock;
	}
}
