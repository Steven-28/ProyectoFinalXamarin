using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Ventas.Models
{
    public class Clientes
    {
        [PrimaryKey, AutoIncrement]
        public int IdCliente { get; set; }
        [MaxLength(60)]
        public string NombreCliente { get; set; }
        [MaxLength(50)]
        public string ApellidoCliente { get; set;}
    }
}
