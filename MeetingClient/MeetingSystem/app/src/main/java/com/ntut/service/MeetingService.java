package com.ntut.service;

import android.app.Service;
import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.util.Log;

import com.ntut.activity.MeetingMainActivity;
import com.ntut.meetingInterface.ServerCallback;
import com.ntut.structure.ServerInfo;
import com.ntut.utils.ServerFinder;

public class MeetingService extends Service implements ServerCallback {
	private final String TAG = "MeetingService";
	public final static String INFO = "SERVER_INFO";
	private ServerFinder finder = null;

	@Override
	public void onCreate() {
		Log.i(TAG, "onCreate");
		if (finder == null) {
			finder = new ServerFinder();
			finder.setServerCallback(this);
			finder.start();
		}
		super.onCreate();
	}

	@Override
	public IBinder onBind(Intent arg0) {
		Log.i(TAG, "onBind");
		return null;
	}

	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		Log.i(TAG, "onStartCommand");
		return START_STICKY;
	}

	@Override
	public void onDestroy() {
		Log.i(TAG, "onDestroy");
		releaseServerFinder();
		super.onDestroy();
	}

	@Override
	public void onServerFinded(ServerInfo info) {
		Log.d(TAG, "onServerFinded");
		Log.d(TAG, info.toString());
		releaseServerFinder();
		//openNetworkService(info);
		openMeetingActivity(info);
		stopSelf();
	}

	private void releaseServerFinder() {
		if (finder != null) {
			finder.stop();
			finder = null;
		}
	}

	private void openNetworkService(ServerInfo info) {
		Intent service = new Intent();
		Bundle bundle = new Bundle();
		bundle.putSerializable(INFO, info);
		service.putExtras(bundle);
		service.setClass(getApplicationContext(), NetworkService.class);
		service.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		startService(service);
	}

	private void openMeetingActivity(ServerInfo info) {
		Log.d(TAG, "openMeetingActivity");
		Intent activity = new Intent();
		Bundle bundle = new Bundle();
		bundle.putSerializable(INFO, info);
		activity.putExtras(bundle);
		activity.setClass(getApplicationContext(), MeetingMainActivity.class);
		activity.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		activity.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);
		startActivity(activity);
	};
}
