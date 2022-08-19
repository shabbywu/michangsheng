using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C61 RID: 3169
	public class ServerErrorDescrs
	{
		// Token: 0x0600561F RID: 22047 RVA: 0x0023C238 File Offset: 0x0023A438
		public ServerErrorDescrs()
		{
			ServerErr serverErr;
			serverErr.id = 0;
			serverErr.name = "SUCCESS";
			serverErr.descr = "成功。";
			ServerErrorDescrs.serverErrs.Add(serverErr.id, serverErr);
			ServerErr serverErr2;
			serverErr2.id = 1;
			serverErr2.name = "SERVER_ERR_SRV_NO_READY";
			serverErr2.descr = "服务器没有准备好。";
			ServerErrorDescrs.serverErrs.Add(serverErr2.id, serverErr2);
			ServerErr serverErr3;
			serverErr3.id = 2;
			serverErr3.name = "SERVER_ERR_SRV_OVERLOAD";
			serverErr3.descr = "服务器负载过重。";
			ServerErrorDescrs.serverErrs.Add(serverErr3.id, serverErr3);
			ServerErr serverErr4;
			serverErr4.id = 3;
			serverErr4.name = "SERVER_ERR_ILLEGAL_LOGIN";
			serverErr4.descr = "非法登录。";
			ServerErrorDescrs.serverErrs.Add(serverErr4.id, serverErr4);
			ServerErr serverErr5;
			serverErr5.id = 4;
			serverErr5.name = "SERVER_ERR_NAME_PASSWORD";
			serverErr5.descr = "用户名或者密码不正确。";
			ServerErrorDescrs.serverErrs.Add(serverErr5.id, serverErr5);
			ServerErr serverErr6;
			serverErr6.id = 5;
			serverErr6.name = "SERVER_ERR_NAME";
			serverErr6.descr = "用户名不正确。";
			ServerErrorDescrs.serverErrs.Add(serverErr6.id, serverErr6);
			ServerErr serverErr7;
			serverErr7.id = 6;
			serverErr7.name = "SERVER_ERR_PASSWORD";
			serverErr7.descr = "密码不正确。";
			ServerErrorDescrs.serverErrs.Add(serverErr7.id, serverErr7);
			ServerErr serverErr8;
			serverErr8.id = 7;
			serverErr8.name = "SERVER_ERR_ACCOUNT_CREATE_FAILED";
			serverErr8.descr = "创建账号失败（已经存在一个相同的账号）。";
			ServerErrorDescrs.serverErrs.Add(serverErr8.id, serverErr8);
			ServerErr serverErr9;
			serverErr9.id = 8;
			serverErr9.name = "SERVER_ERR_BUSY";
			serverErr9.descr = "操作过于繁忙(例如：在服务器前一次请求未执行完毕的情况下连续N次创建账号)。";
			ServerErrorDescrs.serverErrs.Add(serverErr9.id, serverErr9);
			ServerErr serverErr10;
			serverErr10.id = 9;
			serverErr10.name = "SERVER_ERR_ACCOUNT_LOGIN_ANOTHER";
			serverErr10.descr = "当前账号在另一处登录了。";
			ServerErrorDescrs.serverErrs.Add(serverErr10.id, serverErr10);
			ServerErr serverErr11;
			serverErr11.id = 10;
			serverErr11.name = "SERVER_ERR_ACCOUNT_IS_ONLINE";
			serverErr11.descr = "账号已登陆。";
			ServerErrorDescrs.serverErrs.Add(serverErr11.id, serverErr11);
			ServerErr serverErr12;
			serverErr12.id = 11;
			serverErr12.name = "SERVER_ERR_PROXY_DESTROYED";
			serverErr12.descr = "与客户端关联的proxy在服务器上已经销毁。";
			ServerErrorDescrs.serverErrs.Add(serverErr12.id, serverErr12);
			ServerErr serverErr13;
			serverErr13.id = 12;
			serverErr13.name = "SERVER_ERR_ENTITYDEFS_NOT_MATCH";
			serverErr13.descr = "EntityDefs不匹配。";
			ServerErrorDescrs.serverErrs.Add(serverErr13.id, serverErr13);
			ServerErr serverErr14;
			serverErr14.id = 13;
			serverErr14.name = "SERVER_ERR_SERVER_IN_SHUTTINGDOWN";
			serverErr14.descr = "服务器正在关闭中。";
			ServerErrorDescrs.serverErrs.Add(serverErr14.id, serverErr14);
			ServerErr serverErr15;
			serverErr15.id = 14;
			serverErr15.name = "SERVER_ERR_NAME_MAIL";
			serverErr15.descr = "Email地址错误。";
			ServerErrorDescrs.serverErrs.Add(serverErr15.id, serverErr15);
			ServerErr serverErr16;
			serverErr16.id = 15;
			serverErr16.name = "SERVER_ERR_ACCOUNT_LOCK";
			serverErr16.descr = "账号被冻结。";
			ServerErrorDescrs.serverErrs.Add(serverErr16.id, serverErr16);
			ServerErr serverErr17;
			serverErr17.id = 16;
			serverErr17.name = "SERVER_ERR_ACCOUNT_DEADLINE";
			serverErr17.descr = "账号已过期。";
			ServerErrorDescrs.serverErrs.Add(serverErr17.id, serverErr17);
			ServerErr serverErr18;
			serverErr18.id = 17;
			serverErr18.name = "SERVER_ERR_ACCOUNT_NOT_ACTIVATED";
			serverErr18.descr = "账号未激活。";
			ServerErrorDescrs.serverErrs.Add(serverErr18.id, serverErr18);
			ServerErr serverErr19;
			serverErr19.id = 18;
			serverErr19.name = "SERVER_ERR_VERSION_NOT_MATCH";
			serverErr19.descr = "与服务端的版本不匹配。";
			ServerErrorDescrs.serverErrs.Add(serverErr19.id, serverErr19);
			ServerErr serverErr20;
			serverErr20.id = 19;
			serverErr20.name = "SERVER_ERR_OP_FAILED";
			serverErr20.descr = "操作失败。";
			ServerErrorDescrs.serverErrs.Add(serverErr20.id, serverErr20);
			ServerErr serverErr21;
			serverErr21.id = 20;
			serverErr21.name = "SERVER_ERR_SRV_STARTING";
			serverErr21.descr = "服务器正在启动中。";
			ServerErrorDescrs.serverErrs.Add(serverErr21.id, serverErr21);
			ServerErr serverErr22;
			serverErr22.id = 21;
			serverErr22.name = "SERVER_ERR_ACCOUNT_REGISTER_NOT_AVAILABLE";
			serverErr22.descr = "未开放账号注册功能。";
			ServerErrorDescrs.serverErrs.Add(serverErr22.id, serverErr22);
			ServerErr serverErr23;
			serverErr23.id = 22;
			serverErr23.name = "SERVER_ERR_CANNOT_USE_MAIL";
			serverErr23.descr = "不能使用email地址。";
			ServerErrorDescrs.serverErrs.Add(serverErr23.id, serverErr23);
			ServerErr serverErr24;
			serverErr24.id = 23;
			serverErr24.name = "SERVER_ERR_NOT_FOUND_ACCOUNT";
			serverErr24.descr = "找不到此账号。";
			ServerErrorDescrs.serverErrs.Add(serverErr24.id, serverErr24);
			ServerErr serverErr25;
			serverErr25.id = 24;
			serverErr25.name = "SERVER_ERR_DB";
			serverErr25.descr = "数据库错误(请检查dbmgr日志和DB)。";
			ServerErrorDescrs.serverErrs.Add(serverErr25.id, serverErr25);
			ServerErr serverErr26;
			serverErr26.id = 25;
			serverErr26.name = "SERVER_ERR_USER1";
			serverErr26.descr = "用户自定义错误码1。";
			ServerErrorDescrs.serverErrs.Add(serverErr26.id, serverErr26);
			ServerErr serverErr27;
			serverErr27.id = 26;
			serverErr27.name = "SERVER_ERR_USER2";
			serverErr27.descr = "用户自定义错误码2。";
			ServerErrorDescrs.serverErrs.Add(serverErr27.id, serverErr27);
			ServerErr serverErr28;
			serverErr28.id = 27;
			serverErr28.name = "SERVER_ERR_USER3";
			serverErr28.descr = "用户自定义错误码3。";
			ServerErrorDescrs.serverErrs.Add(serverErr28.id, serverErr28);
			ServerErr serverErr29;
			serverErr29.id = 28;
			serverErr29.name = "SERVER_ERR_USER4";
			serverErr29.descr = "用户自定义错误码4。";
			ServerErrorDescrs.serverErrs.Add(serverErr29.id, serverErr29);
			ServerErr serverErr30;
			serverErr30.id = 29;
			serverErr30.name = "SERVER_ERR_USER5";
			serverErr30.descr = "用户自定义错误码5。";
			ServerErrorDescrs.serverErrs.Add(serverErr30.id, serverErr30);
			ServerErr serverErr31;
			serverErr31.id = 30;
			serverErr31.name = "SERVER_ERR_USER6";
			serverErr31.descr = "用户自定义错误码6。";
			ServerErrorDescrs.serverErrs.Add(serverErr31.id, serverErr31);
			ServerErr serverErr32;
			serverErr32.id = 31;
			serverErr32.name = "SERVER_ERR_USER7";
			serverErr32.descr = "用户自定义错误码7。";
			ServerErrorDescrs.serverErrs.Add(serverErr32.id, serverErr32);
			ServerErr serverErr33;
			serverErr33.id = 32;
			serverErr33.name = "SERVER_ERR_USER8";
			serverErr33.descr = "用户自定义错误码8。";
			ServerErrorDescrs.serverErrs.Add(serverErr33.id, serverErr33);
			ServerErr serverErr34;
			serverErr34.id = 33;
			serverErr34.name = "SERVER_ERR_USER9";
			serverErr34.descr = "用户自定义错误码9。";
			ServerErrorDescrs.serverErrs.Add(serverErr34.id, serverErr34);
			ServerErr serverErr35;
			serverErr35.id = 34;
			serverErr35.name = "SERVER_ERR_USER10";
			serverErr35.descr = "用户自定义错误码10。";
			ServerErrorDescrs.serverErrs.Add(serverErr35.id, serverErr35);
			ServerErr serverErr36;
			serverErr36.id = 35;
			serverErr36.name = "SERVER_ERR_LOCAL_PROCESSING";
			serverErr36.descr = "本地处理，通常为某件事情不由第三方处理而是由KBE服务器处理。";
			ServerErrorDescrs.serverErrs.Add(serverErr36.id, serverErr36);
			ServerErr serverErr37;
			serverErr37.id = 36;
			serverErr37.name = "SERVER_ERR_ACCOUNT_RESET_PASSWORD_NOT_AVAILABLE";
			serverErr37.descr = "未开放账号重置密码功能。";
			ServerErrorDescrs.serverErrs.Add(serverErr37.id, serverErr37);
			ServerErr serverErr38;
			serverErr38.id = 37;
			serverErr38.name = "SERVER_ERR_ACCOUNT_LOGIN_ANOTHER_SERVER";
			serverErr38.descr = "当前账号在其他服务器登陆了。";
			ServerErrorDescrs.serverErrs.Add(serverErr38.id, serverErr38);
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x0023C9F2 File Offset: 0x0023ABF2
		public void Clear()
		{
			ServerErrorDescrs.serverErrs.Clear();
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x0023CA00 File Offset: 0x0023AC00
		public string serverErrStr(ushort id)
		{
			ServerErr serverErr;
			if (!ServerErrorDescrs.serverErrs.TryGetValue(id, out serverErr))
			{
				return "";
			}
			return serverErr.name + "[" + serverErr.descr + "]";
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x0023CA40 File Offset: 0x0023AC40
		public ServerErr serverErr(ushort id)
		{
			ServerErr result;
			ServerErrorDescrs.serverErrs.TryGetValue(id, out result);
			return result;
		}

		// Token: 0x04005107 RID: 20743
		public static Dictionary<ushort, ServerErr> serverErrs = new Dictionary<ushort, ServerErr>();
	}
}
