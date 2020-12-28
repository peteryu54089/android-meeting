package com.ntut.meetingInterface;

import com.ntut.structure.ScoreSettingDataItem;

public interface Updatable {
	public void sendUpdateMessage();
	public void onUpdate(Object SettingDataItems);
}
