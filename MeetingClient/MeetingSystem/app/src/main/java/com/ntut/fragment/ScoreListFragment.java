package com.ntut.fragment;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;

import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.os.Bundle;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.BaseAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import com.ntut.activity.MeetingMainActivity;
import com.ntut.mainactivity.R;
import com.ntut.meetingInterface.Updatable;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreSettingDataItem;

public class ScoreListFragment extends Fragment implements Updatable,Serializable{
	@Override
	public void onResume() { 
		Log.i(TAG, "onResume");
		super.onResume();
	}
	private final String TAG = "ScoreListActivity";
	public final static String UPDATER = "Updater";
	private final int SECOND = 1000;
	private final int UPDATE_INTERVAL = 2 * SECOND;
	private ArrayList<HashMap<String, Object>> list = new ArrayList<HashMap<String, Object>>();
	private ListView listView = null;
	private ListAdapter adapter = null;
	private Messenger serviceMessenger = null;
	private Messenger activityMessenger = null;
	private Thread updateThread = null;
	public ScoreListFragment()
	{
		
	}
	@Override
	public void onStart() {
		Log.i(TAG, "onStart");
		super.onStart();
		initControls();
		serviceMessenger = ((MeetingMainActivity)getActivity()).getServiceMessager();
		activityMessenger = ((MeetingMainActivity)getActivity()).getActivityMessager();
		getActivity().getIntent().putExtra(UPDATER, this);
		if(updateThread == null)
		{
			updateThread = new Thread(updateRunnable);
			updateThread.start();
		}
	}

	@Override
	public void onStop() {
		Log.i(TAG, "onStop");
		super.onStop();
		if(updateThread != null)
		{
			updateThread.interrupt();
			updateThread = null;
		}
	}

	private void initControls() {
		getActivity().getWindow().setFlags(
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		listView = (ListView) getActivity().findViewById(R.id.serverListView);
		TextView emptyTextView=(TextView) getActivity().findViewById(R.id.emptyText);
		emptyTextView.setText("評分表為空");
		listView.setEmptyView(emptyTextView);
		adapter = new SimpleAdapter(getActivity(), list,
				R.layout.scorelistitem, new String[] { "title", "desc" },
				new int[] { R.id.titleTextView, R.id.descTextView });

		listView.setAdapter(adapter);
		listView.setOnItemClickListener(itemClickListener);
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		Log.i(TAG, "onCreateView");
		return inflater.inflate(R.layout.score, container, false);
	}

	private OnItemClickListener itemClickListener = new OnItemClickListener() {

		public void onItemClick(AdapterView<?> view, View v, int pos, long arg3) {
			HashMap<String, Object> item = (HashMap<String, Object>) view.getItemAtPosition(pos);
			ScoreSettingDataItem socreSettingItem = (ScoreSettingDataItem) item
					.get("scoreSettingDataItem");
			Intent intent = getActivity().getIntent();
			intent.putExtra("url", socreSettingItem.getScoreWebIp());
			intent.putExtra("scoreId", socreSettingItem.getScoreId());
			intent.putExtra("fileName", socreSettingItem.getWebFileName());
			ScoreFragment scoreFragment=new ScoreFragment();
			getActivity().getIntent().putExtra(ScoreListFragment.UPDATER, ScoreListFragment.this);
			replaceFragment(android.R.id.content, scoreFragment);
		}
	};
 
	private void replaceFragment(int id,Fragment fragment)
	{
		FragmentManager fragmentManager = getFragmentManager();
		FragmentTransaction transaction = fragmentManager.beginTransaction();
		transaction.replace(id, fragment);
		transaction.addToBackStack(null);
		transaction.commit();
	}

	private Runnable updateRunnable = new Runnable() {

		@Override
		public void run() {
			while (updateThread != null && !updateThread.interrupted()) {
				try {
					sendUpdateMessage();
					Thread.sleep(UPDATE_INTERVAL);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
		}
	};
	@Override
	public synchronized void sendUpdateMessage() {
		if (serviceMessenger != null && activityMessenger != null) {
			Message message = Message.obtain();
			message.what = ClientMessage.ScoreMessage.ordinal();
			message.replyTo = activityMessenger;

			Bundle data = new Bundle(); 
			message.setData(data);
			try {
				serviceMessenger.send(message);
			} catch (RemoteException e) {
				e.printStackTrace();
			}
		}
	}

	@Override
	public synchronized void  onUpdate(Object settingDataItems) {
		Log.i(TAG, "onUpdate");
		ScoreSettingDataItem[] scoreSettingDataItems=(ScoreSettingDataItem[])settingDataItems;
		list.clear();
		for (ScoreSettingDataItem scoreSettingDataItem : scoreSettingDataItems) {
			HashMap<String, Object> item = new HashMap<String, Object>();
			item.put("scoreSettingDataItem", scoreSettingDataItem);
			int start = scoreSettingDataItem.getScoreSourcePath().lastIndexOf(
					'\\') + 1;
			int end = scoreSettingDataItem.getScoreSourcePath()
					.lastIndexOf('.');
			item.put("title", scoreSettingDataItem.getScoreSourcePath()
					.substring(start, end));
			item.put("desc", "");
			list.add(item);
		}
		((BaseAdapter) adapter).notifyDataSetChanged();
	}
}
