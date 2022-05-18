using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TuPo
{
	// Token: 0x02000A85 RID: 2693
	public class TuPoUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x0600452C RID: 17708 RVA: 0x001D9808 File Offset: 0x001D7A08
		public void ShowTuPo(int level, int oldHp, int curHp, int oldShenShi, int curShenShi, int oldShouYuan, int curShouYuan, int oldDunSu, int curDunSu, bool isBigTuPo, string desc)
		{
			if (BindData.ContainsKey("FightBeforeHpMax"))
			{
				oldHp = BindData.Get<int>("FightBeforeHpMax");
			}
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = new Vector3(0f, 81f, 0f);
			base.transform.localScale = new Vector3(0.821f, 0.821f, 0.821f);
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			this.LevelImage.sprite = this.SpritesList[level - 1];
			this.OldHP.text = oldHp.ToString();
			this.CurHP.text = curHp.ToString();
			this.OldShenShi.text = oldShenShi.ToString();
			this.CurShenShi.text = curShenShi.ToString();
			this.OldShouYuan.text = oldShouYuan.ToString();
			this.CurShouYuan.text = curShouYuan.ToString();
			this.OldDunSu.text = oldDunSu.ToString();
			this.CurDunSu.text = curDunSu.ToString();
			this.Desc.text = desc;
			base.gameObject.SetActive(true);
			this.Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.Panel, Vector3.one, 0.5f);
			ESCCloseManager.Inst.RegisterClose(this);
			if (level == 13)
			{
				this.loadTalk4031OnClose = true;
			}
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000317CD File Offset: 0x0002F9CD
		public void Close()
		{
			if (this.loadTalk4031OnClose)
			{
				Resources.Load<GameObject>("talkPrefab/TalkPrefab/Talk4031").Inst(null);
			}
			Tools.canClickFlag = true;
			Object.Destroy(base.gameObject);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x00031804 File Offset: 0x0002FA04
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003D5D RID: 15709
		[SerializeField]
		private List<Sprite> SpritesList;

		// Token: 0x04003D5E RID: 15710
		[SerializeField]
		private Image LevelImage;

		// Token: 0x04003D5F RID: 15711
		[SerializeField]
		private Text OldHP;

		// Token: 0x04003D60 RID: 15712
		[SerializeField]
		private Text CurHP;

		// Token: 0x04003D61 RID: 15713
		[SerializeField]
		private Text OldShenShi;

		// Token: 0x04003D62 RID: 15714
		[SerializeField]
		private Text CurShenShi;

		// Token: 0x04003D63 RID: 15715
		[SerializeField]
		private Text OldShouYuan;

		// Token: 0x04003D64 RID: 15716
		[SerializeField]
		private Text CurShouYuan;

		// Token: 0x04003D65 RID: 15717
		[SerializeField]
		private Text OldDunSu;

		// Token: 0x04003D66 RID: 15718
		[SerializeField]
		private Text CurDunSu;

		// Token: 0x04003D67 RID: 15719
		[SerializeField]
		private Text Desc;

		// Token: 0x04003D68 RID: 15720
		[SerializeField]
		private Transform Panel;

		// Token: 0x04003D69 RID: 15721
		private bool loadTalk4031OnClose;
	}
}
