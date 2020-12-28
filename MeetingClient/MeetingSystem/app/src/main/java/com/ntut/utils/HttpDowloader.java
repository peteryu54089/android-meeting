package com.ntut.utils;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.math.BigInteger;
import java.net.HttpURLConnection;
import java.net.ProtocolException;
import java.net.URL;

public class HttpDowloader {
	private String filePathString = "";
	private String urlString;
	ProgressNotify progressnotify;
	HttpURLConnection urlConnection = null;
		
	public HttpDowloader(String urlString, String filePathString)
			throws IOException {
		this.filePathString = filePathString;
		this.urlString=urlString;
		connect();
	}

	public void connect() throws IOException, ProtocolException {
		URL url = new URL(urlString);
		urlConnection = (HttpURLConnection) url.openConnection();
		urlConnection.setRequestMethod("GET"); 
		urlConnection.connect();
	}
	
	public void disconnect() {
		urlConnection.disconnect();
		urlConnection = null;
	}

	public void registerProgressNotify(ProgressNotify notify) {
		progressnotify = notify;
	}

	private void notifyProgressChange(int current, BigInteger totalLength) {
		progressnotify.notifyProgressValue(current, totalLength);
	}

	public int getContentLength() {
		if (urlConnection != null)
			return urlConnection.getContentLength();
		else {
			return -1;
		}
	}

	public void startDownload(BigInteger totalLength) throws IOException {
		File file = new File(filePathString);
		FileOutputStream fileOutput = new FileOutputStream(file);
		if(urlConnection== null)
			connect();
		InputStream inputStream = urlConnection.getInputStream();
		int downloadedSize = 0;
		byte[] buffer = new byte[1024 * 1024];
		int bufferLength = 0; // used to store a temporary size of the buffer

		while (downloadedSize < getContentLength()) {
			while ((bufferLength = inputStream.read(buffer)) > 0) {
				fileOutput.write(buffer, 0, bufferLength);
				downloadedSize += bufferLength;
				notifyProgressChange(bufferLength, totalLength);
			}
		}
		inputStream.close();
		fileOutput.close();
		disconnect();
	}
}
