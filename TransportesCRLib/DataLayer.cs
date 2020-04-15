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
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    class DataLayer
    {
        #region "Definicion de variables"
        public static SqlConnection sqlConnData = new SqlConnection(); //connector a la base de datos
        public string _LatestError = ""; //campo publico para mostrar mensajes de la clase
        public static string _ServerDataSource = "BINARYCRCD2"; //campo para el datasourse usado en la coneccion a la base de datos
        public static string _ServerInitialCatalog = "TransportesCR"; //campo para el catalogo/base de datos usado en la coneccion
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
        /// <summary>
        /// Metodo para retornar todos los conductores segun el paramatro
        /// si el parametro esta vacio retorna todos los conductores
        /// </summary>
        /// <param name="prmBuscar"></param>
        /// <returns>Retorna una coleccion de tipo Conductor</returns>
        public Dictionary<string, Conductor> GetConductores(string prmBuscar)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            //Creamos la coleccion de tipo Conductor
            Dictionary<string, Conductor> Conductores = new Dictionary<string, Conductor>();
            OpenData("query"); //abrimos la coneccion a la base de datos
            try
            {
                if (!string.IsNullOrEmpty(prmBuscar.Trim())) //si el parametro es valido y no esta en blanco
                {
                    commandData = new System.Data.SqlClient.SqlCommand("SELECT [Identificacion],[Nombre],[PrimerApeliido],[SegundoApellido]" +
                        ",[RutaAsignada] FROM [Conductor] with(nolock) " +
                        "where Identificacion like '%'+@Identificacion+'%' " +
                        "or Nombre like '%'+@Nombre+'%' " +
                        "or PrimerApeliido like '%'+@PrimerApellido+'%' " +
                        "or SegundoApellido like '%'+@SegundoApellido+'%' " +
                        "or RutaAsignada like '%'+@RutaAsignada+'%' " +
                        "order by Identificacion", sqlConnData);
                    //agregamos los parametros al query con el valor requerido
                    commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                    commandData.Parameters["@Identificacion"].Value = prmBuscar.Trim();
                    commandData.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Nombre"].Value = prmBuscar.Trim();
                    commandData.Parameters.Add("@PrimerApellido", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@PrimerApellido"].Value = prmBuscar.Trim();
                    commandData.Parameters.Add("@SegundoApellido", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@SegundoApellido"].Value = prmBuscar.Trim();
                    commandData.Parameters.Add("@RutaAsignada", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@RutaAsignada"].Value = prmBuscar.Trim();
                }
                else
                {
                    //si el parametro esta en blanco
                    commandData = new System.Data.SqlClient.SqlCommand("SELECT [Identificacion],[Nombre],[PrimerApeliido],[SegundoApellido]" +
                        ",[RutaAsignada] FROM [Conductor] with(nolock) order by Identificacion", sqlConnData);
                }

                commandData.CommandType = CommandType.Text;
                reader = commandData.ExecuteReader(CommandBehavior.CloseConnection); //cargamos el resultado del query al reader
                if (reader.HasRows)//si existen registros
                {
                    while (reader.Read())//leemos cada registro
                    {
                        // Creamos la instancia para la clase Conductor
                        // con los datos del query
                        Conductor conductor = new Conductor(reader["Identificacion"].ToString()
                            , reader["Nombre"].ToString()
                            , reader["PrimerApeliido"].ToString()
                            , reader["SegundoApellido"].ToString()
                            , reader["RutaAsignada"].ToString()
                            );
                        Conductores.Add(conductor.Identificacion, conductor);//agregamos la instancia a la coleccion
                    }
                    reader.Close();//cerramos el reader
                }
                CloseData(); //cerramos la coneccion a la base de datos
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
            }
            CloseData(); //cerramos la base de datos
            return Conductores; //retornamos la coleccion 
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
        /// <summary>
        /// Metodo para retornar todos los camiones segun el paramatro
        /// si el parametro esta vacio retorna todos los camiones
        /// </summary>
        /// <param name="prmBuscar"></param>
        /// <returns>Retorna una coleccion de tipo Camion</returns>
        public Dictionary<string, Camion> GetCamiones(string prmBuscar)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            //Creamos la coleccion de tipo Conductor
            Dictionary<string, Camion> Camioniones = new Dictionary<string, Camion>();
            OpenData("query");//abrimos la coneccion a la base de datos
            try
            {
                if (!string.IsNullOrEmpty(prmBuscar.Trim()))//si el parametro es valido y no esta en blanco
                {
                    commandData = new System.Data.SqlClient.SqlCommand("SELECT [Placa],[AnnoModelo],[Marca],[CapacidadKG],[CapacidadVl] FROM [Camion] with(nolock) " +
                         "where Placa like '%'+@Placa+'%' " +
                         "or AnnoModelo like '%'+@AnnoModelo+'%' " +
                         "or Marca like '%'+@Marca+'%' " +
                         "order by Placa", sqlConnData);
                    //agregamos los parametros al query con el valor requerido
                    commandData.Parameters.Add("@Placa", System.Data.SqlDbType.VarChar, 8);
                    commandData.Parameters["@Placa"].Value = prmBuscar.Trim();
                    commandData.Parameters.Add("@AnnoModelo", System.Data.SqlDbType.VarChar, 4);
                    commandData.Parameters["@AnnoModelo"].Value = prmBuscar.Trim();
                    commandData.Parameters.Add("@Marca", System.Data.SqlDbType.VarChar, 50);
                    commandData.Parameters["@Marca"].Value = prmBuscar.Trim();
                }
                else
                {
                    //si el parametro esta en blanco
                    commandData = new System.Data.SqlClient.SqlCommand("SELECT [Placa],[AnnoModelo],[Marca],[CapacidadKG],[CapacidadVl] " +
                        "FROM [Camion] with(nolock) order by Placa", sqlConnData);
                }

                commandData.CommandType = CommandType.Text;
                reader = commandData.ExecuteReader(CommandBehavior.CloseConnection);//cargamos el resultado del query al reader
                if (reader.HasRows)//si existen registros
                {
                    while (reader.Read())//leemos cada registro
                    {
                        // Creamos la instancia para la clase camion
                        // con los datos del query
                        Camion camion = new Camion(reader["Placa"].ToString()
                            , reader["AnnoModelo"].ToString()
                            , reader["Marca"].ToString()
                            , reader["CapacidadKG"].ToString()
                            , reader["CapacidadVl"].ToString()
                            );
                        Camioniones.Add(camion.Placa, camion);//agregamos la instancia a la coleccion
                    }
                    reader.Close();//cerramos el reader
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
            }

            CloseData();//cerramos la base de datos
            return Camioniones;//Retornamos la coleccion
        }
        #endregion //"Camion"

        #region "ConductorxCamion"
        /// <summary>
        /// Metodo para retornar todos los camiones x conductor segun el paramatro
        /// si el parametro esta vacio retorna todos los registros
        /// el retorno es una coleccion con un string Value con formato especial
        /// </summary>
        /// <param name="prmBuscar"></param>
        /// <param name="prmAssigned"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetConductorxCamion(string prmBuscar, int prmAssigned)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            //Creamos la coleccion de tipo string
            Dictionary<string, string> ConductorxCamion = new Dictionary<string, string>();
            OpenData("query");//abrimos la coneccion a la base de datos
            if (!string.IsNullOrEmpty(prmBuscar.Trim()))//si el parametro es valido y no esta en blanco
            {
                if (prmAssigned == 0)//si buscamos los registros de camiones x conductor NO asignados
                {
                    commandData = new System.Data.SqlClient.SqlCommand("select c.Placa,Detalles= trim(c.Placa)+' / '+trim(c.Marca)+'('+trim(c.AnnoModelo)+') '+trim(convert(varchar,convert(decimal(8,2),c.CapacidadKG))) " +
                        " from Camion c with(nolock)  " +
                        " left join ConductorCamion cxc with(nolock) on cxc.Placa = c.Placa and cxc.Identificacion = @Identificacion " +
                        " where cxc.Placa is null " +
                        " order by c.Placa ", sqlConnData);
                }
                else //si buscamos los registros de camiones x conductor ASIGNADOS
                {
                    commandData = new System.Data.SqlClient.SqlCommand("select c.Placa,Detalles= trim(c.Placa)+' / '+trim(c.Marca)+'('+trim(c.AnnoModelo)+') '+trim(convert(varchar,convert(decimal(8,2),c.CapacidadKG))) " +
                        " from Camion c with(nolock)  " +
                        " left join ConductorCamion cxc with(nolock) on cxc.Placa = c.Placa and cxc.Identificacion = @Identificacion " +
                        " where cxc.Placa is NOT null " +
                        " order by c.Placa ", sqlConnData);
                }
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = prmBuscar.Trim();
            }

            commandData.CommandType = CommandType.Text;
            reader = commandData.ExecuteReader(CommandBehavior.CloseConnection);//cargamos el resultado del query al reader
            if (reader.HasRows)//si existen registros
            {
                while (reader.Read())//leemos cada registro
                {
                    ConductorxCamion.Add(reader["Placa"].ToString(), reader["Detalles"].ToString());//agregamos la instancia a la coleccion
                }
                reader.Close();//cerramos el reader
            }
            CloseData();//cerramos la base de datos
            return ConductorxCamion;//Retornamos la coleccion
        }
        /// <summary>
        /// Metodo retorna los datos del conductor para ligarlo a los camiones
        /// </summary>
        /// <param name="prmBuscar"></param>
        /// <returns></returns>
        public Dictionary<string, Conductor> GetConductorxCamion(string prmBuscar)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            //Creamos la coleccion de tipo string
            Dictionary<string, Conductor> Conductores = new Dictionary<string, Conductor>();
            OpenData("query");//abrimos la coneccion a la base de datos
            if (!string.IsNullOrEmpty(prmBuscar.Trim()))//si el parametro es valido y no esta en blanco
            {
                commandData = new System.Data.SqlClient.SqlCommand("SELECT [Identificacion],[Nombre],[PrimerApeliido],[SegundoApellido],[RutaAsignada] FROM [Conductor] with(nolock) " +
                    "where Identificacion= @Identificacion " +
                    "order by Identificacion", sqlConnData);
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = prmBuscar.Trim();
            }

            commandData.CommandType = CommandType.Text;
            reader = commandData.ExecuteReader(CommandBehavior.CloseConnection);//cargamos el resultado del query al reader
            if (reader.HasRows)//si existen registros
            {
                while (reader.Read())//leemos cada registro
                {
                    // Creamos la instancia para la clase conductor
                    // con los datos del query
                    Conductor conductor = new Conductor(reader["Identificacion"].ToString()
                        , reader["Nombre"].ToString()
                        , reader["PrimerApeliido"].ToString()
                        , reader["SegundoApellido"].ToString()
                        , reader["RutaAsignada"].ToString()
                        );
                    Conductores.Add(conductor.Identificacion, conductor);//agregamos la instancia a la coleccion
                }
                reader.Close();//cerramos el reader
            }
            CloseData();//cerramos la base de datos
            return Conductores;//Retornamos la coleccion
        }



        /// <summary>
        /// Metodo retorna los datos del conductor para ligarlo a los camiones
        /// </summary>
        /// <param name="prmBuscar"></param>
        /// <returns></returns>
        public List<ConductorxCamion> GetConductorxCamiones(string prmBuscar)
        {
            SqlCommand commandData = new SqlCommand();
            SqlDataReader reader;
            //Creamos la coleccion de tipo string
            List<ConductorxCamion> conductorxcamiones = new List<ConductorxCamion>();
            OpenData("query");//abrimos la coneccion a la base de datos
            try
            {
                if (!string.IsNullOrEmpty(prmBuscar.Trim()))//si el parametro es valido y no esta en blanco
                {
                    commandData = new System.Data.SqlClient.SqlCommand("SELECT [Identificacion],[Placa] FROM [ConductorCamion] with(nolock) " +
                        "where Identificacion like '%'+@prmBuscar+'%' " +
                        "or Placa like '%'+@prmBuscar+'%' " +
                        "order by Identificacion", sqlConnData);
                    //agregamos los parametros al query con el valor requerido
                    commandData.Parameters.Add("@prmBuscar", System.Data.SqlDbType.VarChar, 10);
                    commandData.Parameters["@prmBuscar"].Value = prmBuscar.Trim();
                }
                else
                {
                    commandData = new System.Data.SqlClient.SqlCommand("SELECT [Identificacion],[Placa] FROM [ConductorCamion] with(nolock) order by Identificacion", sqlConnData);
                }

                commandData.CommandType = CommandType.Text;
                reader = commandData.ExecuteReader(CommandBehavior.CloseConnection);//cargamos el resultado del query al reader
                if (reader.HasRows)//si existen registros
                {
                    while (reader.Read())//leemos cada registro
                    {
                        // Creamos la instancia para la clase ConductorxCamion
                        // con los datos del query
                        ConductorxCamion conductorxcamion = new ConductorxCamion(reader["Identificacion"].ToString()
                            , reader["Placa"].ToString()
                            );
                        conductorxcamiones.Add(conductorxcamion);//agregamos la instancia a la coleccion
                    }
                    reader.Close();//cerramos el reader
                }
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData(); //cerramos la base de datos
            }
            CloseData();//cerramos la base de datos
            return conductorxcamiones;//Retornamos la coleccion
        }

        /// <summary>
        /// Metodo para borrar todos los registros de camiones x conductor 
        /// </summary>
        /// <param name="prmIdentificacion"></param>
        /// <returns></returns>
        public bool BorrarConductorxCamion(string prmIdentificacion)
        {
            SqlCommand commandData = new SqlCommand();
            try
            {
                commandData = new System.Data.SqlClient.SqlCommand("DELETE FROM [ConductorCamion] WHERE identificacion = @Identificacion ", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = prmIdentificacion.Trim();

                OpenData("query");//abrimos la coneccion a la base de datos
                commandData.ExecuteNonQuery();//ejecutamos el query 
                CloseData();//cerramos la base de datos
                return true;//Retornamos el valor True si el resultado es positivo
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData();//cerramos la base de datos
                return false; //retornamos el valor false como resultado de errores
            }
        }
        /// <summary>
        /// Metodo para guardar los camiones asigandos al conductor
        /// </summary>
        /// <param name="prmIdentificacion"></param>
        /// <param name="prmPlaca"></param>
        /// <returns></returns>
        public bool GuardaConductorxCamion(string prmIdentificacion, string prmPlaca)
        {
            SqlCommand commandData = new SqlCommand();
            try
            {
                commandData = new System.Data.SqlClient.SqlCommand("INSERT INTO [ConductorCamion]([Identificacion],[Placa])" +
                    "VALUES(@Identificacion,@Placa)", sqlConnData);
                commandData.CommandType = CommandType.Text;
                //agregamos los parametros al query con el valor requerido
                commandData.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar, 10);
                commandData.Parameters["@Identificacion"].Value = prmIdentificacion.Trim();
                commandData.Parameters.Add("@Placa", System.Data.SqlDbType.VarChar, 8);
                commandData.Parameters["@Placa"].Value = prmPlaca.Trim();

                OpenData("query");//abrimos la coneccion a la base de datos
                commandData.ExecuteNonQuery();//ejecutamos el query 
                CloseData();//cerramos la base de datos
                _LatestError = "Camion por conductor agregado satisfactoriamente.";//si no existe ningun error retornamos un mensaje
                return true;//Retornamos el valor True si el resultado es positivo
            }
            catch (Exception ex)
            {
                _LatestError = ex.Message;//si existe algun error se muestra un mensaje
                CloseData();//cerramos la base de datos
                return false;
            }
        }

        #endregion //"ConductorxCamion"

    }
}
