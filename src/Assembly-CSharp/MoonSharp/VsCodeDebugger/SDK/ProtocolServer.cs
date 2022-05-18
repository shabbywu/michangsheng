using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011DB RID: 4571
	public abstract class ProtocolServer
	{
		// Token: 0x06006FD1 RID: 28625 RVA: 0x0004BFFD File Offset: 0x0004A1FD
		public ProtocolServer()
		{
			this._sequenceNumber = 1;
			this._bodyLength = -1;
			this._rawData = new ByteBuffer();
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x002A070C File Offset: 0x0029E90C
		public void ProcessLoop(Stream inputStream, Stream outputStream)
		{
			this._outputStream = outputStream;
			byte[] array = new byte[4096];
			this._stopRequested = false;
			while (!this._stopRequested)
			{
				int num = inputStream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				if (num > 0)
				{
					this._rawData.Append(array, num);
					this.ProcessData();
				}
			}
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x0004C01E File Offset: 0x0004A21E
		public void Stop()
		{
			this._stopRequested = true;
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x0004C027 File Offset: 0x0004A227
		public void SendEvent(Event e)
		{
			this.SendMessage(e);
		}

		// Token: 0x06006FD5 RID: 28629
		protected abstract void DispatchRequest(string command, Table args, Response response);

		// Token: 0x06006FD6 RID: 28630 RVA: 0x002A0764 File Offset: 0x0029E964
		private void ProcessData()
		{
			for (;;)
			{
				if (this._bodyLength >= 0)
				{
					if (this._rawData.Length < this._bodyLength)
					{
						break;
					}
					byte[] bytes = this._rawData.RemoveFirst(this._bodyLength);
					this._bodyLength = -1;
					this.Dispatch(ProtocolServer.Encoding.GetString(bytes));
				}
				else
				{
					string @string = this._rawData.GetString(ProtocolServer.Encoding);
					int num = @string.IndexOf("\r\n\r\n");
					if (num == -1)
					{
						break;
					}
					Match match = ProtocolServer.CONTENT_LENGTH_MATCHER.Match(@string);
					if (!match.Success || match.Groups.Count != 2)
					{
						break;
					}
					this._bodyLength = Convert.ToInt32(match.Groups[1].ToString());
					this._rawData.RemoveFirst(num + "\r\n\r\n".Length);
				}
			}
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x002A0838 File Offset: 0x0029EA38
		private void Dispatch(string req)
		{
			try
			{
				Table table = JsonTableConverter.JsonToTable(req, null);
				if (table != null && table["type"].ToString() == "request")
				{
					if (this.TRACE)
					{
						Console.Error.WriteLine(string.Format("C {0}: {1}", table["command"], req));
					}
					Response response = new Response(table);
					this.DispatchRequest(table.Get("command").String, table.Get("arguments").Table, response);
					this.SendMessage(response);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x002A08E0 File Offset: 0x0029EAE0
		protected void SendMessage(ProtocolMessage message)
		{
			int sequenceNumber = this._sequenceNumber;
			this._sequenceNumber = sequenceNumber + 1;
			message.seq = sequenceNumber;
			if (this.TRACE_RESPONSE && message.type == "response")
			{
				Console.Error.WriteLine(string.Format(" R: {0}", JsonTableConverter.ObjectToJson(message)));
			}
			if (this.TRACE && message.type == "event")
			{
				Event @event = (Event)message;
				Console.Error.WriteLine(string.Format("E {0}: {1}", @event.@event, JsonTableConverter.ObjectToJson(@event.body)));
			}
			byte[] array = ProtocolServer.ConvertToBytes(message);
			try
			{
				this._outputStream.Write(array, 0, array.Length);
				this._outputStream.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x002A09B8 File Offset: 0x0029EBB8
		private static byte[] ConvertToBytes(ProtocolMessage request)
		{
			string s = JsonTableConverter.ObjectToJson(request);
			byte[] bytes = ProtocolServer.Encoding.GetBytes(s);
			string s2 = string.Format("Content-Length: {0}{1}", bytes.Length, "\r\n\r\n");
			byte[] bytes2 = ProtocolServer.Encoding.GetBytes(s2);
			byte[] array = new byte[bytes2.Length + bytes.Length];
			Buffer.BlockCopy(bytes2, 0, array, 0, bytes2.Length);
			Buffer.BlockCopy(bytes, 0, array, bytes2.Length, bytes.Length);
			return array;
		}

		// Token: 0x040062CA RID: 25290
		public bool TRACE;

		// Token: 0x040062CB RID: 25291
		public bool TRACE_RESPONSE;

		// Token: 0x040062CC RID: 25292
		protected const int BUFFER_SIZE = 4096;

		// Token: 0x040062CD RID: 25293
		protected const string TWO_CRLF = "\r\n\r\n";

		// Token: 0x040062CE RID: 25294
		protected static readonly Regex CONTENT_LENGTH_MATCHER = new Regex("Content-Length: (\\d+)");

		// Token: 0x040062CF RID: 25295
		protected static readonly Encoding Encoding = Encoding.UTF8;

		// Token: 0x040062D0 RID: 25296
		private int _sequenceNumber;

		// Token: 0x040062D1 RID: 25297
		private Stream _outputStream;

		// Token: 0x040062D2 RID: 25298
		private ByteBuffer _rawData;

		// Token: 0x040062D3 RID: 25299
		private int _bodyLength;

		// Token: 0x040062D4 RID: 25300
		private bool _stopRequested;
	}
}
