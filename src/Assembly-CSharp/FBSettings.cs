using System;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class FBSettings : ScriptableObject
{
	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0000FFF9 File Offset: 0x0000E1F9
	private static FBSettings Instance
	{
		get
		{
			if (FBSettings.instance == null)
			{
				FBSettings.instance = (Resources.Load("FacebookSettings") as FBSettings);
				if (FBSettings.instance == null)
				{
					FBSettings.instance = ScriptableObject.CreateInstance<FBSettings>();
				}
			}
			return FBSettings.instance;
		}
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x00010038 File Offset: 0x0000E238
	public void SetAppIndex(int index)
	{
		if (this.selectedAppIndex != index)
		{
			this.selectedAppIndex = index;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0001004F File Offset: 0x0000E24F
	public int SelectedAppIndex
	{
		get
		{
			return this.selectedAppIndex;
		}
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x00010057 File Offset: 0x0000E257
	public void SetAppId(int index, string value)
	{
		if (this.appIds[index] != value)
		{
			this.appIds[index] = value;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00010077 File Offset: 0x0000E277
	// (set) Token: 0x06000FFD RID: 4093 RVA: 0x0001007F File Offset: 0x0000E27F
	public string[] AppIds
	{
		get
		{
			return this.appIds;
		}
		set
		{
			if (this.appIds != value)
			{
				this.appIds = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x00010096 File Offset: 0x0000E296
	public void SetAppLabel(int index, string value)
	{
		if (this.appLabels[index] != value)
		{
			this.AppLabels[index] = value;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000FFF RID: 4095 RVA: 0x000100B6 File Offset: 0x0000E2B6
	// (set) Token: 0x06001000 RID: 4096 RVA: 0x000100BE File Offset: 0x0000E2BE
	public string[] AppLabels
	{
		get
		{
			return this.appLabels;
		}
		set
		{
			if (this.appLabels != value)
			{
				this.appLabels = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06001001 RID: 4097 RVA: 0x000100D5 File Offset: 0x0000E2D5
	public static string AppId
	{
		get
		{
			return FBSettings.Instance.AppIds[FBSettings.Instance.SelectedAppIndex];
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06001002 RID: 4098 RVA: 0x000100EC File Offset: 0x0000E2EC
	public static bool IsValidAppId
	{
		get
		{
			return FBSettings.AppId != null && FBSettings.AppId.Length > 0 && !FBSettings.AppId.Equals("0");
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06001003 RID: 4099 RVA: 0x00010116 File Offset: 0x0000E316
	// (set) Token: 0x06001004 RID: 4100 RVA: 0x00010122 File Offset: 0x0000E322
	public static bool Cookie
	{
		get
		{
			return FBSettings.Instance.cookie;
		}
		set
		{
			if (FBSettings.Instance.cookie != value)
			{
				FBSettings.Instance.cookie = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06001005 RID: 4101 RVA: 0x00010141 File Offset: 0x0000E341
	// (set) Token: 0x06001006 RID: 4102 RVA: 0x0001014D File Offset: 0x0000E34D
	public static bool Logging
	{
		get
		{
			return FBSettings.Instance.logging;
		}
		set
		{
			if (FBSettings.Instance.logging != value)
			{
				FBSettings.Instance.logging = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06001007 RID: 4103 RVA: 0x0001016C File Offset: 0x0000E36C
	// (set) Token: 0x06001008 RID: 4104 RVA: 0x00010178 File Offset: 0x0000E378
	public static bool Status
	{
		get
		{
			return FBSettings.Instance.status;
		}
		set
		{
			if (FBSettings.Instance.status != value)
			{
				FBSettings.Instance.status = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06001009 RID: 4105 RVA: 0x00010197 File Offset: 0x0000E397
	// (set) Token: 0x0600100A RID: 4106 RVA: 0x000101A3 File Offset: 0x0000E3A3
	public static bool Xfbml
	{
		get
		{
			return FBSettings.Instance.xfbml;
		}
		set
		{
			if (FBSettings.Instance.xfbml != value)
			{
				FBSettings.Instance.xfbml = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x0600100B RID: 4107 RVA: 0x000101C2 File Offset: 0x0000E3C2
	// (set) Token: 0x0600100C RID: 4108 RVA: 0x000101CE File Offset: 0x0000E3CE
	public static string IosURLSuffix
	{
		get
		{
			return FBSettings.Instance.iosURLSuffix;
		}
		set
		{
			if (FBSettings.Instance.iosURLSuffix != value)
			{
				FBSettings.Instance.iosURLSuffix = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x0600100D RID: 4109 RVA: 0x000101F2 File Offset: 0x0000E3F2
	public static string ChannelUrl
	{
		get
		{
			return "/channel.html";
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x0600100E RID: 4110 RVA: 0x000101F9 File Offset: 0x0000E3F9
	// (set) Token: 0x0600100F RID: 4111 RVA: 0x00010205 File Offset: 0x0000E405
	public static bool FrictionlessRequests
	{
		get
		{
			return FBSettings.Instance.frictionlessRequests;
		}
		set
		{
			if (FBSettings.Instance.frictionlessRequests != value)
			{
				FBSettings.Instance.frictionlessRequests = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x000042DD File Offset: 0x000024DD
	private static void DirtyEditor()
	{
	}

	// Token: 0x04000C74 RID: 3188
	private const string facebookSettingsAssetName = "FacebookSettings";

	// Token: 0x04000C75 RID: 3189
	private const string facebookSettingsPath = "Facebook/Resources";

	// Token: 0x04000C76 RID: 3190
	private const string facebookSettingsAssetExtension = ".asset";

	// Token: 0x04000C77 RID: 3191
	private static FBSettings instance;

	// Token: 0x04000C78 RID: 3192
	[SerializeField]
	private int selectedAppIndex;

	// Token: 0x04000C79 RID: 3193
	[SerializeField]
	private string[] appIds = new string[]
	{
		"0"
	};

	// Token: 0x04000C7A RID: 3194
	[SerializeField]
	private string[] appLabels = new string[]
	{
		"App Name"
	};

	// Token: 0x04000C7B RID: 3195
	[SerializeField]
	private bool cookie = true;

	// Token: 0x04000C7C RID: 3196
	[SerializeField]
	private bool logging = true;

	// Token: 0x04000C7D RID: 3197
	[SerializeField]
	private bool status = true;

	// Token: 0x04000C7E RID: 3198
	[SerializeField]
	private bool xfbml;

	// Token: 0x04000C7F RID: 3199
	[SerializeField]
	private bool frictionlessRequests = true;

	// Token: 0x04000C80 RID: 3200
	[SerializeField]
	private string iosURLSuffix = "";
}
