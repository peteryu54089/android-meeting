package com.ntut.utils;

import com.ntut.service.MeetingService;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class MeetingReceiver extends BroadcastReceiver {
	private final String TAG = "MeetingReceiver";
	@Override
	public void onReceive(Context context, Intent intent) {
		String action = intent.getAction();
		Log.i(TAG,"onReceive");
        if(action.equals(Intent.ACTION_BOOT_COMPLETED)) {
        	Log.i(TAG,"ACTION_BOOT_COMPLETED");
            Intent service = new Intent();
            service.setClass(context, MeetingService.class);
            context.startService(service);
        }
	}

}
