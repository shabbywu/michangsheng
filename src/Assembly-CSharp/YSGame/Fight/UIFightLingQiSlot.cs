using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000E01 RID: 3585
	public class UIFightLingQiSlot : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
	{
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x0003DD82 File Offset: 0x0003BF82
		// (set) Token: 0x0600568A RID: 22154 RVA: 0x0003DD8A File Offset: 0x0003BF8A
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

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600568B RID: 22155 RVA: 0x0003DDB0 File Offset: 0x0003BFB0
		// (set) Token: 0x0600568C RID: 22156 RVA: 0x00240DE8 File Offset: 0x0023EFE8
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

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600568D RID: 22157 RVA: 0x00240E58 File Offset: 0x0023F058
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

		// Token: 0x0600568E RID: 22158 RVA: 0x00240EA8 File Offset: 0x0023F0A8
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

		// Token: 0x0600568F RID: 22159 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
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

		// Token: 0x06005690 RID: 22160 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void PlayAddLingQiAnim(int count)
		{
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void PlayRemoveLingQiAnim(int count)
		{
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x0003DDED File Offset: 0x0003BFED
		// (set) Token: 0x06005693 RID: 22163 RVA: 0x002410C8 File Offset: 0x0023F2C8
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

		// Token: 0x06005694 RID: 22164 RVA: 0x0003DDF5 File Offset: 0x0003BFF5
		public bool CanInteractive()
		{
			return this.LingQiType != LingQiType.Count && !this.IsLock;
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0003DE0B File Offset: 0x0003C00B
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(3);
			}
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x0003DE1C File Offset: 0x0003C01C
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(0);
			}
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x00241130 File Offset: 0x0023F330
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

		// Token: 0x06005698 RID: 22168 RVA: 0x0003DE2D File Offset: 0x0003C02D
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.CanInteractive())
			{
				this.SetLingQiSprite(2);
			}
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void OnClick()
		{
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void OnLeftClick()
		{
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void OnRightClick()
		{
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x0003DE3E File Offset: 0x0003C03E
		public virtual void SetNull()
		{
			this.LingQiType = LingQiType.Count;
			this.lingQiCount = 0;
			this.IsLock = false;
			this.LingQiCountText.text = "";
			this.SetLingQiCountShow(false);
			this.HighlightObj.SetActive(false);
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x00241178 File Offset: 0x0023F378
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

		// Token: 0x0600569E RID: 22174 RVA: 0x0003DE78 File Offset: 0x0003C078
		public void PlayLingQiSound()
		{
			UIFightPanel.Inst.NeedPlayLingQiSound = true;
		}

		// Token: 0x04005617 RID: 22039
		public static bool IgnoreEffect;

		// Token: 0x04005618 RID: 22040
		public GameObject LingQiCountObj;

		// Token: 0x04005619 RID: 22041
		public Text LingQiCountText;

		// Token: 0x0400561A RID: 22042
		public Image LingQiImage;

		// Token: 0x0400561B RID: 22043
		public GameObject HighlightObj;

		// Token: 0x0400561C RID: 22044
		private bool nowShowCount = true;

		// Token: 0x0400561D RID: 22045
		[SerializeField]
		private LingQiType lingQiType = LingQiType.Count;

		// Token: 0x0400561E RID: 22046
		protected float lingQiTweenTime = 1f;

		// Token: 0x0400561F RID: 22047
		private int lingQiCount;

		// Token: 0x04005620 RID: 22048
		private bool isLock;
	}
}
