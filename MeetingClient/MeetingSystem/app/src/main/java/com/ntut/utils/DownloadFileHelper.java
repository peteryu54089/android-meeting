package com.ntut.utils;

import java.io.File;
import java.io.IOException;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;

import android.os.AsyncTask;
import android.os.Environment;
import android.util.Log;

import com.ntut.structure.FileDataItem;

public class DownloadFileHelper {
	
	ProgressNotify progreesNotify;
	private final static String TAG = "DownloadFileHelper";
	private String getFileName(String filePath) {
		int i = filePath.lastIndexOf('\\')+1;
		return filePath.substring(i);
	}

	public void registerProgress(ProgressNotify notify) {
		progreesNotify = notify;
	}

	public void startDownload(FileDataItem[] files) {
		Log.d(TAG, "startDownload");
		BigInteger totalLength = new BigInteger("0");
		List<HttpDowloader> downloadList=new ArrayList<HttpDowloader>();
		String rootPath = Environment.getExternalStorageDirectory().toString() + "/SessionSystem/";
		File dir = new File(rootPath);
		if(!dir.exists())
			dir.mkdir();
		for (FileDataItem webFile : files) {
			
			String fileNameString = getFileName(webFile.getFilePath()); 
			File loaclFile = new File(dir, fileNameString);
			HttpDowloader httpDownloader = null;
			try {  
				httpDownloader = new HttpDowloader(webFile.getWebPath(),
						loaclFile.getAbsolutePath());
				totalLength=totalLength.add(new BigInteger(Long.toString(httpDownloader
						.getContentLength())));
				httpDownloader.disconnect();
				downloadList.add(httpDownloader);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}  
		}
		Log.d(TAG, "start downloading");
		for(HttpDowloader dowloader:downloadList)
		{
			 //new DownLoadFileAsync(totalLength).executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR, dowloader);
			 new DownLoadFileAsync(totalLength).execute(dowloader);
		}

	}

	class DownLoadFileAsync extends AsyncTask<HttpDowloader, String, String> {
		BigInteger totalLength;
		
		public DownLoadFileAsync(BigInteger totalLength) {
			// TODO Auto-generated constructor stub
			this.totalLength=totalLength;
		} 
		@Override
		protected String doInBackground(HttpDowloader... params) {
			// TODO Auto-generated method stub
			HttpDowloader httpDowloader = params[0];
			
			try { 
				httpDowloader.registerProgressNotify(progreesNotify);
				httpDowloader.startDownload(totalLength);

			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			return null;
		}

	}
}
