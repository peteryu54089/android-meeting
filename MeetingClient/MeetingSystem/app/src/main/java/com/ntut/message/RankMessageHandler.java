package com.ntut.message;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;

import com.ntut.client.MeetingInputStream;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.message.VoteMessageHandler.VoteMessageMode;
import com.ntut.structure.RankSettingDataItem;
import com.ntut.structure.VoteSettingDataItem;
import com.ntut.utils.GsonUtils;

import android.content.Context;
import android.util.Log;

public class RankMessageHandler implements IMessage{
	private final static String TAG = "RankMessageHandler";
	public static enum RankMessageMode{
		RankRequest,RankResponse
	}
	public static void init() {
		MessagePool.registerMessage(ClientMessage.RankMessage,
				new RankMessageHandler());
	}

	@Override
	public void sendMessage(DataOutput dataOutput, Object obj)
			throws IOException {
		int mode = (obj != null) ?RankMessageMode.RankResponse.ordinal() : RankMessageMode.RankRequest.ordinal();
		dataOutput.writeInt(ClientMessage.RankMessage.ordinal());
		Log.d(TAG, "sendMessage mode : "+String.valueOf(mode));
		dataOutput.writeInt(mode);
		if (obj != null) {
			String jsonStr = GsonUtils.ObjectToJson(obj);
			Log.d(TAG, jsonStr);
			dataOutput.writeUTF(jsonStr);
		}
		
	}

	@Override
	public void handlerMessage(Context context, Object obj) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public Object receivedMessage(DataInput dataInput) throws IOException {
		// TODO Auto-generated method stub
		String jsonStr = ((MeetingInputStream)dataInput).getMsg();
		Log.d(TAG, jsonStr + "");
		RankSettingDataItem[] obj=(RankSettingDataItem[])GsonUtils.JsonToObject(jsonStr, RankSettingDataItem[].class);
		Log.d(TAG, String.valueOf(obj.length));
		return obj;
	}

}
