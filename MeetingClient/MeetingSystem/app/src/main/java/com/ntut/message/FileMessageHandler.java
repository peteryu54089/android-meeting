package com.ntut.message;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;
import java.math.BigInteger;
import java.util.Random;

import android.app.Notification;
import android.app.NotificationManager;
import android.content.Context;
import android.util.Log;
import android.widget.RemoteViews;

import com.ntut.client.MeetingInputStream;
import com.ntut.downloader.DownloadTask;
import com.ntut.mainactivity.R;
import com.ntut.message.MessagePool.ClientMessage;
import com.ntut.structure.FileDataItem;
import com.ntut.utils.DownloadFileHelper;
import com.ntut.utils.GsonUtils;
import com.ntut.utils.ProgressNotify;

public class FileMessageHandler implements IMessage {
	private String TAG = "FileMessageHandler";
//	private NotificationManager nm;
//	private Notification notification;
//	private int notificationID = new Random().nextInt(999999);
	
	public static void init() {
		MessagePool.registerMessage(ClientMessage.FileMessage,
				new FileMessageHandler());
	}

	@Override
	public void sendMessage(DataOutput dataOutput, Object obj)
			throws IOException {
		// TODO Auto-generated method stub

	}

	@Override
	public void handlerMessage(Context context, Object obj) {
		// TODO Auto-generated method stub
//		nm = (NotificationManager) context
//				.getSystemService(Context.NOTIFICATION_SERVICE);
//		notification = new Notification();		
//		notification.icon = R.drawable.ic_launcher;
//		notification.tickerText = "File is downloading";
//		notification.contentView = new RemoteViews(context.getPackageName(),
//				R.layout.notification_msg);
		startDownload(context,obj);
	}
	@Override
	public Object receivedMessage(DataInput dataInput) throws IOException {
		// TODO Auto-generated method stub
		int i = 0;
		String receiveString = ((MeetingInputStream)dataInput).getMsg();
		Log.d(TAG, receiveString + "");
		FileDataItem[] filePaths = (FileDataItem[]) GsonUtils.JsonToObject(
				receiveString, FileDataItem[].class);
		Log.d(TAG, Integer.toString(filePaths.length));
		return filePaths;
	}
	private void startDownload(Context context,Object obj) {
		Log.d(TAG, "startDownload");
		DownloadTask downloadTask = new DownloadTask(context);
		downloadTask.execute((FileDataItem[]) obj);
		/*DownloadFileHelper fileHelper=new DownloadFileHelper();
		downloaded = new BigInteger("0");
		if (obj != null) {
			FileDataItem[] filePaths = (FileDataItem[]) obj;
			fileHelper.registerProgress(this);
			fileHelper.startDownload(filePaths);
		}*/
	}
//	private void showNotification() {
//		// TODO Auto-generated method stub
//		nm.notify(notificationID, notification);
//	}

//	private BigInteger downloaded = new BigInteger("0");
//	int lastDownload = 0;
//	@Override
//	public synchronized void notifyProgressValue(int bufferLen, BigInteger totalLength) {
//		// TODO Auto-generated method stub
//		downloaded = downloaded.add(new BigInteger(Integer.toString(bufferLen)));
//		Log.d("progreesNotify",
//				String.format("Current : %s    Total : %s",
//						downloaded.toString(), totalLength.toString()));
//		if (totalLength.compareTo(new BigInteger("0")) == 0)
//			return;
//		int currentValue = Integer.parseInt(downloaded
//				.multiply(new BigInteger("100")).divide(totalLength)
//				.toString());
//		if (lastDownload != currentValue) {
//			notification.contentView.setProgressBar(R.id.pb, 100,
//					currentValue, false);
//			notification.contentView.setTextViewText(R.id.down_tv, downloaded
//					.multiply(new BigInteger("100")).divide(totalLength)
//					.intValue()
//					+ "%");
//			showNotification(); 
//		}
//		if (downloaded.compareTo(totalLength) == 0)
//			nm.cancel(notificationID);
//		lastDownload = currentValue;
//	}

}
