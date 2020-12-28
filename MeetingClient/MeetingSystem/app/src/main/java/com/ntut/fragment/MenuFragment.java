package com.ntut.fragment;

import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;

import android.R.string;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.content.res.Resources;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.ntut.mainactivity.R;

public class MenuFragment extends Fragment implements OnItemClickListener {
	private final static String TAG = "MenuFragment";
	private List<Fragment> fragments = null;
	private TextView randomTextView = null;
	private RelativeLayout idLayout = null;
	private ImageView imageView = null;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		return inflater.inflate(R.layout.menu, container, false);
	}

	@Override
	public void onStart() {
		super.onStart();
		fragments = createFragments();
		initControlls();
		if (fragments.size() > 0)
			replaceFragment(R.id.content_fragment, fragments.get(0));
		ListView listView = (ListView) getActivity().findViewById(
				R.id.menu_list);
		Resources res = getResources();
		String[] items = res.getStringArray(R.array.list_items);
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(getActivity(),
				R.layout.list_item, items);
		listView.setAdapter(adapter);
		listView.setOnItemClickListener(this);
		
		 randomTextView.setText(getLocalIpAddress());
		 idLayout.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				int visibility = (imageView.getVisibility() == View.VISIBLE)? View.INVISIBLE : View.VISIBLE;
				imageView.setVisibility(visibility);
			}
		});
//		 randomTextView.setOnClickListener(new OnClickListener() {
//			
//			@Override
//			public void onClick(View v) {
//				// TODO Auto-generated method stub				 
//				if(randomTextView.getText().toString().equals("Hide"))
//					randomTextView.setText(getLocalIpAddress());
//				else 
//					randomTextView.setText("Hide");
//						
//			}
//		});
	}

	public String getLocalIpAddress() {
		WifiManager wifiMan = (WifiManager) getActivity().getSystemService(
				Context.WIFI_SERVICE);
		WifiInfo wifiInf = wifiMan.getConnectionInfo();
		int ipAddress = wifiInf.getIpAddress();
		String ip = String.format("%03d", (ipAddress >> 24 & 0xff));
		return ip;
	}

	private void initControlls() {
		randomTextView = (TextView) getActivity().findViewById(R.id.randomTextView);
		idLayout = (RelativeLayout) getActivity().findViewById(R.id.idLayout);
		imageView = (ImageView)getActivity().findViewById(R.id.lockImageView);
	}

	private void replaceFragment(int id, Fragment fragment) {
		FragmentManager fragmentManager = getFragmentManager();
		FragmentTransaction transaction = fragmentManager.beginTransaction();
		transaction.replace(id, fragment);
		// transaction.addToBackStack(null);
		transaction.commit();
	}

	@Override
	public void onItemClick(AdapterView<?> arg0, View view, int pos, long id) {
		Log.i(TAG, String.format("onItemClick : pos(%d) id(%d)", pos, id));
		if (pos == -1 || pos >= fragments.size())
			return;
		Fragment newFragment = fragments.get(pos);
		Log.i(TAG, String.format("onItemClick : newFragment = %s", newFragment
				.getClass().getName()));
		replaceFragment(R.id.content_fragment, newFragment);
	}

	private List<Fragment> createFragments() {
		List<Fragment> fragments = new ArrayList<Fragment>() {
			{
				add(new BroadcastFragment());
				add(new ScoreListFragment());
				add(new VoteListFragment());
				add(new RankListFragment());
				add(new FileExplorerFragment());
			}
		};
		return fragments;
	}
}
