package com.ntut.message;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;

import android.content.Context;
import android.util.Log;

import com.google.gson.JsonSyntaxException;
import com.ntut.client.MeetingInputStream;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreSettingDataItem;
import com.ntut.utils.GsonUtils;

public class ScoreMessageHandler implements IMessage {
	private final static String TAG = "ScoreMessageHandler";

	private ScoreMessageHandler() {
	}

	public static enum ScoreMessageMode {
		ScoreRequest, ScoreResponse
	}

	public static void init() {
		MessagePool.registerMessage(ClientMessage.ScoreMessage,
				new ScoreMessageHandler());
	}

	@Override
	public void sendMessage(DataOutput dataOutput, Object obj)
			throws IOException {
		int mode = (obj != null) ? ScoreMessageMode.ScoreResponse.ordinal()
				: ScoreMessageMode.ScoreRequest.ordinal();
		dataOutput.writeInt(ClientMessage.ScoreMessage.ordinal());
		Log.d(TAG, "sendMessage mode : " + String.valueOf(mode));
		dataOutput.writeInt(mode);
		if (obj != null) {
			String jsonStr = GsonUtils.ObjectToJson(obj);
			Log.d(TAG, jsonStr);
			dataOutput.writeUTF(jsonStr);
		}
	}

	@Override
	public void handlerMessage(Context context, Object obj) {

	}

	@Override
	public Object receivedMessage(DataInput dataInput) throws IOException {
		//String jsonStr = dataInput.readLine();
		String jsonStr = ((MeetingInputStream)dataInput).getMsg();
		Log.d(TAG, jsonStr + "");
		ScoreSettingDataItem[] obj = (ScoreSettingDataItem[]) GsonUtils
				.JsonToObject(jsonStr, ScoreSettingDataItem[].class);
		if (obj != null)
			Log.d(TAG, String.valueOf(obj.length));
		return obj;
	}
}
