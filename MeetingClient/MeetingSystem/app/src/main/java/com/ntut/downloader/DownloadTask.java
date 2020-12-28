package com.ntut.downloader;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.math.BigInteger;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Random;

import com.ntut.mainactivity.R;
import com.ntut.structure.FileDataItem;

import android.app.Notification;
import android.app.NotificationManager;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Environment;
import android.os.PowerManager;
import android.text.StaticLayout;
import android.util.Log;
import android.widget.RemoteViews;

public class DownloadTask extends AsyncTask<FileDataItem, BigInteger, String> {
	private final String TAG = "DownloadTask";
	private final String BASE_PATH = Environment.getExternalStorageDirectory().toString() + "/SessionSystem/";
	private final int notificationID = new Random().nextInt(999999);
	private Context context = null;
	private NotificationManager notificationManager = null;
	private Notification notification = null;
	private BigInteger oldProgress = new BigInteger("-1");
	private String curFileName = "";
	private String tmpFileName = "";
	
	public DownloadTask(Context context) {
		this.context = context;
		notificationManager = (NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);
		notification = new Notification();		
		notification.icon = R.drawable.ic_launcher;
		notification.tickerText = "File is downloading";
		notification.contentView = new RemoteViews(context.getPackageName(),R.layout.notification_msg);
	}

	@Override
	protected String doInBackground(FileDataItem... fileDatas) {
		// take CPU lock to prevent CPU from going off if the user
		// presses the power button during download
		PowerManager pm = (PowerManager) context
				.getSystemService(Context.POWER_SERVICE);
		PowerManager.WakeLock wl = pm.newWakeLock(
				PowerManager.PARTIAL_WAKE_LOCK, getClass().getName());
		if(fileDatas != null && fileDatas.length > 0) {
			for (FileDataItem fileData : fileDatas) {
				wl.acquire();
				try {
					InputStream input = null;
					OutputStream output = null;
					HttpURLConnection connection = null;
					try {
						String webUrl = fileData.getWebPath();
						Log.d(TAG, webUrl);
						
						URL url = new URL(webUrl);
						connection = (HttpURLConnection) url.openConnection();
						connection.connect();
	
						// expect HTTP 200 OK, so we don't mistakenly save error
						// report
						// instead of the file
	//					if (connection.getResponseCode() != HttpURLConnection.HTTP_OK)
	//						return "Server returned HTTP "
	//								+ connection.getResponseCode() + " "
	//								+ connection.getResponseMessage();
						
						
						if (connection.getResponseCode() == HttpURLConnection.HTTP_OK) {
							// this will be useful to display download percentage
							// might be -1: server did not report the length
							String fileLengthStr = Integer.toString(connection.getContentLength());
							BigInteger fileLength = new BigInteger(fileLengthStr);
							curFileName = getFileName(fileData.getFilePath());
							tmpFileName = curFileName + ".tmp";
							
							// download the file
							File outFile = createFile(BASE_PATH, tmpFileName);
							input = connection.getInputStream();
							output = new FileOutputStream(outFile);
							
							byte data[] = new byte[4096];
							BigInteger total = new BigInteger("0");
							int count;
							while ((count = input.read(data)) != -1) {
								// allow canceling with back button
								if (isCancelled())
									return null;
								String countStr = Integer.toString(count);
								total = total.add(new BigInteger(countStr));
								// publishing the progress....
								if (fileLength.compareTo(new BigInteger("0")) == 1) // only if total length is known
									publishProgress((total.multiply(new BigInteger("100")).divide(fileLength)));
								output.write(data, 0, count);
							}
							File targetFile = createFile(BASE_PATH, curFileName);
							outFile.renameTo(targetFile);
						}
					} catch (Exception e) {
						return e.toString();
					} finally {
						try {
							if (output != null)
								output.close();
							if (input != null)
								input.close();
						} catch (IOException ignored) {
						}
	
						if (connection != null)
							connection.disconnect();
					}
				} finally {
					wl.release();
				}
			}
		}
		return null;
	}

	@Override
	protected void onPostExecute(String result) {
		Log.d(TAG, "onPostExecute");
		Log.d(TAG, (result == null)? "null" : result);
		notificationManager.cancel(notificationID);
		super.onPostExecute(result);
	}

	@Override
	protected void onPreExecute() {
		Log.d(TAG, "onPreExecute");
		super.onPreExecute();
	}

	@Override
	protected void onProgressUpdate(BigInteger... values) {
//		Log.d(TAG, "onProgressUpdate");
		if(oldProgress.compareTo(values[0]) != 0) {
			if(values != null && values.length > 0) {
				int progress = values[0].intValue();
				String progresStr = progress + "%";
				Log.d(TAG, progresStr);
				notification.contentView.setProgressBar(R.id.pb, 100, progress, false);
				notification.contentView.setTextViewText(R.id.down_title, curFileName);
				notification.contentView.setTextViewText(R.id.down_tv, progresStr);
				notificationManager.notify(notificationID, notification);
			}
			oldProgress = values[0];
		}
		super.onProgressUpdate(values);
	}

	private String getFileName(String filePath) {
		int i = filePath.lastIndexOf('\\')+1;
		return filePath.substring(i);
	}
	
	private File createFile(String base, String fileName) {
		File baseDir = new File(BASE_PATH);
		if(!baseDir.exists())
			baseDir.mkdir();
		return new File(baseDir, fileName);
	}
}
