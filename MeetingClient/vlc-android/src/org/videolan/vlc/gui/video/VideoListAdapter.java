/*****************************************************************************
 * VideoListAdapter.java
 *****************************************************************************
 * Copyright © 2011-2012 VLC authors and VideoLAN
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

package org.videolan.vlc.gui.video;

import java.util.Comparator;
import java.util.HashMap;

import org.videolan.vlc.Media;
import org.videolan.vlc.R;
import org.videolan.vlc.Util;

import android.content.Context;
import android.content.res.ColorStateList;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class VideoListAdapter extends ArrayAdapter<Media>
                                 implements Comparator<Media> {

    public final static int SORT_BY_TITLE = 0;
    public final static int SORT_BY_LENGTH = 1;
    private int mSortDirection = 1;
    private int mSortBy = SORT_BY_TITLE;
    private String mLastMRL;

    public VideoListAdapter(Context context) {
        super(context, 0);
    }

    public final static String TAG = "VLC/MediaLibraryAdapter";

    public synchronized void update(Media item) {
        int position = getPosition(item);
        if (position != -1) {
            remove(item);
            insert(item, position);
        }
    }

    public void setLastMedia(String lastMRL, HashMap<String, Long> times) {
        mLastMRL = lastMRL;
        // update times
        for (int i = 0; i < getCount(); ++i) {
            Media media = getItem(i);
            Long time = times.get(media.getLocation());
            if (time != null)
                media.setTime(time);
        }
    }

    public void sortBy(int sortby) {
        switch (sortby) {
            case SORT_BY_TITLE:
                if (mSortBy == SORT_BY_TITLE)
                    mSortDirection *= -1;
                else {
                    mSortBy = SORT_BY_TITLE;
                    mSortDirection = 1;
                }
                break;
            case SORT_BY_LENGTH:
                if (mSortBy == SORT_BY_LENGTH)
                    mSortDirection *= -1;
                else {
                    mSortBy = SORT_BY_LENGTH;
                    mSortDirection *= 1;
                }
                break;
            default:
                mSortBy = SORT_BY_TITLE;
                mSortDirection = 1;
                break;
        }
        sort();
    }

    public void sort() {
        super.sort(this);
    }

    @Override
    public int compare(Media item1, Media item2) {
        int compare = 0;
        switch (mSortBy) {
            case SORT_BY_TITLE:
                compare = item1.getTitle().toUpperCase().compareTo(
                        item2.getTitle().toUpperCase());
                break;
            case SORT_BY_LENGTH:
                compare = ((Long) item1.getLength()).compareTo(item2.getLength());
                break;
        }
        return mSortDirection * compare;
    }

    /**
     * Display the view of a file browser item.
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        ViewHolder holder;
        View v = convertView;
        if (v == null) {
            LayoutInflater inflater = (LayoutInflater) getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            v = inflater.inflate(R.layout.video_list_item, parent, false);
            holder = new ViewHolder();
            holder.layout = v.findViewById(R.id.layout_item);
            holder.thumbnail = (ImageView) v.findViewById(R.id.ml_item_thumbnail);
            holder.title = (TextView) v.findViewById(R.id.ml_item_title);
            holder.subtitle = (TextView) v.findViewById(R.id.ml_item_subtitle);
            v.setTag(holder);
        } else
            holder = (ViewHolder) v.getTag();

        Media media = getItem(position);
        Util.setItemBackground(holder.layout, position);
        holder.title.setText(media.getTitle());

        Bitmap thumbnail;
        if (media.getPicture() != null) {
            thumbnail = media.getPicture();
            holder.thumbnail.setImageBitmap(thumbnail);
        } else {
            // set default thumbnail
            thumbnail = BitmapFactory.decodeResource(v.getResources(), R.drawable.thumbnail);
            holder.thumbnail.setImageBitmap(thumbnail);
        }

        ColorStateList titleColor = v.getResources().getColorStateList(media.getLocation().equals(mLastMRL)
                ? R.color.list_title_last
                : R.color.list_title);
        holder.title.setTextColor(titleColor);

        long lastTime = media.getTime();
        String text;
        if (lastTime > 0) {
            text = String.format("%s / %s",
                    Util.millisToString(lastTime),
                    Util.millisToString(media.getLength()));
        }
        else {
            text = String.format("%s",
                    Util.millisToString(media.getLength()));
        }
        
        if (media.getWidth() > 0 && media.getHeight() > 0) {
            text += String.format(" - %dx%d", media.getWidth(), media.getHeight());
        }

        holder.subtitle.setText(text);

        return v;
    }

    static class ViewHolder {
        View layout;
        ImageView thumbnail;
        TextView title;
        TextView subtitle;
        ImageView more;
    }
}
