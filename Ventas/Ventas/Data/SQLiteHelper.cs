using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Ventas.Models;

namespace Ventas.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Clientes>().Wait();
        }

        public async Task<int> SaveClientesAsync(Clientes client)
        {
            if (client.IdCliente != 0)
            {
                await db.UpdateAsync(client);
                return client.IdCliente;
                //return db.UpdateAsync(client);
            }
            else
            {
                await db.InsertAsync(client);
                return client.IdCliente;
                //return db.InsertAsync(client);
            }
        }

        /// <summary>
        /// Eliminar Un Cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Task<int> DeleteClienteAsync(Clientes client)
        {
            return db.DeleteAsync(client);
        }
        /// <summary>
        /// Recuperar Todos Los Clientes
        /// </summary>
        /// <returns></returns>
        public Task<List<Clientes>> GetClientesAsync()
        {
            return db.Table<Clientes>().ToListAsync();
        }

        /// <summary>
        /// Recupera Clientes por id
        /// </summary>
        /// <param name="idCliente">Id del cliente que se requiere</param>
        /// <returns></returns>
        public Task<Clientes> GetClienteByIdAsync(int idCliente)
        {
            return db.Table<Clientes>().Where(a => a.IdCliente == idCliente).FirstOrDefaultAsync();
        }
    }
}
