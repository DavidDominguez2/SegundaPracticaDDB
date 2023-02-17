using Oracle.ManagedDataAccess.Client;
using SegundaPracticaDDB.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

#region PROCEDURES
//CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
//(P_NOMBRE COMICS.NOMBRE%TYPE,
//P_IMAGEN COMICS.IMAGEN%TYPE,
//P_DESCRIPCION COMICS.DESCRIPCION%TYPE
//)
//AS
//  P_IDCOMIC INT;
//BEGIN
//SELECT MAX(IDCOMIC)+1 INTO P_IDCOMIC FROM COMICS;

//INSERT INTO COMICS VALUES(P_IDCOMIC, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
//COMMIT;
//END;
#endregion

namespace SegundaPracticaDDB.Repositories {
    public class OCLRepositoryComic : IRepositoryComic {

        OracleConnection cn;
        OracleCommand com;
        DataTable tableComic;


        public OCLRepositoryComic() {
            string connectionString =
                @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;

            string sql = "SELECT * FROM COMICS";
            OracleDataAdapter adapter = new OracleDataAdapter(sql, connectionString);
            this.tableComic = new DataTable();
            adapter.Fill(this.tableComic);

        }

        public void CreateComic(string nombre, string imagen, string descripcion) {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";

            OracleParameter pamNombre = new OracleParameter(":NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);

            OracleParameter pamImagen = new OracleParameter(":IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);

            OracleParameter pamDescripcion = new OracleParameter(":DESCRIPCION", descripcion);
            this.com.Parameters.Add(pamDescripcion);

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }

        public List<Comic> GetComics() {
            var consulta = from data in this.tableComic.AsEnumerable()
                           select new Comic {
                               IdComic = data.Field<int>("IDCOMIC"),
                               Nombre = data.Field<string>("NOMBRE"),
                               Imagen = data.Field<string>("IMAGEN"),
                               Descripcion = data.Field<string>("DESCRIPCION")
                           };
            return consulta.ToList();
        }
    }
}

