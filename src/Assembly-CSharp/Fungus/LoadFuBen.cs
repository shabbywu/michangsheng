using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000F8C RID: 3980
	[CommandInfo("YSTools", "LoadFuBen", "加载副本", 0)]
	[AddComponentMenu("")]
	public class LoadFuBen : Command
	{
		// Token: 0x06006F62 RID: 28514 RVA: 0x002A6CE8 File Offset: 0x002A4EE8
		public override void OnEnter()
		{
			LoadFuBen.loadfuben(this._sceneName, this.FirstPositon.Value);
			this.Continue();
		}

		// Token: 0x06006F63 RID: 28515 RVA: 0x002A6D0C File Offset: 0x002A4F0C
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

		// Token: 0x06006F64 RID: 28516 RVA: 0x002A6D44 File Offset: 0x002A4F44
		public static void loadfuben(string fubenName, int positon)
		{
			new Thread(new ThreadStart(LoadFuBen.methodName)).Start();
			Tools.instance.getPlayer().fubenContorl[fubenName].setFirstIndex(positon);
			Tools.instance.getPlayer().zulinContorl.kezhanLastScence = Tools.getScreenName();
			Tools.instance.loadMapScenes(fubenName, true);
			Tools.instance.getPlayer().lastFuBenScence = Tools.getScreenName();
			Tools.instance.getPlayer().NowFuBen = fubenName;
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x002A6DCB File Offset: 0x002A4FCB
		public override string GetSummary()
		{
			if (this._sceneName.Value.Length == 0)
			{
				return "Error: No scene name selected";
			}
			return this._sceneName.Value;
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006F67 RID: 28519 RVA: 0x002A6DF0 File Offset: 0x002A4FF0
		public override bool HasReference(Variable variable)
		{
			return this._sceneName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006F68 RID: 28520 RVA: 0x002A6E0E File Offset: 0x002A500E
		protected virtual void OnEnable()
		{
			if (this.sceneNameOLD != "")
			{
				this._sceneName.Value = this.sceneNameOLD;
				this.sceneNameOLD = "";
			}
		}

		// Token: 0x04005C09 RID: 23561
		[Tooltip("副本的场景名称")]
		[SerializeField]
		protected StringData _sceneName = new StringData("");

		// Token: 0x04005C0A RID: 23562
		[Tooltip("首次进入时角色被传送到的位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable FirstPositon;

		// Token: 0x04005C0B RID: 23563
		[HideInInspector]
		[FormerlySerializedAs("sceneName")]
		public string sceneNameOLD = "";
	}
}
