/*********************************************************************
 * Copyright 2020 Pablo Ugalde
 * Universidad Estatal A Distancia
 * PRIMER CUATRI-2020 00830 PROGRAMACION AVANZADA
 * 
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    public class DataLayer
    {
        #region "Definicion de variables"
        public static SqlConnection sqlConnData = new SqlConnection(); //connector a la base de datos
        public string _LatestError = ""; //campo publico para mostrar mensajes de la clase
        public static string _ServerDataSource = "BINARYCRCD2"; //campo para el datasourse usado en la coneccion a la base de datos
        public static string _ServerInitialCatalog = "TransportesCR3"; //campo para el catalogo/base de datos usado en la coneccion
        //String de coneccion a la base de datos con los valores por defecto
        public static string _DBConnectionString = "Data Source=" + _ServerDataSource + ";Initial Catalog=" + _ServerInitialCatalog + ";Trusted_Connection=yes;";
        #endregion //Definicion de variables

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DataLayer() { }
        /// <summary>
        /// Constructor de la clase con los valores de datasourse y catalogo
        /// </summary>
        /// <param name="ServerDataSource"></param>
        /// <param name="ServerInitialCatalog"></param>
        public DataLayer(string ServerDataSource, string ServerInitialCatalog)
        {
            //Asignamos los valores a los campos
            _ServerDataSource = ServerDataSource;
            _ServerInitialCatalog = ServerInitialCatalog;
            _DBConnectionString = "Data Source=" + _ServerDataSource + ";Initial Catalog=" + _ServerInitialCatalog + ";Trusted_Connection=yes;";
        }

        #region "Connection open and close"
        /// <summary>
        /// Metodo para abrir la coneccion a la base de datos
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        public bool OpenData(string strType)
        {
            SqlCommand commandData = new SqlCommand();
            try
            {
                //si el tipo de comando es un store procedure
                if (strType == "sp") { commandData.CommandType = CommandType.StoredProcedure; }
                //sino es un query
                else { commandData.CommandType = CommandType.Text; }
                //asignamos el string de coneccion al comando de coneccion
                sqlConnData.ConnectionString = _DBConnectionString;
                commandData.Connection = sqlConnData;
                //si la coneccion no esta abierta entonces la abrimos
                if (sqlConnData.State != ConnectionState.Open) { sqlConnData.Open(); }
                // si la coneccion esta abierta entonces retornamos el valor de true
                if (sqlConnData.State == ConnectionState.Open) { return true; }
                // si no se pudo abrir o no esta abierta retornamos el valor de false
                else { return false; }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                return false; //retornamos el valor de false
            }
        }
        /// <summary>
        /// Metodo para cerrar la coneccion a la base de datos
        /// </summary>
        public void CloseData()
        {
            try
            {
                sqlConnData.Close();
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
            }
        }
        /// <summary>
        /// Metodo para probar la coneccion a la base de datos
        /// </summary>
        /// <returns></returns>
        public bool testData()
        {
            //por defecto el valor retornado seria false
            bool result = false;
            try
            {
                CloseData(); //cerramos la coneccion
                if (OpenData("query")) //tratatomos de abrir la coneccion
                {
                    CloseData(); // si se logro abrir entonces la cerramos
                    result = true; //retornamos el valor de true
                }
                else
                    result = false; //si no se logro abrir retornamos el valor false
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                result = false; // retornamos el valor de false
            }
            return result;
        }
        #endregion //"Connection open and close"

        #region "Conductor"
        /// <summary>
        /// Metodo para validar que exista un conductor
        /// </summary>
        /// <param name="prmIdentificacion"></param>
        /// <returns></returns>
        public bool ExisteConductor(string prmIdentificacion)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT * FROM [Conductor] with(nolock) " +
                    "where Identificacion = @Identificacion", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = prmIdentificacion.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    return true; // retornamos el valor de true
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    return false; // retornamos el valor de false
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                return false; //retornamos el valor false
            }

        }
        /// <summary>
        /// Metodo para guardar la clase conductor en la base de datos
        /// </summary>
        /// <param name="conductor"></param>
        /// <returns></returns>
        public bool GuardaConductor(Conductor conductor)
        {
            SqlCommand commandData = new SqlCommand();
            try
            {
                //query para insertar condutor
                commandData = new System.Data.SqlClient.SqlCommand("INSERT INTO [Conductor]([Identificacion],[Nombre]" +
                    ",[PrimerApeliido],[SegundoApellido],[RutaAsignada])" +
                    "VALUES(@Identificacion,@Nombre,@PrimerApellido,@SegundoApellido,@RutaAsignada)", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = conductor.Identificacion.Trim();
                commandData.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Nombre"].Value = conductor.Nombre.Trim();
                commandData.Parameters.Add("@PrimerApellido", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@PrimerApellido"].Value = conductor.PApellido.Trim();
                commandData.Parameters.Add("@SegundoApellido", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@SegundoApellido"].Value = conductor.SApellido.Trim();
                commandData.Parameters.Add("@RutaAsignada", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@RutaAsignada"].Value = conductor.Ruta.Trim();

                if (ExisteConductor(conductor.Identificacion) == false) //buscamos si existe el conductor
                {
                    OpenData("query"); //abrimos la base de datos
                    commandData.ExecuteNonQuery(); //ejecutamos el query en la base de datos con todos los parametros
                    CloseData();// cerramos la coneccion a la base de datos
                    _LatestError = "Conductor " + conductor.Identificacion.Trim() + " agregado satisfactoriamente.";
                    return true; //retornamos el valor de true si todo salio bien
                }
                else
                {
                    _LatestError = "Conductor ya existe";
                    return false; //retornamos el valor de false si el conductor ya existe
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la coneccion a la base de datos
                return false; // retornamos el valor de false si hay algun error
            }
        }

        public string UsuarioAcceso(Conductor conductor)
        {
            string retorno = "";
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT * FROM [Conductor] with(nolock) " +
                    "where UserName = @UserName and Acceso='CONDUCTOR'", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@UserName"].Value = conductor.UserName.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    retorno = "OKUsuarioAcceso"; // retornamos el valor de true
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    retorno = "DenegadoUsuarioAcceso"; // retornamos el valor de false
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                retorno = "Error"; //retornamos el valor false
            }
            return retorno; //retornamos el valor de true si todo salio bien
        }
        public bool ExisteUsuario(string UserName)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT * FROM [Conductor] with(nolock) " +
                    "where UserName = @UserName", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@UserName"].Value = UserName.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    return true; // retornamos el valor de true
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    return false; // retornamos el valor de false
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                return false; //retornamos el valor false
            }
        }

        public bool ConductorActivo(string Identificacion)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("select * from viaje " +
                    " where Identificacion = @Identificacion and Estado<> 'ACTIVO' ", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = Identificacion.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    return true; // retornamos el valor de true
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    return false; // retornamos el valor de false
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                return false; //retornamos el valor false
            }
        }
        public string RegistrarConductorCamion(Conductor conductor)
        {
            string retorno = "";
            SqlCommand commandData = new SqlCommand();
            try
            {
                if (ExisteConductor(conductor.Identificacion) == true) //buscamos si existe el conductor
                {
                    retorno = "Conductor ya existe!";
                }
                else if (ExisteUsuario(conductor.UserName) == true) //buscamos si existe el conductor
                {
                    retorno = "Usuario ya existe!";
                }
                else if (ExisteCamion(conductor.Placa) == true) //buscamos si existe el conductor
                {
                    retorno = "Camion ya existe!";
                }
                else if(retorno=="")
                {
                    //query para insertar condutor
                    string strquery = "INSERT INTO [Conductor]([Identificacion],[Nombre]" +
                        ",[PrimerApeliido],[SegundoApellido],[RutaAsignada],UserName, acceso)" +
                        "VALUES(@Identificacion,@Nombre,@PrimerApellido,@SegundoApellido,@RutaAsignada,@UserName,@Acceso);" +
                        "INSERT INTO [Camion]([Placa],[AnnoModelo],[Marca])" +
                    "VALUES(@Placa,@AnnoModelo,@Marca)";
                    commandData = new System.Data.SqlClient.SqlCommand(strquery, sqlConnData);
                    commandData.CommandType = CommandType.Text;
                    //agregamos los parametros al query con el valor requerido
                    commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                    commandData.Parameters["@Identificacion"].Value = conductor.Identificacion.Trim();
                    commandData.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Nombre"].Value = conductor.Nombre.Trim();
                    commandData.Parameters.Add("@PrimerApellido", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@PrimerApellido"].Value = conductor.PApellido.Trim();
                    commandData.Parameters.Add("@SegundoApellido", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@SegundoApellido"].Value = conductor.SApellido.Trim();
                    commandData.Parameters.Add("@RutaAsignada", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@RutaAsignada"].Value = conductor.Ruta.Trim();

                    commandData.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@UserName"].Value = conductor.UserName.Trim();
                    commandData.Parameters.Add("@Acceso", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Acceso"].Value = conductor.Acceso.Trim();

                    commandData.Parameters.Add("@Placa", System.Data.SqlDbType.VarChar, 8);
                    commandData.Parameters["@Placa"].Value = conductor.Placa.Trim();
                    commandData.Parameters.Add("@AnnoModelo", System.Data.SqlDbType.VarChar, 4);
                    commandData.Parameters["@AnnoModelo"].Value = conductor.Modelo.Trim();
                    commandData.Parameters.Add("@Marca", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Marca"].Value = conductor.Marca.Trim();

                    OpenData("query"); //abrimos la base de datos
                    commandData.ExecuteNonQuery(); //ejecutamos el query en la base de datos con todos los parametros
                    CloseData();// cerramos la coneccion a la base de datos
                    retorno = "OKConductor";
                    
                }
            }
            catch (Exception ex)
            {
                CloseData(); //cerramos la coneccion a la base de datos
                retorno= "Error - " +ex.Message; // retornamos el valor de false si hay algun error
            }
            return retorno; //retornamos el valor de true si todo salio bien
        }

        public string RegistrarViaje(Viaje viaje)
        {
            string retorno = "";
            SqlCommand commandData = new SqlCommand();
            try
            {
                if (ConductorActivo(viaje.Identificacion) == true) //buscamos si existe el conductor
                {
                    retorno = "TieneViajeActivo";
                }
                else if (retorno == "")
                {
                    //query para insertar condutor
                    string strquery = "INSERT INTO [dbo].[Viaje]" +
                        "([Id_viaje],[Lugar_inicio],[Lugar_final],[Descripcion],[Tiempoestimado],[Identificacion],[Estado])" +
                        "VALUES(@Id_viaje, @Lugar_inicio, @Lugar_final, @Descripcion, @Tiempoestimado, @Identificacion, @Estado)" ;
                    commandData = new System.Data.SqlClient.SqlCommand(strquery, sqlConnData);
                    commandData.CommandType = CommandType.Text;
                    //agregamos los parametros al query con el valor requerido
                    commandData.Parameters.Add("@Id_viaje", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Id_viaje"].Value = viaje.Id_viaje.Trim();
                    commandData.Parameters.Add("@Lugar_inicio", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Lugar_inicio"].Value = viaje.Lugar_inicio.Trim();
                    commandData.Parameters.Add("@Lugar_final", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Lugar_final"].Value = viaje.Lugar_final.Trim();
                    commandData.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Descripcion"].Value = viaje.Descripcion.Trim();

                    commandData.Parameters.Add("@Tiempoestimado", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Tiempoestimado"].Value = viaje.Tiempoestimado.Trim();

                    commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Identificacion"].Value = viaje.Identificacion.Trim();
                    commandData.Parameters.Add("@Estado", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Estado"].Value = viaje.Estado.Trim();

                    OpenData("query"); //abrimos la base de datos
                    commandData.ExecuteNonQuery(); //ejecutamos el query en la base de datos con todos los parametros
                    CloseData();// cerramos la coneccion a la base de datos
                    retorno = "OKViaje";

                }
            }
            catch (Exception ex)
            {
                CloseData(); //cerramos la coneccion a la base de datos
                retorno = "Error - " + ex.Message; // retornamos el valor de false si hay algun error
            }
            return retorno; //retornamos el valor de true si todo salio bien
        }
        public string RegistrarTracking(Tracking tracking)
        {
            string retorno = "";
            SqlCommand commandData = new SqlCommand();
            try
            {
                //query para insertar condutor
                string strquery = "INSERT INTO [dbo].[Tracking] " +
                "([Id_viaje],[Ubicacion],[Observaciones]) " +
                "VALUES(@Id_viaje, @Ubicacion, @Observaciones)";
                commandData = new System.Data.SqlClient.SqlCommand(strquery, sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Id_viaje", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Id_viaje"].Value = tracking.Id_viaje.Trim();
                commandData.Parameters.Add("@Ubicacion", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Ubicacion"].Value = tracking.Ubicacion.Trim();
                commandData.Parameters.Add("@Observaciones", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Observaciones"].Value = tracking.Observaciones.Trim();

                OpenData("query"); //abrimos la base de datos
                commandData.ExecuteNonQuery(); //ejecutamos el query en la base de datos con todos los parametros
                CloseData();// cerramos la coneccion a la base de datos
                retorno = "OKTracking";

            }
            catch (Exception ex)
            {
                CloseData(); //cerramos la coneccion a la base de datos
                retorno = "Error - " + ex.Message; // retornamos el valor de false si hay algun error
            }
            return retorno; //retornamos el valor de true si todo salio bien
        }
        public string RegistrarViajeFinalizado(string Id_viaje)
        {
            string retorno = "";
            SqlCommand commandData = new SqlCommand();
            try
            {
                //query para insertar condutor
                string strquery = "update viaje set Estado='FINALIZADO' where Id_viaje = @Id_viaje";
                commandData = new System.Data.SqlClient.SqlCommand(strquery, sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Id_viaje", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Id_viaje"].Value = Id_viaje;

                OpenData("query"); //abrimos la base de datos
                commandData.ExecuteNonQuery(); //ejecutamos el query en la base de datos con todos los parametros
                CloseData();// cerramos la coneccion a la base de datos
                retorno = "OKViajeActualizacion";

            }
            catch (Exception ex)
            {
                CloseData(); //cerramos la coneccion a la base de datos
                retorno = "Error - " + ex.Message; // retornamos el valor de false si hay algun error
            }
            return retorno; //retornamos el valor de true si todo salio bien
        }
        public Conductor getConductorActivo(string UserName)
        {
            Conductor retornoconductor=null;
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT [Identificacion]" +
                    ",[Nombre],[PrimerApeliido],[SegundoApellido],[RutaAsignada],[UserName],[Acceso]" +
                    "FROM[dbo].[Conductor] where UserName = @UserName", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@UserName"].Value = UserName.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    Conductor conductor = new Conductor(
                        reader["Identificacion"].ToString()
                        , reader["Nombre"].ToString()
                        , reader["PrimerApeliido"].ToString()
                        , reader["SegundoApellido"].ToString()
                        , reader["RutaAsignada"].ToString()
                        , reader["UserName"].ToString()
                        , reader["Acceso"].ToString()
                        , "","",""
                        );
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    //return true; // retornamos el valor de true
                    return conductor;
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    return retornoconductor;
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                return retornoconductor;
            }
            
        }
        public Viaje getViajeActivoConductor(string UserName)
        {
            Viaje retornoviaje=null;
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT v.Id_viaje,v.Lugar_inicio, " +
                    "v.Lugar_final,v.Descripcion,v.Tiempoestimado,c.identificacion,v.Estado " +
                    "FROM[dbo].[Viaje] v with(nolock), Conductor c with(nolock) " +
                    "where v.Identificacion = c.Identificacion and c.UserName = @UserName and v.Estado='ACTIVO' ", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@UserName"].Value = UserName.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    while (reader.Read())
                    {
                        retornoviaje = new Viaje(
                            reader["Id_viaje"].ToString()
                            , reader["Lugar_inicio"].ToString()
                            , reader["Lugar_final"].ToString()
                            , reader["Descripcion"].ToString()
                            , reader["Tiempoestimado"].ToString()
                            , reader["Identificacion"].ToString()
                            , reader["Estado"].ToString()
                            );
                    }
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    //return true; // retornamos el valor de true
                    return retornoviaje;
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    return retornoviaje;
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                return retornoviaje;
            }

        }
        public Viaje getViajeActivo(string Identificacion)
        {
            Viaje retornoviaje = null;
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query"); //abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT [Id_viaje]" +
                    ",[Lugar_inicio],[Lugar_final],[Descripcion],[Tiempoestimado],[Identificacion],[Estado]" +
                    "FROM[dbo].[Viaje] where Identificacion = @Identificacion and Estado='ACTIVO' ", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = Identificacion.Trim();
                reader = commandData.ExecuteReader(); //cargamos el resultado del query al reader
                if (reader.HasRows) //si existen registros
                {
                    Viaje viaje = new Viaje(
                        reader["Id_viaje"].ToString()
                        , reader["Lugar_inicio"].ToString()
                        , reader["Lugar_final"].ToString()
                        , reader["Descripcion"].ToString()
                        , reader["Tiempoestimado"].ToString()
                        , reader["Identificacion"].ToString()
                        , reader["Estado"].ToString()
                        );
                    reader.Close(); //cerramos el reader
                    CloseData();// si se logro abrir entonces la cerramos
                    //return true; // retornamos el valor de true
                    return viaje;
                }
                else
                {
                    reader.Close(); //cerramos el reader
                    CloseData(); //cerramos la coneccion a la base de datos
                    return retornoviaje;
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
                return retornoviaje;
            }

        }
        public DataTable ConsultarViajes(bool soloactivos)
        {
            SqlCommand commandData = new SqlCommand();
            System.Data.DataSet ds = new System.Data.DataSet();
            DataTable dt = new DataTable();
           
            OpenData("query");
            string sqlconsulta = "SELECT [Id_viaje],c.Nombre,c.UserName, [Lugar_inicio], " +
                "[Lugar_final],[Descripcion],[Tiempoestimado],[Estado]" +
                "FROM[dbo].[Viaje] v with(nolock), Conductor c with(nolock)" +
                "where v.Identificacion = c.Identificacion ";
            if (soloactivos) { sqlconsulta += " and v.Estado='ACTIVO'"; }
            commandData = new System.Data.SqlClient.SqlCommand(sqlconsulta, sqlConnData);
            commandData.CommandType = CommandType.Text;
            //agregamos el parametro al query con el valor requerido

            SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(commandData);
            try
            {
                da.Fill(dt);
            }
            catch (Exception)
            {
                ds = null;
            }
            finally
            {
                da.Dispose();
            }
            CloseData();
            return dt;
        }
        public DataTable ConsultarViajesTracking(string id_viaje)
        {
            SqlCommand commandData = new SqlCommand();
            System.Data.DataSet ds = new System.Data.DataSet();
            DataTable dt = new DataTable();

            OpenData("query");
            string sqlconsulta = "SELECT [Id_viaje],[Ubicacion],[Observaciones] FROM [dbo].[Tracking] where Id_viaje = @Id_Viaje";
            commandData = new System.Data.SqlClient.SqlCommand(sqlconsulta, sqlConnData);
            commandData.CommandType = CommandType.Text;
            commandData.Parameters.Add("@Id_Viaje", System.Data.SqlDbType.VarChar, 50);
            commandData.Parameters["@Id_Viaje"].Value = id_viaje.Trim();
            //agregamos el parametro al query con el valor requerido

            SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(commandData);
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                ds = null;
            }
            finally
            {
                da.Dispose();
            }
            CloseData();
            return dt;
        }
        public DataTable ConsultarConductores()
        {
            SqlCommand commandData = new SqlCommand();
            System.Data.DataSet ds = new System.Data.DataSet();
            DataTable dt = new DataTable();
            OpenData("query");
            string sqlconsulta = " SELECT * FROM [Conductor] with(nolock) ";

            commandData = new System.Data.SqlClient.SqlCommand(sqlconsulta, sqlConnData);
            commandData.CommandType = CommandType.Text;
            //agregamos el parametro al query con el valor requerido

            SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(commandData);
            try
            {
                da.Fill(dt);
            }
            catch (Exception )
            {
                ds = null;
            }
            finally
            {
                da.Dispose();
            }
            CloseData();
            return dt;
        }

        public string ValidarConductor(string Identificacion, string Acceso)
        {
            SqlCommand commandData = new SqlCommand();
            try
            {
                //query para insertar condutor
                commandData = new System.Data.SqlClient.SqlCommand("UPDATE [dbo].[Conductor]" +
                    "SET[acceso] = @Acceso  WHERE Identificacion = @Identificacion", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = Identificacion;
                commandData.Parameters.Add("@Acceso", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Acceso"].Value = Acceso;

                if (ExisteConductor(Identificacion)) //buscamos si existe el alumno
                {
                    OpenData("query"); //abrimos la base de datos
                    commandData.ExecuteNonQuery(); //ejecutamos el query en la base de datos con todos los parametros
                    CloseData();// cerramos la coneccion a la base de datos
                    return "OK"; //retornamos el valor de true si todo salio bien
                }
                else
                {
                    return "ExistenteAlumno"; //retornamos el valor de false si el conductor ya existe
                }
            }
            catch (Exception ex)
            {
                CloseData(); //cerramos la coneccion a la base de datos
                return "Error - " + ex.Message;  // retornamos el valor de false si hay algun error
            }
        }
        #endregion //"Conductor"

        #region "Camion"
        /// <summary>
        /// Metodo para validar que exista un conductor
        /// </summary>
        /// <param name="prmIdentificacion"></param>
        /// <returns></returns>
        public bool ExisteCamion(string prmPlaca)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            try
            {
                OpenData("query");//abrimos la coneccion a la base de datos
                commandData = new System.Data.SqlClient.SqlCommand("SELECT * FROM [Camion] with(nolock) " +
                    "where Placa = @Placa", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos el parametro al query con el valor requerido
                commandData.Parameters.Add("@Placa", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Placa"].Value = prmPlaca.Trim();
                reader = commandData.ExecuteReader();//cargamos el resultado del query al reader
                if (reader.HasRows)//si existen registros
                {
                    reader.Close();//cerramos el reader
                    CloseData();//cerramos la coneccion a la base de datos
                    return true;
                }
                else
                {
                    reader.Close();//cerramos el reader
                    CloseData();//cerramos la coneccion a la base de datos
                    return false;//retornamos el valor false
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData();//cerramos la coneccion a la base de datos
                return false;//retornamos el valor false
            }

        }
        /// <summary>
        /// Metodo para guardar la clase camion en la base de datos
        /// </summary>
        /// <param name="conductor"></param>
        /// <returns></returns>
        public bool GuardaCamion(Camion camion)
        {
            SqlCommand commandData = new SqlCommand();
            try
            {
                //query para insertar camion
                commandData = new System.Data.SqlClient.SqlCommand("INSERT INTO [Camion]([Placa],[AnnoModelo],[Marca],[CapacidadKG],[CapacidadVl])" +
                    "VALUES(@Placa,@AnnoModelo,@Marca,@CapacidadKG,@CapacidadVl)", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Placa", System.Data.SqlDbType.VarChar, 8);
                commandData.Parameters["@Placa"].Value = camion.Placa.Trim();
                commandData.Parameters.Add("@AnnoModelo", System.Data.SqlDbType.VarChar, 4);
                commandData.Parameters["@AnnoModelo"].Value = camion.Modelo.Trim();
                commandData.Parameters.Add("@Marca", System.Data.SqlDbType.VarChar, 50);
                commandData.Parameters["@Marca"].Value = camion.Marca.Trim();
                commandData.Parameters.Add("@CapacidadKG", System.Data.SqlDbType.Decimal);
                commandData.Parameters["@CapacidadKG"].Value = Convert.ToDecimal(camion.capacidadkilos.Trim());
                commandData.Parameters.Add("@CapacidadVl", System.Data.SqlDbType.Decimal);
                commandData.Parameters["@CapacidadVl"].Value = Convert.ToDecimal(camion.capacidadvolumen.Trim());

                if (ExisteCamion(camion.Placa) == false)//buscamos si existe el camion
                {
                    OpenData("query");//abrimos la base de datos
                    commandData.ExecuteNonQuery();//ejecutamos el query en la base de datos con todos los parametros
                    CloseData();// cerramos la coneccion a la base de datos
                    _LatestError = "Camion " + camion.Placa.Trim() + " agregado satisfactoriamente.";
                    return true;//retornamos el valor de true si todo salio bien
                }
                else
                {
                    _LatestError = "Camion ya existe";
                    return false;//retornamos el valor de false si el conductor ya existe
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData();// cerramos la coneccion a la base de datos
                return false;// retornamos el valor de false si hay algun error
            }
        }
        #endregion //"Camion"

        

    }
}
