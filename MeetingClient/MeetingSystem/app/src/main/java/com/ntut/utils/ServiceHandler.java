package com.ntut.utils;

import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;

import com.ntut.client.MeetingClient;
import com.ntut.message.MessagePool.ClientMessage;

public class ServiceHandler extends Handler {
	private final static String TAG = "ServiceHandler";

	private MeetingClient client = null;

	public ServiceHandler(Looper looper, MeetingClient client) {
		super(looper);
		this.client = client;
	}

	@Override
	public void handleMessage(Message msg) {
		int messageIdx = msg.what;
		Object obj = msg.obj;
		ClientMessage clientMessage = ClientMessage.values()[messageIdx];
		switch (clientMessage) {
		case ScoreMessage:
		case VoteMessage: 
		case RankMessage:
			Messenger messenger = (Messenger) msg.replyTo;
			if (messenger != null) {
				Message message = Message.obtain();
				//message.what = ClientMessage.ScoreMessage.ordinal();
				message.what = msg.what;
				Bundle data = new Bundle(); 
				message.setData(data);
				try {
					messenger.send(message);
				} catch (RemoteException e) {
					e.printStackTrace();
				}
			}
			try {
				Log.i(TAG,clientMessage.toString());
				if(obj!=null)
					Log.d("test",obj.toString());
				client.send(clientMessage, obj);
			} catch (Exception e) {
				e.printStackTrace();
			}
			break;
		case Close://清除檔案
			Log.d(TAG, "Close");
			break;
		case Abort:
			Log.d(TAG, "Abort");
			break;
		default:
			Log.e(TAG, String.format("%s not found.", clientMessage.toString()));
			super.handleMessage(msg);
			break;
		}
	}
}
