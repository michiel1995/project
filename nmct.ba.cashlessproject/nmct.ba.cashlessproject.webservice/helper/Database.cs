using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webservice.helper
{
    public class Database
    {
        
            //1. connectionstring ophalen 
            //2. Connectie maken en openen 
            //3. Command opstellen (vb SELECT...)
            //4. parameters toevoegen (optioneel)
            //5. execute Reader(bij GET) of execute non query (bij Post)
            //6. connectie sluiten 


            //1. en 2.
            private static DbConnection GetConnection(string name)
            {
                //de juiste connectie ophalen
                ConnectionStringSettings settings;
                settings = ConfigurationManager.ConnectionStrings[name];
                //connectie met database aanmaken
                DbConnection con = DbProviderFactories.GetFactory(settings.ProviderName).CreateConnection();
                con.ConnectionString = settings.ConnectionString;
                con.Open();
                //connectie teruggeven
                return con;
            }

            //6.
            public static void Releaseconnection(DbConnection con)
            {
                if (con != null)
                {
                    con.Close();
                    con = null;
                }
            }

            //4.
            private static DbCommand BuildCommand(string name, string sql, params DbParameter[] parameters)
            {
                //connectie maken
                DbCommand Com = GetConnection(name).CreateCommand();

                //comando opstellen
                Com.CommandType = System.Data.CommandType.Text;
                Com.CommandText = sql;

                //eventueel param toevoegen
                if (parameters != null)
                {
                    Com.Parameters.AddRange(parameters);
                }

                //commando teruggeven
                return Com;
            }

            public static DbParameter addParameter(string connection, string name, object value)
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connection];
                DbParameter par = DbProviderFactories.GetFactory(settings.ProviderName).CreateParameter();
                par.ParameterName = name;
                par.Value = value;
                return par;
            }

            //GET
            public static DbDataReader GetData(string name, string sql, params DbParameter[] parameters)
            {
                DbCommand command = null;
                DbDataReader reader = null;
                try
                {
                    command = BuildCommand(name, sql, parameters);
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    return reader;
                }
                catch (Exception)
                {
                    if (reader != null)
                        reader.Close();

                    if (command != null)
                        Releaseconnection(command.Connection);
                    throw;
                }
            }

            //UPDATE EN DELETE
            public static int ModifyData(string name, string sql, params DbParameter[] parameters)
            {
                //geeft aantal rijen die aangepast zijn terug 
                DbCommand command = null;

                try
                {
                    command = BuildCommand(name, sql, parameters);
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    if (command != null)
                        Releaseconnection(command.Connection);
                    throw;
                }
            }

            //POST
            public static int InsertData(string name, string sql, params DbParameter[] parameters)
            {
                DbCommand command = null;
                //geeft id terug van laatst aangapste
                try
                {
                    command = BuildCommand(name, sql, parameters);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.CommandText = "SELECT @@IDENTITY";
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    command.Connection.Close();
                    return id;

                }
                catch (Exception)
                {

                    if (command != null)
                        Releaseconnection(command.Connection);
                    throw;
                }
            }



            //zorgt voor een transactie
            public static DbTransaction BeginTransaction(string name)
            {
                DbConnection con = null;
                try
                {
                    con = GetConnection(name);
                    return con.BeginTransaction();
                }
                catch (Exception)
                {
                    Releaseconnection(con);
                    throw;
                }
            }

            private static DbCommand BuildCommand(DbTransaction trans, string sql, params DbParameter[] parameters)
            {
                //connectie maken
                DbCommand Com = trans.Connection.CreateCommand();
                Com.Transaction = trans;

                //comando opstellen
                Com.CommandType = System.Data.CommandType.Text;
                Com.CommandText = sql;

                //eventueel param toevoegen
                if (parameters != null)
                {
                    Com.Parameters.AddRange(parameters);
                }

                //commando teruggeven
                return Com;
            }

            public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
            {
                DbCommand command = null;
                DbDataReader reader = null;
                try
                {
                    command = BuildCommand(trans, sql, parameters);
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    return reader;
                }
                catch (Exception)
                {
                    if (reader != null)
                        reader.Close();

                    if (command != null)
                        Releaseconnection(command.Connection);
                    throw;
                }
            }

            public static int ModifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
            {
                //geeft aantal rijen die aangepast zijn terug 
                DbCommand command = null;

                try
                {
                    command = BuildCommand(trans, sql, parameters);
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    if (command != null)
                        Releaseconnection(command.Connection);
                    throw;
                }
            }

            public static int InsertData(DbTransaction trans, string sql, params DbParameter[] parameters)
            {
                DbCommand command = null;
                //geeft id terug van laatst aangapste
                try
                {
                    command = BuildCommand(trans, sql, parameters);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.CommandText = "SELECT @@IDENTITY";
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    command.Connection.Close();
                    return id;

                }
                catch (Exception)
                {

                    if (command != null)
                        Releaseconnection(command.Connection);
                    throw;
                }
            }
        
    }
}