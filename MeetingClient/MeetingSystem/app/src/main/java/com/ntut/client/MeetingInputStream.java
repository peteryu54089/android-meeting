package com.ntut.client;

import java.io.BufferedReader;
import java.io.DataInput;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;

import com.ntut.structure.ScoreSettingDataItem;
import com.ntut.utils.GsonUtils;

import android.R.integer;
import android.R.string;
import android.util.Log;

public class MeetingInputStream implements DataInput {
	private BufferedReader reader;
	private int cmd;
	private String msg;

	public MeetingInputStream(InputStream stream) {
		reader = new BufferedReader(new InputStreamReader(stream));
		setCmd(0);
		setMsg("");
	}

	// @Override
	// public boolean readBoolean() throws IOException {
	// return dis.readBoolean();
	// }
	//
	// @Override
	// public byte readByte() throws IOException {
	// // TODO Auto-generated method stub
	// return dis.readByte();
	// }
	//
	// @Override
	// public char readChar() throws IOException {
	// // TODO Auto-generated method stub
	// return dis.readChar();
	// }
	//
	// @Override
	// public double readDouble() throws IOException {
	// // TODO Auto-generated method stub
	// return dis.readDouble();
	// }
	//
	// @Override
	// public float readFloat() throws IOException {
	// return dis.readFloat();
	// }
	//
	// @Override
	// public void readFully(byte[] dst) throws IOException {
	// dis.readFully(dst);
	// }
	//
	// @Override
	// public void readFully(byte[] dst, int offset, int byteCount)
	// throws IOException {
	// dis.readFully(dst,offset,byteCount);
	// }
	//
	// @Override
	// public int readInt() throws IOException {
	//
	// return dis.readInt();
	// }
	//
	// @Override
	// public String readLine() throws IOException {
	// int length = dis.readInt();
	// byte[] buffer = new byte[length];
	// dis.read(buffer);
	// return new String(buffer, "UTF-8");
	// }
	//
	// @Override
	// public long readLong() throws IOException {
	// return dis.readLong();
	// }
	//
	// @Override
	// public short readShort() throws IOException {
	// return dis.readShort();
	// }
	//
	// @Override
	// public String readUTF() throws IOException {
	// return dis.readLine();
	// }
	//
	// @Override
	// public int readUnsignedByte() throws IOException {
	// return dis.readUnsignedByte();
	// }
	//
	// @Override
	// public int readUnsignedShort() throws IOException {
	// return dis.readUnsignedShort();
	// }
	//
	// @Override
	// public int skipBytes(int count) throws IOException {
	// return dis.skipBytes(count);
	// }

	public void readMessage() {
		String line = null;
		try {
			line = reader.readLine();
			Log.d("223", line+"");
		} catch (IOException e) {
			e.printStackTrace();
		}
		if (line != null) {
			int cmdStart = line.indexOf("cmd:");
			int cmdEnd = line.indexOf("msg:", cmdStart);
			if (cmdStart != -1 && cmdEnd != -1)
				setCmd(Integer.parseInt(line.substring(cmdStart + 4, cmdEnd)));

			int msgStart = line.indexOf("msg:");
			int msgEnd = line.indexOf("]", msgStart);
			if (msgStart != -1 && msgEnd != -1)
				setMsg(line.substring(msgStart + 4, msgEnd + 1));
			Log.d("123", "Cmd:" + getCmd() + "\n" + "Msg:" + getMsg());
		}
	}

	public int getCmd() {
		return cmd;
	}

	public void setCmd(int cmd) {
		this.cmd = cmd;
	}

	public String getMsg() {
		return msg;
	}

	public void setMsg(String msg) {
		this.msg = msg;
	}

	@Override
	public boolean readBoolean() throws IOException {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public byte readByte() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public char readChar() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public double readDouble() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public float readFloat() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public void readFully(byte[] dst) throws IOException {
		// TODO Auto-generated method stub

	}

	@Override
	public void readFully(byte[] dst, int offset, int byteCount)
			throws IOException {
		// TODO Auto-generated method stub

	}

	@Override
	public int readInt() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public String readLine() throws IOException {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public long readLong() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public short readShort() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int readUnsignedByte() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int readUnsignedShort() throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public String readUTF() throws IOException {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public int skipBytes(int count) throws IOException {
		// TODO Auto-generated method stub
		return 0;
	}
}
