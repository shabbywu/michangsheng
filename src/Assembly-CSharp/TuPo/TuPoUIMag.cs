using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TuPo
{
	// Token: 0x0200072C RID: 1836
	public class TuPoUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x06003A80 RID: 14976 RVA: 0x0019200C File Offset: 0x0019020C
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

		// Token: 0x06003A81 RID: 14977 RVA: 0x001921A8 File Offset: 0x001903A8
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

		// Token: 0x06003A82 RID: 14978 RVA: 0x001921DF File Offset: 0x001903DF
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x040032BA RID: 12986
		[SerializeField]
		private List<Sprite> SpritesList;

		// Token: 0x040032BB RID: 12987
		[SerializeField]
		private Image LevelImage;

		// Token: 0x040032BC RID: 12988
		[SerializeField]
		private Text OldHP;

		// Token: 0x040032BD RID: 12989
		[SerializeField]
		private Text CurHP;

		// Token: 0x040032BE RID: 12990
		[SerializeField]
		private Text OldShenShi;

		// Token: 0x040032BF RID: 12991
		[SerializeField]
		private Text CurShenShi;

		// Token: 0x040032C0 RID: 12992
		[SerializeField]
		private Text OldShouYuan;

		// Token: 0x040032C1 RID: 12993
		[SerializeField]
		private Text CurShouYuan;

		// Token: 0x040032C2 RID: 12994
		[SerializeField]
		private Text OldDunSu;

		// Token: 0x040032C3 RID: 12995
		[SerializeField]
		private Text CurDunSu;

		// Token: 0x040032C4 RID: 12996
		[SerializeField]
		private Text Desc;

		// Token: 0x040032C5 RID: 12997
		[SerializeField]
		private Transform Panel;

		// Token: 0x040032C6 RID: 12998
		private bool loadTalk4031OnClose;
	}
}
