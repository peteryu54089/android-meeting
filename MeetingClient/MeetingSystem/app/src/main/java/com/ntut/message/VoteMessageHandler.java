package com.ntut.message;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;

import android.content.Context;
import android.util.Log;

import com.ntut.client.MeetingInputStream;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreSettingDataItem;
import com.ntut.structure.VoteSettingDataItem;
import com.ntut.utils.GsonUtils;

public class VoteMessageHandler implements IMessage {
	private final static String TAG = "VoteMessageHandler";
	private VoteMessageHandler() {
	}
	public static enum VoteMessageMode{
		VoteRequest,VoteResponse
	}
	public static void init() {
		MessagePool.registerMessage(ClientMessage.VoteMessage,
				new VoteMessageHandler());
	}

	@Override
	public void sendMessage(DataOutput dataOutput, Object obj) throws IOException {
		int mode = (obj != null) ? VoteMessageMode.VoteResponse.ordinal() : VoteMessageMode.VoteRequest.ordinal();
		dataOutput.writeInt(ClientMessage.VoteMessage.ordinal());
		Log.d(TAG, "sendMessage mode : "+String.valueOf(mode));
		dataOutput.writeInt(mode);
		if (obj != null) {
			String jsonStr = GsonUtils.ObjectToJson(obj);
			Log.d(TAG, jsonStr);
			dataOutput.writeUTF(jsonStr);
		}
//		String jsonStr = GsonUtils.ObjectToJson(obj);
//		dataOutput.writeInt(ClientMessage.VoteMessage.ordinal());
//		dataOutput.writeUTF(jsonStr);
	}

	@Override
	public void handlerMessage(Context context, Object obj) {
		// TODO Auto-generated method stub

	}

	@Override
	public Object receivedMessage(DataInput dataInput) throws IOException {
		String jsonStr = ((MeetingInputStream)dataInput).getMsg();
		Log.d(TAG, jsonStr + "");
		VoteSettingDataItem[] obj=(VoteSettingDataItem[])GsonUtils.JsonToObject(jsonStr, VoteSettingDataItem[].class);
		Log.d(TAG, String.valueOf(obj.length));
		return obj;
	}

}
