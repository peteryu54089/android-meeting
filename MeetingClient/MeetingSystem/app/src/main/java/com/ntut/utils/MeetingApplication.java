/*****************************************************************************
 * VLCApplication.java
 *****************************************************************************
 * Copyright 穢 2010-2012 VLC authors and VideoLAN
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston MA 02110-1301, USA.
 *****************************************************************************/
package com.ntut.utils;

import java.util.Locale;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.preference.PreferenceManager;

public class MeetingApplication extends Application {
	private static MeetingApplication instance;
	private static boolean isMeetingMainVisible = false;
	private static boolean isMeetingControlVisible = false;

	@Override
	public void onCreate() {
		super.onCreate();

		// Are we using advanced debugging - locale?
		SharedPreferences pref = PreferenceManager
				.getDefaultSharedPreferences(this);
		String p = pref.getString("set_locale", "");
		if (p != null && !p.equals("")) {
			Locale locale;
			// workaround due to region code
			if (p.startsWith("zh")) {
				locale = Locale.CHINA;
			} else {
				locale = new Locale(p);
			}
			Locale.setDefault(locale);
			Configuration config = new Configuration();
			config.locale = locale;
			getBaseContext().getResources().updateConfiguration(config,
					getBaseContext().getResources().getDisplayMetrics());
		}

		instance = this;
	}

	public static boolean isMeetingMainVisible() {
		return isMeetingMainVisible;
	}

	public static void meetingMainResumed() {
		isMeetingMainVisible = true;
	}

	public static void meetingMainPaused() {
		isMeetingMainVisible = false;
	}
	
	public static boolean isMeetingControlVisible() {
		return isMeetingMainVisible;
	}

	public static void meetingControlResumed() {
		isMeetingControlVisible = true;
	}

	public static void meetingControlPaused() {
		isMeetingControlVisible = false;
	}

	/**
	 * @return the main context of the Application
	 */
	public static Context getAppContext() {
		return instance;
	}

	/**
	 * @return the main resources from the Application
	 */
	public static Resources getAppResources() {
		return instance.getResources();
	}
}
