package com.ntut.utils;

//import com.android.ntut.meeting.element.ScoreMapItem;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonSyntaxException;

public class GsonUtils {
	private static Gson gson = null;
	private static boolean isInit = false;

	static {
		if (!isInit) {
			GsonBuilder builder = new GsonBuilder();
			builder.setFieldNamingPolicy(FieldNamingPolicy.UPPER_CAMEL_CASE);
			gson = builder.create();
		}
		isInit = true;
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public static Object JsonToObject(String jsonStr, Class targetClass) {
		jsonStr = jsonStr.replace("\\", "\\\\");
		try {
			Object object = gson.fromJson(jsonStr, targetClass);
			return object;
		} catch (JsonSyntaxException e) {
			return null;
		}
	}

	public static String ObjectToJson(Object obj) {
		return gson.toJson(obj);
	}

	/*
	 * public static ScoreMapItem JsonToScoreMapItem(String jsonStr) { jsonStr =
	 * jsonStr.replace("\\", "\\\\"); return gson.fromJson(jsonStr,
	 * ScoreMapItem.class); }
	 * 
	 * public static ScoreMapItem[] JsonToScoreMapItemList(String jsonStr) {
	 * jsonStr = jsonStr.replace("\\", "\\\\"); return gson.fromJson(jsonStr,
	 * ScoreMapItem[].class); }
	 */
}
