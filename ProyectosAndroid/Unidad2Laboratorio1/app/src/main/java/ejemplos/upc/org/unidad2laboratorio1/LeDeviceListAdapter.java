package ejemplos.upc.org.unidad2laboratorio1;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.estimote.sdk.Beacon;
import com.estimote.sdk.Utils;

import java.io.StringBufferInputStream;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

/**
 * Created by Victor Moran on 15/04/2015.
 */
public class LeDeviceListAdapter extends BaseAdapter {

    private MyDBHandler database;
    private ArrayList<Beacon> beacons;
    private LayoutInflater inflater;

    public LeDeviceListAdapter(Context context) {
        this.inflater = LayoutInflater.from(context);
        this.beacons = new ArrayList<Beacon>();
        this.database = new MyDBHandler(context,null,null,1);
    }

    public void replaceWith(Collection<Beacon> newBeacons) {
        this.beacons.clear();
        this.beacons.addAll(newBeacons);
        notifyDataSetChanged();
    }

    @Override
    public int getCount() {
        return beacons.size();
    }

    @Override
    public Beacon getItem(int position) {
        return beacons.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View view, ViewGroup parent) {
        view = inflateIfRequired(view, position, parent);
        bind(getItem(position), view);
        return view;
    }

    private void bind(Beacon beacon, View view) {
        ViewHolder holder = (ViewHolder) view.getTag();
        holder.macTextView.setText(String.format("Name: %s (%.2fm)", beacon.getMacAddress(), Utils.computeAccuracy(beacon)));
        holder.majorTextView.setText("Major: " + beacon.getMajor());
        holder.minorTextView.setText("Minor: " + beacon.getMinor());
        holder.measuredPowerTextView.setText("MPower: " + beacon.getMeasuredPower());
        holder.rssiTextView.setText("RSSI: " + beacon.getRssi());
        holder.distanceTextView.setText("Distance: " + calculateDistance(beacon));
        database.addRSSI(beacon);
        //Log.i("Base de datos", "meto esto en la base: "+  beacon.getMacAddress()+ ""+ String.valueOf(beacon.getRssi()) );
    }

    private View inflateIfRequired(View view, int position, ViewGroup parent) {
        if (view == null) {
            view = inflater.inflate(R.layout.device_item, null);
            view.setTag(new ViewHolder(view));
        }
        return view;
    }

    static class ViewHolder {
        final TextView macTextView;
        final TextView majorTextView;
        final TextView minorTextView;
        final TextView measuredPowerTextView;
        final TextView rssiTextView;
        final TextView distanceTextView;

        ViewHolder(View view) {
            macTextView = (TextView) view.findViewWithTag("mac");
            majorTextView = (TextView) view.findViewWithTag("major");
            minorTextView = (TextView) view.findViewWithTag("minor");
            measuredPowerTextView = (TextView) view.findViewWithTag("mpower");
            rssiTextView = (TextView) view.findViewWithTag("rssi");
            distanceTextView = (TextView) view.findViewWithTag("distance");
        }
    }

    private static double calculateDistance(Beacon beacon) {

        int txPower = beacon.getMeasuredPower();
        int rssi = beacon.getRssi();
        double A,B,C;


        //constant based on Nexus 4
        A = 0.89976;//0.89976
        B = 7.7095; // 7.7095
        C = 0.111; //0.111
        if (rssi == 0) {
            return -1.0; // if we cannot determine distance, return -1.
        }

        double ratio = rssi*1.0/txPower;
        if (ratio < 1.0) {
            return Math.pow(ratio,10);
        }
        else {
            //double accuracy =  (A)*Math.pow(ratio,B) + C;
            //double accuracy = Math.pow(10d,((double)txPower-rssi)/(10*2));
            double accuracy = Math.pow(10d,((double)-62.72-rssi)/(2.2853));
            return accuracy;
        }


    }

    public ArrayList<Beacon> getAllElements()
    {
        return database.getAllElements();
    }
    public void DeleteAllData()
    {
        database.deleteAll();
    }
}
