package com.ntut.message;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.File;
import java.io.IOException;

import com.ntut.message.MessagePool.ClientMessage;

import android.content.Context;
import android.os.Environment;
import android.util.Log;

public class CloseMessageHandler implements IMessage {
	private final static String TAG = "CloseMessageHandler";
	public static void init()
    {
        MessagePool.registerMessage(ClientMessage.Close, new CloseMessageHandler());
    }
	
	@Override
	public void sendMessage(DataOutput dataOutput, Object obj)
			throws IOException {
		// TODO Auto-generated method stub

	}

	@Override
	public void handlerMessage(Context context, Object obj) {
		// TODO Auto-generated method stub

	}

	@Override
	public Object receivedMessage(DataInput dataInput) throws IOException {
		// TODO Auto-generated method stub
		Log.d(TAG, "receivedMessage");
	    String rootPath = Environment.getExternalStorageDirectory().toString() + "/SessionSystem/";
	    File dir = new File(rootPath);
        if(dir.exists())
        {
        	 File files[] = dir.listFiles();
        	 for(File file:files)
        	 {
        		 file.delete();
        	 }
        }
		return null;
	}

}
