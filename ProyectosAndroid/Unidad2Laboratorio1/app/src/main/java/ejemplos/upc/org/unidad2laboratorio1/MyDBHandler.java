package ejemplos.upc.org.unidad2laboratorio1;

import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.content.Context;
import android.content.ContentValues;
import android.database.Cursor;
import android.util.Log;

import com.estimote.sdk.Beacon;

import java.util.ArrayList;

/**
 * Created by Eduardo on 20/04/2015.
 */

public class MyDBHandler extends SQLiteOpenHelper {

    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "Beacons.db";
    private static final String TABLE_PRODUCTS = "Beacons";

    public static final String COLUMN_ID = "_id";
    public static final String COLUMN_MAC = "mac";
    public static final String COLUMN_RSSI = "rssi";

    public MyDBHandler(Context context, String name,
                       SQLiteDatabase.CursorFactory factory, int version) {
        super(context, DATABASE_NAME, factory, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_PRODUCTS_TABLE = "CREATE TABLE " +
                TABLE_PRODUCTS + "("
                + COLUMN_ID + " INTEGER PRIMARY KEY," + COLUMN_MAC
                + " TEXT," + COLUMN_RSSI + " INTEGER" + ")";
        db.execSQL(CREATE_PRODUCTS_TABLE);

    }


    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion,
                          int newVersion) {

        db.execSQL("DROP TABLE IF EXISTS " + TABLE_PRODUCTS);
        onCreate(db);
    }

    public void addRSSI(Beacon beacon) {

        ContentValues values = new ContentValues();
        values.put(COLUMN_MAC, beacon.getMacAddress());
        values.put(COLUMN_RSSI, beacon.getRssi());

        SQLiteDatabase db = this.getWritableDatabase();

        db.insert(TABLE_PRODUCTS, null, values);
        db.close();
    }

    public void deleteAll(){
        SQLiteDatabase db = this.getWritableDatabase();
        db.execSQL("DELETE from "+ TABLE_PRODUCTS );
    }

    public ArrayList<Beacon> getAllElements() {

        ArrayList<Beacon> list = new ArrayList<Beacon>();

        // Select All Query
        String selectQuery = "SELECT  * FROM " + TABLE_PRODUCTS + " order by "+ COLUMN_MAC;

        SQLiteDatabase db = this.getReadableDatabase();
        try {

            Cursor cursor = db.rawQuery(selectQuery, null);
            try {

                // looping through all rows and adding to list
                if (cursor.moveToFirst()) {
                    do {
                        //Log.i("a ver", "mac: "+  cursor.getString(1)+ " rssi: " + cursor.getString(2));
                        Beacon obj = new Beacon("A1B2C3D4F5G6H7J8K9A10W1234123DWQ","",cursor.getString(1),0,0,0, Integer.parseInt(cursor.getString(2)));


                        //you could add additional columns here..

                        list.add(obj);
                    } while (cursor.moveToNext());
                }

            } finally {
                try { cursor.close(); } catch (Exception ignore) {}
            }

        } finally {
            try { db.close(); } catch (Exception ignore) {}
        }

        return list;
    }

}