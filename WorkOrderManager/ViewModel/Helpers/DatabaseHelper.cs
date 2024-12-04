using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManager.ViewModel.Helpers {
    public class DatabaseHelper {


        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "WorkOrderFile.db3");

        public static bool Insert<T>(T item) {

            bool result = false;
            using (SQLiteConnection myDb = new SQLiteConnection(dbFile)) {

                myDb.CreateTable<T>();
                int rows = myDb.Insert(item);
                if (rows > 0) {

                    result = true;
                }
                return result;
            }

        }

        public static bool Update<T>(T item) {

            bool result = false;
            using (SQLiteConnection myDb = new SQLiteConnection(dbFile)) {

                myDb.CreateTable<T>();
                int rows = myDb.Update(item);
                if (rows > 0) {

                    result = true;
                }
                return result;
            }
        }

        public static bool Delete<T>(T item) {

            bool result = false;
            using (SQLiteConnection myDb = new SQLiteConnection(dbFile)) {

                myDb.CreateTable<T>();
                int rows = myDb.Delete(item);
                if (rows > 0) {

                    result = true;
                }
                return result;
            }
        }

        public static List<T> Read<T>() where T : new() {

            List<T> list;
            using (SQLiteConnection myDb = new SQLiteConnection(dbFile)) {

                myDb.CreateTable<T>();
                list = myDb.Table<T>().ToList();
            }
            return list;
        }

    }
}
