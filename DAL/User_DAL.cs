using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Linq;
using DnD_CMS.Models;

namespace DnD_CMS.DAL
{
    public class User_DAL
    {
        string conString = "*******";

        //Get all Users from database
        public List<userModel> getAllUsers()
        {
            Console.WriteLine("test 0");
            List<userModel> userList = new List<userModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllUsers";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtCharacters = new DataTable();

                connection.Open();
                sqlDA.Fill(dtCharacters);
                connection.Close();

                foreach (DataRow row in dtCharacters.Rows)
                {
                    userList.Add(new userModel
                    {
                        userID = Convert.ToInt32(row["userID"]),
                        userName = row["userName"].ToString(),
                        userPassword = row["userPassword"].ToString()
                    });
                }
            }
            return userList;
        }

        //Insert Users into database
        public bool insertUsers(userModel user)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_insertUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userName", user.userName);
                command.Parameters.AddWithValue("@userPassword", user.userPassword);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Validates whether or not a user with inputted credentials exists in database.
        public bool validateUser(userModel user)
        {
            int id = 0;
            var result = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_validateUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", user.userName);
                command.Parameters.AddWithValue("@password", user.userPassword);

                connection.Open();
                id = command.ExecuteNonQuery();
                result = (int)command.ExecuteScalar();
                connection.Close();
            }
            if(result == 1){
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
