package upc.org.demobeacons;

import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import com.estimote.sdk.Beacon;

import java.util.ArrayList;
import java.util.Collection;

/**
 * Created by Victor Moran on 13/04/2015.
 */
public class ListAdapter extends BaseAdapter {

    private ArrayList<Beacon> beacons;

    public ListAdapter(){
        this.beacons = new ArrayList<Beacon>();
    }

    public void replaceWith(Collection<Beacon> newBeacons) {
        this.beacons.clear();
        this.beacons.addAll(newBeacons);
        notifyDataSetChanged();
    }

    public void addBeacon(Beacon beacon){

        beacons.add(beacon);
    }
    @Override
    public int getCount() {
        return beacons.size();
    }

    @Override
    public Object getItem(int position) {
        return beacons.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        return null;
    }
}
