using System.Collections.Generic;

namespace KBEngine;

public class ServerErrorDescrs
{
	public static Dictionary<ushort, ServerErr> serverErrs = new Dictionary<ushort, ServerErr>();

	public ServerErrorDescrs()
	{
		ServerErr value = default(ServerErr);
		value.id = 0;
		value.name = "SUCCESS";
		value.descr = "成功。";
		serverErrs.Add(value.id, value);
		ServerErr value2 = default(ServerErr);
		value2.id = 1;
		value2.name = "SERVER_ERR_SRV_NO_READY";
		value2.descr = "服务器没有准备好。";
		serverErrs.Add(value2.id, value2);
		ServerErr value3 = default(ServerErr);
		value3.id = 2;
		value3.name = "SERVER_ERR_SRV_OVERLOAD";
		value3.descr = "服务器负载过重。";
		serverErrs.Add(value3.id, value3);
		ServerErr value4 = default(ServerErr);
		value4.id = 3;
		value4.name = "SERVER_ERR_ILLEGAL_LOGIN";
		value4.descr = "非法登录。";
		serverErrs.Add(value4.id, value4);
		ServerErr value5 = default(ServerErr);
		value5.id = 4;
		value5.name = "SERVER_ERR_NAME_PASSWORD";
		value5.descr = "用户名或者密码不正确。";
		serverErrs.Add(value5.id, value5);
		ServerErr value6 = default(ServerErr);
		value6.id = 5;
		value6.name = "SERVER_ERR_NAME";
		value6.descr = "用户名不正确。";
		serverErrs.Add(value6.id, value6);
		ServerErr value7 = default(ServerErr);
		value7.id = 6;
		value7.name = "SERVER_ERR_PASSWORD";
		value7.descr = "密码不正确。";
		serverErrs.Add(value7.id, value7);
		ServerErr value8 = default(ServerErr);
		value8.id = 7;
		value8.name = "SERVER_ERR_ACCOUNT_CREATE_FAILED";
		value8.descr = "创建账号失败（已经存在一个相同的账号）。";
		serverErrs.Add(value8.id, value8);
		ServerErr value9 = default(ServerErr);
		value9.id = 8;
		value9.name = "SERVER_ERR_BUSY";
		value9.descr = "操作过于繁忙(例如：在服务器前一次请求未执行完毕的情况下连续N次创建账号)。";
		serverErrs.Add(value9.id, value9);
		ServerErr value10 = default(ServerErr);
		value10.id = 9;
		value10.name = "SERVER_ERR_ACCOUNT_LOGIN_ANOTHER";
		value10.descr = "当前账号在另一处登录了。";
		serverErrs.Add(value10.id, value10);
		ServerErr value11 = default(ServerErr);
		value11.id = 10;
		value11.name = "SERVER_ERR_ACCOUNT_IS_ONLINE";
		value11.descr = "账号已登陆。";
		serverErrs.Add(value11.id, value11);
		ServerErr value12 = default(ServerErr);
		value12.id = 11;
		value12.name = "SERVER_ERR_PROXY_DESTROYED";
		value12.descr = "与客户端关联的proxy在服务器上已经销毁。";
		serverErrs.Add(value12.id, value12);
		ServerErr value13 = default(ServerErr);
		value13.id = 12;
		value13.name = "SERVER_ERR_ENTITYDEFS_NOT_MATCH";
		value13.descr = "EntityDefs不匹配。";
		serverErrs.Add(value13.id, value13);
		ServerErr value14 = default(ServerErr);
		value14.id = 13;
		value14.name = "SERVER_ERR_SERVER_IN_SHUTTINGDOWN";
		value14.descr = "服务器正在关闭中。";
		serverErrs.Add(value14.id, value14);
		ServerErr value15 = default(ServerErr);
		value15.id = 14;
		value15.name = "SERVER_ERR_NAME_MAIL";
		value15.descr = "Email地址错误。";
		serverErrs.Add(value15.id, value15);
		ServerErr value16 = default(ServerErr);
		value16.id = 15;
		value16.name = "SERVER_ERR_ACCOUNT_LOCK";
		value16.descr = "账号被冻结。";
		serverErrs.Add(value16.id, value16);
		ServerErr value17 = default(ServerErr);
		value17.id = 16;
		value17.name = "SERVER_ERR_ACCOUNT_DEADLINE";
		value17.descr = "账号已过期。";
		serverErrs.Add(value17.id, value17);
		ServerErr value18 = default(ServerErr);
		value18.id = 17;
		value18.name = "SERVER_ERR_ACCOUNT_NOT_ACTIVATED";
		value18.descr = "账号未激活。";
		serverErrs.Add(value18.id, value18);
		ServerErr value19 = default(ServerErr);
		value19.id = 18;
		value19.name = "SERVER_ERR_VERSION_NOT_MATCH";
		value19.descr = "与服务端的版本不匹配。";
		serverErrs.Add(value19.id, value19);
		ServerErr value20 = default(ServerErr);
		value20.id = 19;
		value20.name = "SERVER_ERR_OP_FAILED";
		value20.descr = "操作失败。";
		serverErrs.Add(value20.id, value20);
		ServerErr value21 = default(ServerErr);
		value21.id = 20;
		value21.name = "SERVER_ERR_SRV_STARTING";
		value21.descr = "服务器正在启动中。";
		serverErrs.Add(value21.id, value21);
		ServerErr value22 = default(ServerErr);
		value22.id = 21;
		value22.name = "SERVER_ERR_ACCOUNT_REGISTER_NOT_AVAILABLE";
		value22.descr = "未开放账号注册功能。";
		serverErrs.Add(value22.id, value22);
		ServerErr value23 = default(ServerErr);
		value23.id = 22;
		value23.name = "SERVER_ERR_CANNOT_USE_MAIL";
		value23.descr = "不能使用email地址。";
		serverErrs.Add(value23.id, value23);
		ServerErr value24 = default(ServerErr);
		value24.id = 23;
		value24.name = "SERVER_ERR_NOT_FOUND_ACCOUNT";
		value24.descr = "找不到此账号。";
		serverErrs.Add(value24.id, value24);
		ServerErr value25 = default(ServerErr);
		value25.id = 24;
		value25.name = "SERVER_ERR_DB";
		value25.descr = "数据库错误(请检查dbmgr日志和DB)。";
		serverErrs.Add(value25.id, value25);
		ServerErr value26 = default(ServerErr);
		value26.id = 25;
		value26.name = "SERVER_ERR_USER1";
		value26.descr = "用户自定义错误码1。";
		serverErrs.Add(value26.id, value26);
		ServerErr value27 = default(ServerErr);
		value27.id = 26;
		value27.name = "SERVER_ERR_USER2";
		value27.descr = "用户自定义错误码2。";
		serverErrs.Add(value27.id, value27);
		ServerErr value28 = default(ServerErr);
		value28.id = 27;
		value28.name = "SERVER_ERR_USER3";
		value28.descr = "用户自定义错误码3。";
		serverErrs.Add(value28.id, value28);
		ServerErr value29 = default(ServerErr);
		value29.id = 28;
		value29.name = "SERVER_ERR_USER4";
		value29.descr = "用户自定义错误码4。";
		serverErrs.Add(value29.id, value29);
		ServerErr value30 = default(ServerErr);
		value30.id = 29;
		value30.name = "SERVER_ERR_USER5";
		value30.descr = "用户自定义错误码5。";
		serverErrs.Add(value30.id, value30);
		ServerErr value31 = default(ServerErr);
		value31.id = 30;
		value31.name = "SERVER_ERR_USER6";
		value31.descr = "用户自定义错误码6。";
		serverErrs.Add(value31.id, value31);
		ServerErr value32 = default(ServerErr);
		value32.id = 31;
		value32.name = "SERVER_ERR_USER7";
		value32.descr = "用户自定义错误码7。";
		serverErrs.Add(value32.id, value32);
		ServerErr value33 = default(ServerErr);
		value33.id = 32;
		value33.name = "SERVER_ERR_USER8";
		value33.descr = "用户自定义错误码8。";
		serverErrs.Add(value33.id, value33);
		ServerErr value34 = default(ServerErr);
		value34.id = 33;
		value34.name = "SERVER_ERR_USER9";
		value34.descr = "用户自定义错误码9。";
		serverErrs.Add(value34.id, value34);
		ServerErr value35 = default(ServerErr);
		value35.id = 34;
		value35.name = "SERVER_ERR_USER10";
		value35.descr = "用户自定义错误码10。";
		serverErrs.Add(value35.id, value35);
		ServerErr value36 = default(ServerErr);
		value36.id = 35;
		value36.name = "SERVER_ERR_LOCAL_PROCESSING";
		value36.descr = "本地处理，通常为某件事情不由第三方处理而是由KBE服务器处理。";
		serverErrs.Add(value36.id, value36);
		ServerErr value37 = default(ServerErr);
		value37.id = 36;
		value37.name = "SERVER_ERR_ACCOUNT_RESET_PASSWORD_NOT_AVAILABLE";
		value37.descr = "未开放账号重置密码功能。";
		serverErrs.Add(value37.id, value37);
		ServerErr value38 = default(ServerErr);
		value38.id = 37;
		value38.name = "SERVER_ERR_ACCOUNT_LOGIN_ANOTHER_SERVER";
		value38.descr = "当前账号在其他服务器登陆了。";
		serverErrs.Add(value38.id, value38);
	}

	public void Clear()
	{
		serverErrs.Clear();
	}

	public string serverErrStr(ushort id)
	{
		if (!serverErrs.TryGetValue(id, out var value))
		{
			return "";
		}
		return value.name + "[" + value.descr + "]";
	}

	public ServerErr serverErr(ushort id)
	{
		serverErrs.TryGetValue(id, out var value);
		return value;
	}
}
