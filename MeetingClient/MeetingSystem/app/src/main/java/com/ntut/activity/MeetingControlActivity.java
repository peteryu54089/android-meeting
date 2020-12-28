package com.ntut.activity;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.ActivityManager.RunningServiceInfo;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.WindowManager;

import com.ntut.mainactivity.R;
import com.ntut.service.MeetingService;
import com.ntut.utils.MeetingApplication;

public class MeetingControlActivity extends Activity{
	private final String TAG = "MeetingControlActivity";
	private ProgressDialog dialog = null;
	private Intent service = new Intent();
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_meeting_control);
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		service = new Intent();
		service.setClass(this, MeetingService.class);
	}

	@Override
	protected void onPause() {
		super.onPause();
		stopService(service);
		MeetingApplication.meetingControlPaused();
		if (dialog != null)
			dialog.dismiss();
	}

	@Override
	protected void onResume() {
		super.onResume();
		startService(service);
		if (dialog == null)
			dialog = ProgressDialog.show(this, "", "等待會議開始...", true);
		MeetingApplication.meetingControlResumed();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
}
