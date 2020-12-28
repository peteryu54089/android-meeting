package com.ntut.client;

import java.io.DataOutput;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.ByteBuffer;

public class MeetingOutputStream implements DataOutput {
	private DataOutputStream dos = null;
	public MeetingOutputStream(OutputStream stream)
	{
		dos = new DataOutputStream(stream);
	}
	
	@Override
	public void write(byte[] buffer) throws IOException {
		dos.write(buffer);
	}

	@Override
	public void write(int oneByte) throws IOException {
		dos.write(oneByte);
	}

	@Override
	public void write(byte[] buffer, int offset, int count) throws IOException {
		dos.write(buffer, offset, count);
	}

	@Override
	public void writeBoolean(boolean val) throws IOException {
		dos.writeBoolean(val);
	}

	@Override
	public void writeByte(int val) throws IOException {
		dos.writeByte(val);
	}

	@Override
	public void writeBytes(String str) throws IOException {
		writeInt(str.getBytes("UTF-8").length);
		dos.write(str.getBytes());
	}

	@Override
	public void writeChar(int val) throws IOException {
		dos.writeChar(val);
	}

	@Override
	public void writeChars(String str) throws IOException {
		writeInt(str.getBytes("UTF-8").length);
		dos.write(str.getBytes());
	}

	@Override
	public void writeDouble(double val) throws IOException {
		dos.writeDouble(val);
	}

	@Override
	public void writeFloat(float val) throws IOException {
		dos.writeFloat(val);
	}

	@Override
	public void writeInt(int val) throws IOException {
		byte[] bytes = ByteBuffer.allocate(4).putInt(val).array();
		dos.write(bytes);
	}

	@Override
	public void writeLong(long val) throws IOException {
		dos.writeLong(val);
	}

	@Override
	public void writeShort(int val) throws IOException {
		dos.writeShort(val);
	}

	@Override
	public void writeUTF(String str) throws IOException {
		writeInt(str.getBytes("UTF-8").length);
		dos.write(str.getBytes());
	}

}
