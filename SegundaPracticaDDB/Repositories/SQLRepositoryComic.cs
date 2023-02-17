using SegundaPracticaDDB.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#region PROCEDURES
//CREATE PROCEDURE SP_INSERT_COMIC
//(@NOMBRE nvarchar(150), @IMAGEN NVARCHAR(600), @DESCRIPCION NVARCHAR(500))
//AS
//    DECLARE @IDCOM INT;
//SET @IDCOM = (SELECT MAX(IDCOMIC)+1 FROM COMICS)

//	INSERT INTO COMICS VALUES(@IDCOM, @NOMBRE, @IMAGEN,
//    @DESCRIPCION)
//GO
#endregion

namespace SegundaPracticaDDB.Repositories {
    public class SQLRepositoryComic : IRepositoryComic {

        private SqlConnection cn;
        private SqlCommand com;
        private DataTable tableComic;


        public SQLRepositoryComic() {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2022";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;

            string sql = "SELECT * FROM COMICS";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            this.tableComic = new DataTable();
            adapter.Fill(this.tableComic);

        }

        public void CreateComic(string nombre, string imagen, string descripcion) {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";

            SqlParameter pamNombre = new SqlParameter("@NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);

            SqlParameter pamImagen = new SqlParameter("@IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);

            SqlParameter pamDescripcion = new SqlParameter("@DESCRIPCION", descripcion);
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
