using Android.Content;
using Android.Database.Sqlite;
using Android.Provider;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse.DB
{
    public class WarehousesDBHelper : SQLiteOpenHelper
    {
        private const int _databaseVersion = 1;
        private const string _databaseName = "Warehouses.db";
        private const string _tableCreationScript = "CREATE TABLE " + WarehousesDBContract.Warehouse.TableName + " (" +
            IBaseColumns.Id + " INTEGER PRIMARY KEY, " +
            WarehousesDBContract.Warehouse.ProductName + " TEXT, " +
            WarehousesDBContract.Warehouse.Price + " REAL, " +
            WarehousesDBContract.Warehouse.Quantity + " INTEGER)";
        private const string _tableDeletionScript = "DROP TABLE IF EXISTS " + WarehousesDBContract.Warehouse.TableName;
        private const string _getLowQuantityProductsScript = "SELECT " + WarehousesDBContract.Warehouse.ProductName +
                                                            ", " + WarehousesDBContract.Warehouse.Quantity +
                                                            " FROM " + WarehousesDBContract.Warehouse.TableName +
                                                            " WHERE " + WarehousesDBContract.Warehouse.Quantity + " < 5";

        public WarehousesDBHelper(Context context)
            : base(context, _databaseName, null, _databaseVersion)
        {

        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(_tableCreationScript);

            foreach (var product in WarehousesDBContract.Warehouse.Data)
            {
                var values = new ContentValues();
                values.Put(WarehousesDBContract.Warehouse.ProductName, product.ProductName);
                values.Put(WarehousesDBContract.Warehouse.Price, product.Price);
                values.Put(WarehousesDBContract.Warehouse.Quantity, product.Quantity);
                db.Insert(WarehousesDBContract.Warehouse.TableName, null, values);
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(_tableDeletionScript);
            OnCreate(db);
        }
        public List<ProductEntity> GetAllProducts()
        {
            var projection = new string[]
            {
                IBaseColumns.Id,
                WarehousesDBContract.Warehouse.ProductName,
                WarehousesDBContract.Warehouse.Price,
                WarehousesDBContract.Warehouse.Quantity,
            };

            var sortOrder = WarehousesDBContract.Warehouse.ProductName + " ASC";
            var cursor = ReadableDatabase.Query(
                WarehousesDBContract.Warehouse.TableName,
                projection,
                null,
                null,
                null,
                null,
                sortOrder);
            var warehouseList = new List<ProductEntity>();

            while (cursor.MoveToNext())
            {
                warehouseList.Add(new ProductEntity
                {
                    Id = cursor.GetInt(cursor.GetColumnIndexOrThrow(IBaseColumns.Id)),
                    ProductName = cursor.GetString(cursor.GetColumnIndexOrThrow(WarehousesDBContract.Warehouse.ProductName)),
                    Price = cursor.GetDouble(cursor.GetColumnIndexOrThrow(WarehousesDBContract.Warehouse.Price)),
                    Quantity = cursor.GetInt(cursor.GetColumnIndexOrThrow(WarehousesDBContract.Warehouse.Quantity)),
                });
            }
            cursor.Close();
            return warehouseList;
        }
        public override void OnDowngrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            OnUpgrade(db, oldVersion, newVersion);
        }

        public List<ProductEntity> GetLowQuantityProducts()
        {
            var projection = new string[]
            {
                IBaseColumns.Id,
                WarehousesDBContract.Warehouse.ProductName,
                WarehousesDBContract.Warehouse.Price,
                WarehousesDBContract.Warehouse.Quantity,
            };

            var selection = WarehousesDBContract.Warehouse.Quantity + " < ?";
            var selectionArgs = new string[]
            {
                "5",
            };

            var sortOrder = WarehousesDBContract.Warehouse.ProductName + " ASC";
            var cursor = ReadableDatabase.Query(
                WarehousesDBContract.Warehouse.TableName,
                projection,
                selection,
                selectionArgs,
                null,
                null,
                sortOrder);
            var productList = new List<ProductEntity>();

            while (cursor.MoveToNext())
            {
                productList.Add(new ProductEntity
                {
                    Id = cursor.GetInt(cursor.GetColumnIndexOrThrow(IBaseColumns.Id)),
                    ProductName = cursor.GetString(cursor.GetColumnIndexOrThrow(WarehousesDBContract.Warehouse.ProductName)),
                    Price = cursor.GetDouble(cursor.GetColumnIndexOrThrow(WarehousesDBContract.Warehouse.Price)),
                    Quantity = cursor.GetInt(cursor.GetColumnIndexOrThrow(WarehousesDBContract.Warehouse.Quantity)),
                });
            }
            cursor.Close();
            return productList;
        }
    }
}