package com.ntut.fragment;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;

import com.ntut.mainactivity.R;

import android.app.Fragment;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
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

public class FileExplorerFragment extends Fragment {
	private final String TAG = "FileExplorerFragment";
	private final String BASE_PATH = Environment.getExternalStorageDirectory().toString() + "/SessionSystem/";
	private final int SECOND = 1000;
	private final int RELOAD_INTERVAL = 2 * SECOND;
	private ListView listView = null;
	private ListAdapter adapter = null;
	private ArrayList<HashMap<String, Object>> list = new ArrayList<HashMap<String, Object>>();
	private Handler handler = new Handler();

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		return inflater.inflate(R.layout.file_explorer, container, false);
	}

	@Override
	public void onStart() {
		// TODO Auto-generated method stub
		Log.d(TAG, "onStart");
		listView = (ListView) getActivity().findViewById(R.id.serverListView);
		TextView emptyTextView = (TextView) getActivity().findViewById(
				R.id.emptyText);
		emptyTextView.setText("檔案庫為空");
		listView.setEmptyView(emptyTextView);
		adapter = new SimpleAdapter(getActivity(), list,
				R.layout.file_listview, new String[] { "Icon", "FileName" },
				new int[] { R.id.imageView1, R.id.textView1 });
		
		listView.setAdapter(adapter);
		listView.setTextFilterEnabled(true);
		listView.setOnItemClickListener(itemClickListener);
		reload();
		handler.postDelayed(reloadRunnable, RELOAD_INTERVAL);
		getActivity().getWindow().setFlags(
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		super.onStart();
	}
	
	private void reload() {
		File dir = new File(BASE_PATH);
		if (!dir.exists())
			dir.mkdir();
		File files[] = dir.listFiles();
		list.clear();
		for (File file : files) {
			String path = file.getAbsolutePath();
			String name = file.getName();
			if (name.lastIndexOf(".") != -1) {
				String ext = name.substring(name.lastIndexOf("."));
				if(!ext.equals(".tmp")) {
					int icon = ext.equals(".pdf") ? R.drawable.pdf
							: R.drawable.word;
					HashMap<String, Object> item = new HashMap<String, Object>();
					item.put("Icon", icon);
					item.put("FileName", name);
					item.put("FilePath", path);
					list.add(item);
				}
			}
		}
		((BaseAdapter) adapter).notifyDataSetChanged();
	}
	
	private Runnable reloadRunnable = new Runnable() {
		
		@Override
		public void run() {
			reload();
			handler.postDelayed(reloadRunnable, RELOAD_INTERVAL);
		}
	};

	private OnItemClickListener itemClickListener = new OnItemClickListener() {

		@Override
		public void onItemClick(AdapterView<?> arg0, View arg1, int pos,
				long arg3) {
			HashMap<String, Object> map = (HashMap<String, Object>) listView
					.getItemAtPosition(pos);
			String filePath = (String) map.get("FilePath");
			File f1 = new File(filePath);
			Intent it = new Intent(Intent.ACTION_VIEW);
			it.setDataAndType(Uri.fromFile(f1), "application/pdf");
			startActivity(it);
		}
	};
}
