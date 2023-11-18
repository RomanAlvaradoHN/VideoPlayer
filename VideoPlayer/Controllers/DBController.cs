using SQLite;
using VideoPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer.Controllers {
    public class DBController {

        //DATABASE CONFIGURATION VARIABLES
        //=======================================================================================
        private readonly static string dbFileName = "MyAppDB.db3";

        private readonly SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite |
                                                 SQLiteOpenFlags.Create |
                                                 SQLiteOpenFlags.SharedCache;

        private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
        //---------------------------------------------------------------------------------------
        private SQLiteAsyncConnection connection;
        //======================================================================================


        public DBController() {
        }



        private async Task Init() {
            if (connection is not null) {
                return;
            }
            connection = new SQLiteAsyncConnection(dbPath, flags);
            await connection.CreateTableAsync<Datos>();
        }



        //CREATE ==============================================================
        public async Task<int> Insert(Datos registro) {
            await Init();
            return await connection.InsertAsync(registro);
        }




        //READ ==============================================================
        public async Task<List<Datos>> SelectAll() {
            await Init();
            return await connection.Table<Datos>().ToListAsync();
        }


        public async Task<Datos> SelectById(int id) {
            await Init();
            return await connection.Table<Datos>().Where(col => col.Id == id).FirstOrDefaultAsync();
        }




        //UPDATE ==============================================================
        public async Task<int> Update(Datos registro) {
            await Init();
            return await connection.UpdateAsync(registro);
        }




        //DELETE ==============================================================
        public async Task<int> Delete(Datos registro) {
            await Init();
            return await connection.DeleteAsync(registro);
        }
    }
}