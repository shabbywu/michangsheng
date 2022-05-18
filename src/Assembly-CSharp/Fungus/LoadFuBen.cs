using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001440 RID: 5184
	[CommandInfo("YSTools", "LoadFuBen", "加载副本", 0)]
	[AddComponentMenu("")]
	public class LoadFuBen : Command
	{
		// Token: 0x06007D4C RID: 32076 RVA: 0x00054B9C File Offset: 0x00052D9C
		public override void OnEnter()
		{
			LoadFuBen.loadfuben(this._sceneName, this.FirstPositon.Value);
			this.Continue();
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x002C6568 File Offset: 0x002C4768
		public static void methodName()
		{
			try
			{
				Tools.instance.getPlayer().ResetAllEndlessNode();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x002C65A0 File Offset: 0x002C47A0
		public static void loadfuben(string fubenName, int positon)
		{
			new Thread(new ThreadStart(LoadFuBen.methodName)).Start();
			Tools.instance.getPlayer().fubenContorl[fubenName].setFirstIndex(positon);
			Tools.instance.getPlayer().zulinContorl.kezhanLastScence = Tools.getScreenName();
			Tools.instance.loadMapScenes(fubenName, true);
			Tools.instance.getPlayer().lastFuBenScence = Tools.getScreenName();
			Tools.instance.getPlayer().NowFuBen = fubenName;
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x00054BBF File Offset: 0x00052DBF
		public override string GetSummary()
		{
			if (this._sceneName.Value.Length == 0)
			{
				return "Error: No scene name selected";
			}
			return this._sceneName.Value;
		}

		// Token: 0x06007D50 RID: 32080 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x00054BE4 File Offset: 0x00052DE4
		public override bool HasReference(Variable variable)
		{
			return this._sceneName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x00054C02 File Offset: 0x00052E02
		protected virtual void OnEnable()
		{
			if (this.sceneNameOLD != "")
			{
				this._sceneName.Value = this.sceneNameOLD;
				this.sceneNameOLD = "";
			}
		}

		// Token: 0x04006AD8 RID: 27352
		[Tooltip("副本的场景名称")]
		[SerializeField]
		protected StringData _sceneName = new StringData("");

		// Token: 0x04006AD9 RID: 27353
		[Tooltip("首次进入时角色被传送到的位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable FirstPositon;

		// Token: 0x04006ADA RID: 27354
		[HideInInspector]
		[FormerlySerializedAs("sceneName")]
		public string sceneNameOLD = "";
	}
}
