package com.ntut.utils;

import android.app.Activity;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.Toast;

import com.ntut.fragment.RankListFragment;
import com.ntut.fragment.ScoreListFragment;
import com.ntut.fragment.VoteListFragment;
import com.ntut.meetingInterface.Updatable;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreSettingDataItem;

public class ActivityHandler extends Handler {
	private final static String TAG = "ActivityHandler";
	private final int DELAY_INTERVAL = 1500;
	private Activity activity = null;

	public ActivityHandler(Activity activity) {
		this.activity = activity;
	}

	@Override
	public void handleMessage(Message msg) {
		int messageIdx = msg.what;
		Object obj = msg.obj;
		ClientMessage clientMessage = ClientMessage.values()[messageIdx];
		switch (clientMessage) {
		case ScoreMessage:
			scoreMessageProcessing(msg, obj);
			break;
		case VoteMessage:
			Log.d(TAG, "VoteMessage");
			voteMessageProcessing(msg,obj);
			break;	
		case RankMessage:
			Log.d(TAG, "RankMessage");
			rankMessageProcessing(msg,obj);
			break;
	 	case FileMessage:
	 		break;
		case Close://清除檔案
			Log.d(TAG, "Close");
			break;
		case Abort:
			Log.d(TAG, "Abort");
			//UNDO:需要清除STORAGE中的DATA
			Toast.makeText(activity, "會議已結束", Toast.LENGTH_LONG).show();
			new Thread(runnable).start();
			break;
		default:
			Log.e(TAG, String.format("%s not found.", clientMessage.toString()));
			super.handleMessage(msg);
			break;
		}
	}

	private void rankMessageProcessing(Message msg,Object obj)
	{
		if(msg.obj != null)
			Log.d(TAG, obj.toString());
		else 
			return;
		Updatable updater = (Updatable)activity.getIntent().getSerializableExtra(RankListFragment.UPDATER);
		updater.onUpdate(obj);
	}
	private void voteMessageProcessing(Message msg, Object obj) {
		// TODO Auto-generated method stub
		if(msg.obj != null)
			Log.d(TAG, obj.toString());
		else 
			return;
		Updatable updater = (Updatable)activity.getIntent().getSerializableExtra(VoteListFragment.UPDATER);
		updater.onUpdate(obj);
	}

	private void scoreMessageProcessing(Message msg, Object obj) {
		//Log.d(TAG, msg.getData().getString("RET"));
		if(msg.obj != null)
			Log.d(TAG, obj.toString());
		else
			return;
		Updatable updater = (Updatable)activity.getIntent().getSerializableExtra(ScoreListFragment.UPDATER);
		updater.onUpdate((ScoreSettingDataItem[]) obj);
	}

	private Runnable runnable = new Runnable() {
		@Override
		public void run() {
			try {
				Thread.sleep(DELAY_INTERVAL);
				activity.finish();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	};
}
