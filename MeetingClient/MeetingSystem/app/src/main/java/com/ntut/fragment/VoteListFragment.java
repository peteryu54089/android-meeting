package com.ntut.fragment;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;

import com.ntut.activity.MeetingMainActivity;
import com.ntut.mainactivity.R;
import com.ntut.meetingInterface.Updatable;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.ScoreSettingDataItem;
import com.ntut.structure.VoteSettingDataItem;

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

public class VoteListFragment extends Fragment implements Updatable,Serializable{

	private final String TAG = "VoteListActivity";
	private ListView listView = null;
	private ArrayList<HashMap<String, Object>> list = new ArrayList<HashMap<String, Object>>();
	private ListAdapter adapter = null;
	private Messenger serviceMessenger = null;
	private Messenger activityMessenger = null; 
	private Thread updateThread = null;
	private final int UPDATE_INTERVAL = 2000;
	public final static String UPDATER = " VoteListUpdater";
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		return inflater.inflate(R.layout.vote, container, false);
	}
	@Override
	public void onStart() {
		Log.i(TAG, "onStart");super.onStart();
		serviceMessenger = ((MeetingMainActivity)getActivity()).getServiceMessager();
		activityMessenger = ((MeetingMainActivity)getActivity()).getActivityMessager();
		initControls();
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
		// TODO Auto-generated method stub
		getActivity().getWindow().setFlags(
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		listView = (ListView) getActivity().findViewById(R.id.voteListView);
		TextView emptyTextView=(TextView) getActivity().findViewById(R.id.emptyText);
		emptyTextView.setText("投票單為空");
		listView.setEmptyView(emptyTextView);
		adapter = new SimpleAdapter(getActivity(), list,
				R.layout.votelistitem, new String[] { "title", "desc" },
				new int[] { R.id.titleTextView, R.id.descTextView });

		listView.setAdapter(adapter);
		listView.setOnItemClickListener(itemClickListener);
		
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
 
	private OnItemClickListener itemClickListener = new OnItemClickListener() {

		public void onItemClick(AdapterView<?> view, View v, int pos, long arg3) {
			HashMap<String, Object> item = (HashMap<String, Object>) view.getItemAtPosition(pos);
			VoteSettingDataItem voteSettingItem=(VoteSettingDataItem)item.get("voteSettingDataItem");
			Intent intent=getActivity().getIntent();
		    intent.putExtra("url", voteSettingItem.getVoteWebIp());
		    intent.putExtra("voteId", voteSettingItem.getVoteId());
		    intent.putExtra("fileName", voteSettingItem.getWebFileName());
			replaceFragment(android.R.id.content, new VoteFragment());
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
	@Override
	public synchronized void sendUpdateMessage() {
		// TODO Auto-generated method stub
		if (serviceMessenger != null && activityMessenger != null) {
			Message message = Message.obtain();			
			message.what = ClientMessage.VoteMessage.ordinal();
			message.replyTo = activityMessenger;
			try {
				serviceMessenger.send(message);
			} catch (RemoteException e) {
				e.printStackTrace();
			}
		}
	}
	@Override
	public synchronized void onUpdate(Object SettingDataItems) {
		// TODO Auto-generated method stub
		Log.i(TAG, "onUpdate");
		VoteSettingDataItem[] voteSettingDataItems=(VoteSettingDataItem[])SettingDataItems;
		list.clear();
		for (VoteSettingDataItem voteSettingDataItem : voteSettingDataItems) {
			HashMap<String, Object> item = new HashMap<String, Object>();
			item.put("voteSettingDataItem", voteSettingDataItem);
			int start = voteSettingDataItem.getVoteSourcePath().lastIndexOf(
					'\\') + 1;
			int end = voteSettingDataItem.getVoteSourcePath()
					.lastIndexOf('.');
			item.put("title", voteSettingDataItem.getVoteSourcePath()
					.substring(start, end));
			item.put("desc", "");
			list.add(item);
		}
		((BaseAdapter) adapter).notifyDataSetChanged();
	}
		 
}
