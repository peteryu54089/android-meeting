package com.ntut.structure;

import java.io.Serializable;

public class ServerInfo implements Serializable{
	private String name;
	private String ip;
	private int broadcastPort;
	private int servicePort;
	private int screenServicePort;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}

	public int getBroadcastPort() {
		return broadcastPort;
	}

	public void setBroadcastPort(int broadcastPort) {
		this.broadcastPort = broadcastPort;
	}

	public int getServicePort() {
		return servicePort;
	}

	public void setServicePort(int servicePort) {
		this.servicePort = servicePort;
	}

	public int getScreenServicePort() {
		return screenServicePort;
	}

	public void setScreenServicePort(int screenServicePort) {
		this.screenServicePort = screenServicePort;
	}

	@Override
	public String toString() {
		return String
				.format("[%s,%s] : BroadcastPort(%d) ServicePort(%d) ScreenServicePort(%d)",
						name, ip, broadcastPort, servicePort, screenServicePort);
	}
}
