using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;

namespace GeneralHelpers
{
    /// <summary>
    /// A helper class for SQL server databases 
    /// </summary>
    public static class DatabaseHelpers
    {
        #region Private Members

        /// <summary>
        /// The conncetion variable to work with the database
        /// </summary>
        private static SqlConnection connection = new SqlConnection();


        #endregion

        #region Set The Connection string


        /// <summary>
        /// Get the entity connection string from the app.config and gets the raw connection string
        /// </summary>
        /// <param name="ConnectionStringName">The connection string name tag in the app.config</param>
        private static void SetConnection(string ConnectionStringName)
        {
            //Gets the connection string in the app.config
            var entityConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            
            var connectionBulider = new EntityConnectionStringBuilder(entityConnectionString);
           
            //Gets the connection string that we can use in the raw connections
            string connectionString = connectionBulider.ProviderConnectionString;
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception execption)
            {
                throw execption;
            }
        }

        #endregion

        #region Back Up DataBase
        /// <summary>
        /// Backs up a the database of the program that called this method to the path that is provided
        /// </summary>
        /// <param name="path">The path to back up the database to</param>
        /// <param name="programName">The program name to use to name the backed up file</param>
        /// <param name="connectionStringName">The connection string name tag in the app.config</param>
        /// <returns></returns>
        public static async Task<int> BackUpDataBase(string path, string programName, string connectionStringName)
        {
            int returnValue = 0;

            //Sets the connection string and gets it from the app.config    
            SetConnection(connectionStringName);

            //Gets the database name from the connection string
            string dataBase = connection.Database.ToString();

            //Creats the query to execute for the back up
            string cmd = "BACKUP DATABASE [" + dataBase + "] TO DISK='" + path + "\\" + programName + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

            try
            {
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    //If the conncetion is not open, open it
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    //Execute the query
                    returnValue = await command.ExecuteNonQueryAsync();

                    //Close the connection when done
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
            return returnValue;
        }
        #endregion

        #region Restore Database
        /// <summary>
        /// Restores the database of the program that calles this method
        /// </summary>
        /// <param name="path">The path to restore the database</param>
        /// <param name="connectionStringName">The connection string name tag in the app.config</param>
        /// <returns>True if success, false if error happened</returns>
        public static async Task<bool> RestoreDatabase(string path,string connectionStringName)
        {
            //Sets the connection string and gets it from the app.config    
            SetConnection(connectionStringName);

            //Gets the database name from the connection string
            string database = connection.Database.ToString();

            try
            {
                //Queries to execute for the restore process
             
                //If the conncetion is not open, open it
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }


                string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, connection);

                await bu2.ExecuteNonQueryAsync();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + path + "'WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, connection);

                await bu3.ExecuteNonQueryAsync();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, connection);

                await bu4.ExecuteNonQueryAsync();

                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            
            return true;
        }

        #endregion
    }
}

