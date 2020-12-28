package com.ntut.activity;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Messenger;
import android.util.Log;
import android.view.Menu;
import android.view.WindowManager;

import com.ntut.mainactivity.R;
import com.ntut.service.MeetingService;
import com.ntut.service.NetworkService;
import com.ntut.structure.ServerInfo;
import com.ntut.utils.ActivityHandler;

public class MeetingMainActivity extends Activity {
	private final String TAG = "MeetingMainActivity";
	public final static String INFO = "SERVER_INFO";
	public final static String ACTIVITY_MESSENGER = "ACTIVITY_MESSENGER";
	public final static String SERVICE_MESSENGER="SERVICE_MESSENGER";
	private Intent service = new Intent();
	private Messenger serviceMessenger = null;
	private Messenger activityMessenger = null;
	private ActivityHandler activityHandler = new ActivityHandler(this);
	
	@Override
	public void onBackPressed() {
		if(getFragmentManager().getBackStackEntryCount() > 0)
			super.onBackPressed();
	}

	private ServiceConnection serviceConnection = new ServiceConnection() {
		
		@Override
		public void onServiceDisconnected(ComponentName name) {
			Log.i(TAG, "onServiceDisconnected");
			finish();
		}
		
		@Override
		public void onServiceConnected(ComponentName name, IBinder service) {
			Log.i(TAG, "onServiceConnected");
			serviceMessenger = new Messenger(service);
			getIntent().putExtra(SERVICE_MESSENGER, serviceMessenger);
			getIntent().putExtra(ACTIVITY_MESSENGER, activityMessenger);
//			/****************************************Test code**************************************/
//			/**********************MeetingMainActivity send message to service**********************/
//			Message message = Message.obtain();
//			message.what = ClientMessage.ScoreMessage.ordinal();
//			message.replyTo = activityMessenger;
//			Bundle data = new Bundle();
//			data.putString("RET", "Activity");
//			message.setData(data);
//			try {
//				serviceMessenger.send(message);
//			} catch (RemoteException e) {
//				e.printStackTrace();
//			}
//			/***************************************************************************************/
		}
	};
	
	@Override
	protected void onStart() {
		Log.i(TAG,"onStart");
		super.onStart();
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		Log.i(TAG,"onCreate");
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_meeting_main);
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		createNetworkService();
	}

	@Override
	protected void onPause() {
		Log.i(TAG,"onPause");
		super.onPause();
//		MeetingApplication.meetingMainPaused();
	}

	@Override
	protected void onResume() {
		Log.i(TAG,"onResume");
		super.onResume();
//		MeetingApplication.meetingMainResumed();
	}

	@Override
	protected void onDestroy() {
		Log.i(TAG,"onDestroy");
		super.onDestroy();
		stopService(service);
		unbindService(serviceConnection);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.meeting_main, menu);
		return true;
	}
	
	private void createNetworkService()
	{
		if(activityMessenger == null)
			activityMessenger = new Messenger(activityHandler);
		Bundle bundle = new Bundle();
		bundle.putSerializable(INFO, getServerInfo());
		service.putExtra(ACTIVITY_MESSENGER, activityMessenger);
		service.putExtras(bundle);
		service.setClass(MeetingMainActivity.this, NetworkService.class);
		service.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		startService(service);
		try {
			boolean isSuccess = bindService(service, serviceConnection, BIND_AUTO_CREATE);
			Log.d(TAG, String.format("Bindservice : %s",Boolean.toString(isSuccess)));
		} catch (Exception e) {
			Log.e(TAG,e.getMessage());
		}
	}
	
	private ServerInfo getServerInfo()
	{
		return (ServerInfo)getIntent().getSerializableExtra(MeetingService.INFO);
	}
	
	public Messenger getActivityMessager() {
		Object object = getIntent().getExtras()
				.get(MeetingMainActivity.ACTIVITY_MESSENGER);
		if (object != null)
			return (Messenger) object;
		else {
			return null;
		}

	}

	public Messenger getServiceMessager() {
		Object object = getIntent().getExtras()
				.get(MeetingMainActivity.SERVICE_MESSENGER);
		if (object != null)
			return (Messenger) object;
		else
			return null;
	}
}
