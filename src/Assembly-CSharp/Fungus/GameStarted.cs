using System;
using System.Collections;
using KBEngine;
using script.YarnEditor.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000E9D RID: 3741
	[EventHandlerInfo("", "Game Started", "The block will execute when the game starts playing.")]
	[AddComponentMenu("")]
	public class GameStarted : EventHandler
	{
		// Token: 0x06006A06 RID: 27142 RVA: 0x0029277C File Offset: 0x0029097C
		protected virtual void Start()
		{
			if (StoryManager.Inst.CheckTrigger(SceneManager.GetActiveScene().name))
			{
				StoryManager.Inst.OldTalk = new UnityAction(this.CheckLater);
				return;
			}
			this.CheckLater();
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x002927C0 File Offset: 0x002909C0
		public void CheckLater()
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.StreamData.FungusSaveMgr.IsNeedStop)
			{
				return;
			}
			if (!StoryManager.Inst.IsEnd)
			{
				return;
			}
			if (Tools.instance.getPlayer().StreamData.FungusSaveMgr.SaveFungusData != null)
			{
				FungusSaveMgr fungusSaveMgr = Tools.instance.getPlayer().StreamData.FungusSaveMgr;
				if (fungusSaveMgr.StopTalkName == this.parentBlock.GetFlowchart().GetParentName())
				{
					fungusSaveMgr.StopTalkName = "";
					return;
				}
			}
			player.StreamData.FungusSaveMgr.LastIsEnd = player.StreamData.FungusSaveMgr.IsEnd();
			player.StreamData.FungusSaveMgr.CurCommand = null;
			base.StartCoroutine(this.GameStartCoroutine());
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0029288F File Offset: 0x00290A8F
		protected virtual IEnumerator GameStartCoroutine()
		{
			int num;
			for (int frameCount = this.waitForFrames; frameCount > 0; frameCount = num - 1)
			{
				yield return new WaitForEndOfFrame();
				num = frameCount;
			}
			this.ExecuteBlock();
			yield break;
		}

		// Token: 0x040059D1 RID: 22993
		[Tooltip("Wait for a number of frames after startup before executing the Block. Can help fix startup order issues.")]
		[SerializeField]
		protected int waitForFrames = 1;
	}
}
