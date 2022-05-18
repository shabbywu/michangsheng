using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DC6 RID: 3526
	public class UITianJiDaBi2Player : MonoBehaviour
	{
		// Token: 0x060054F0 RID: 21744 RVA: 0x00236384 File Offset: 0x00234584
		public void InitData(DaBiPlayer left, DaBiPlayer right, Match match)
		{
			this.LeftName.text = left.Name;
			this.RightName.text = right.Name;
			this.LeftFire.NumberText.text = left.BigScore.ToString();
			this.RightFire.NumberText.text = right.BigScore.ToString();
			this.LeftWinFail.text = "";
			this.RightWinFail.text = "";
			this.LeftWinFail.gameObject.SetActive(false);
			this.RightWinFail.gameObject.SetActive(false);
			this.LeftFire.SetNormal();
			this.RightFire.SetNormal();
			this.JianAnimCtl.Play("UITianJiDaBiJianIdleAnim");
			if (left.IsWanJia)
			{
				this.LeftName.color = new Color(0.078431375f, 0.39607844f, 0.3529412f);
			}
			if (right.IsWanJia)
			{
				this.RightName.color = new Color(0.078431375f, 0.39607844f, 0.3529412f);
			}
			this.LeftNameBtn.mouseEnterEvent.RemoveAllListeners();
			this.RightNameBtn.mouseEnterEvent.RemoveAllListeners();
			this.LeftNameBtn.mouseOutEvent.RemoveAllListeners();
			this.RightNameBtn.mouseOutEvent.RemoveAllListeners();
			this.LeftNameBtn.mouseUpEvent.RemoveAllListeners();
			this.RightNameBtn.mouseUpEvent.RemoveAllListeners();
			this.LeftNameBtn.mouseEnterEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					this.LeftNameHighlight.SetActive(true);
				}
			});
			this.RightNameBtn.mouseEnterEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					this.RightNameHiglight.SetActive(true);
				}
			});
			this.LeftNameBtn.mouseOutEvent.AddListener(delegate()
			{
				this.LeftNameHighlight.SetActive(false);
			});
			this.RightNameBtn.mouseOutEvent.AddListener(delegate()
			{
				this.RightNameHiglight.SetActive(false);
			});
			this.LeftNameBtn.mouseUpEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					UITianJiDaBiPlayerInfo.Show(left, match);
				}
			});
			this.RightNameBtn.mouseUpEvent.AddListener(delegate()
			{
				if (this.canClickName)
				{
					UITianJiDaBiPlayerInfo.Show(right, match);
				}
			});
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x002365E8 File Offset: 0x002347E8
		public void SetWinFail(DaBiPlayer left, DaBiPlayer right)
		{
			FightRecord fightRecord = left.FightRecords[left.FightRecords.Count - 1];
			FightRecord fightRecord2 = right.FightRecords[right.FightRecords.Count - 1];
			this.SetLeftWinFail(fightRecord.WinID == left.ID, true);
			this.SetRightWinFail(fightRecord2.WinID == right.ID, true);
			this.JianAnimCtl.Play("UITianJiDaBiJianEndAnim");
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00236664 File Offset: 0x00234864
		public void SetLeftWinFail(bool win, bool showText = true)
		{
			if (win)
			{
				this.LeftWinFail.text = "胜";
				this.LeftWinFail.color = this.winColor;
				this.LeftFire.SetWin();
			}
			else
			{
				this.LeftWinFail.text = "负";
				this.LeftWinFail.color = this.failColor;
				this.LeftFire.SetFail();
			}
			this.LeftWinFail.gameObject.SetActive(showText);
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x002366E0 File Offset: 0x002348E0
		public void SetRightWinFail(bool win, bool showText = true)
		{
			if (win)
			{
				this.RightWinFail.text = "胜";
				this.RightWinFail.color = this.winColor;
				this.RightFire.SetWin();
			}
			else
			{
				this.RightWinFail.text = "负";
				this.RightWinFail.color = this.failColor;
				this.RightFire.SetFail();
			}
			this.RightWinFail.gameObject.SetActive(showText);
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0003CB2A File Offset: 0x0003AD2A
		public void PlayFightAnim(Action onAnimEnd)
		{
			base.StartCoroutine(this.FightAnimC(onAnimEnd));
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0003CB3A File Offset: 0x0003AD3A
		private IEnumerator FightAnimC(Action onAnimEnd)
		{
			this.canClickName = false;
			float num = Random.Range(0f, 1f);
			yield return new WaitForSeconds(num);
			this.JianAnimCtl.Play("UITianJiDaBiJianFightAnim");
			this.LeftFire.FireAnim.Play("UITianJiDaBiFireFightAnim");
			this.RightFire.FireAnim.Play("UITianJiDaBiFireFightAnim");
			yield return new WaitForSeconds(2f);
			if (onAnimEnd != null)
			{
				onAnimEnd();
			}
			this.canClickName = true;
			yield break;
		}

		// Token: 0x040054A3 RID: 21667
		public UITianJiDaBiFire LeftFire;

		// Token: 0x040054A4 RID: 21668
		public UITianJiDaBiFire RightFire;

		// Token: 0x040054A5 RID: 21669
		public GameObject LeftNameHighlight;

		// Token: 0x040054A6 RID: 21670
		public GameObject RightNameHiglight;

		// Token: 0x040054A7 RID: 21671
		public FpBtn LeftNameBtn;

		// Token: 0x040054A8 RID: 21672
		public FpBtn RightNameBtn;

		// Token: 0x040054A9 RID: 21673
		public Text LeftName;

		// Token: 0x040054AA RID: 21674
		public Text RightName;

		// Token: 0x040054AB RID: 21675
		public Text LeftWinFail;

		// Token: 0x040054AC RID: 21676
		public Text RightWinFail;

		// Token: 0x040054AD RID: 21677
		public Animator JianAnimCtl;

		// Token: 0x040054AE RID: 21678
		private Color winColor = new Color(0.73333335f, 0.38431373f, 0.19607843f);

		// Token: 0x040054AF RID: 21679
		private Color failColor = new Color(0.30588236f, 0.39607844f, 0.49803922f);

		// Token: 0x040054B0 RID: 21680
		private bool canClickName = true;
	}
}
