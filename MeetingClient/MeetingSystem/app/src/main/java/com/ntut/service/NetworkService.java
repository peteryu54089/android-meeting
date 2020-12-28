package com.ntut.service;

import android.app.Service;
import android.content.Intent;
import android.os.Bundle;
import android.os.HandlerThread;
import android.os.IBinder;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;

import com.ntut.activity.MeetingMainActivity;
import com.ntut.client.MeetingClient;
import com.ntut.meetingInterface.MeetingClientCallback;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ServerInfo;
import com.ntut.utils.ServiceHandler;

public class NetworkService extends Service implements MeetingClientCallback {
	private final String TAG = "NetworkService";
	private ServerInfo info = null;
	private MeetingClient client = null;
	private Messenger serviceMessenger = null;
	private Messenger activityMessenger = null;
	private ServiceHandler handler = null;
	private HandlerThread handlerThread = null;

	@Override
	public IBinder onBind(Intent intent) {
		Log.i(TAG, "onBind");
		if (handler == null)
			handler = new ServiceHandler(handlerThread.getLooper(), client);
		if (serviceMessenger == null)
			serviceMessenger = new Messenger(handler);
//		/******************************************Test code************************************/
//		/********************* MeetingMainActivity send message to service *********************/
//		Message message = Message.obtain();
//		message.what = ClientMessage.ScoreMessage.ordinal();
//		message.replyTo = serviceMessenger;
//		Bundle data = new Bundle();
//		data.putString("RET", "Service");
//		message.setData(data);
//		try {
//			activityMessenger.send(message);
//		} catch (RemoteException e) {
//			e.printStackTrace();
//		}
//		/*************************************************************************************/
		return serviceMessenger.getBinder();
	}

	@Override
	public void onCreate() {
		Log.i(TAG, "onCreate");
		super.onCreate();
		handlerThread = new HandlerThread("NetworkService_HandlerThread");
		handlerThread.start();
	}

	@Override
	public void onDestroy() {
		Log.i(TAG, "onDestroy");
		super.onDestroy();
		stopClient();
		client = null;
		handlerThread.quit();
	}

	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		Log.i(TAG, "onStartCommand");
		if (intent != null) {
			ServerInfo info = getServerInfo(intent);
			activityMessenger = getActivityMessenger(intent);
			Log.i(TAG, info.toString());
			this.info = info;
			startClient();
		}
		return START_REDELIVER_INTENT;
	}

	private void stopClient() {
		if (client != null) {
			client.stop();
			client = null;
		}
	}

	private void startClient() {
		if (client == null) {
			client = new MeetingClient(this,info);
			client.setMeetingClientCallback(this);
		}
		client.start();
	}

	private ServerInfo getServerInfo(Intent intent) {
		return (ServerInfo) intent.getSerializableExtra(MeetingService.INFO);
	}

	private Messenger getActivityMessenger(Intent intent) {
		return (Messenger) intent.getExtras()
				.get(MeetingMainActivity.ACTIVITY_MESSENGER);
	}

	@Override
	public void onError(MeetingClient client, Exception e) {
		Log.i(TAG, "onError");
		if(e != null && e.getMessage() != null)
			Log.e(TAG, e.getMessage());
		try {
			if (activityMessenger != null) {
				Message message = Message.obtain();
				message.what = ClientMessage.Abort.ordinal();
				message.replyTo = serviceMessenger;
				activityMessenger.send(message);
			}
		} catch (RemoteException e1) {
			e1.printStackTrace();
		}
		stopSelf();
	}

	@Override
	public void onReceiced(ClientMessage clientMessage, Object obj) {
		// Not yet tested
		try {
			if (activityMessenger != null) {
				Message message = Message.obtain();
				message.what = clientMessage.ordinal();
				message.obj = obj;
				Bundle data = new Bundle();
				//data.putString("RET", "Service");
				message.setData(data);
				message.replyTo = serviceMessenger;
				activityMessenger.send(message);
			}
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}
}
