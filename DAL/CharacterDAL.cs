using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Linq;
using DnD_CMS.Models;


namespace DnD_CMS.DAL
{
    public class CharacterDAL
    {
        string conString = "*******";


        //Get All Characters from the database
        public List<characterModel> getAllCharacters()
        {
            List<characterModel> characterList = new List<characterModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_getAllCharacters";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtCharacters = new DataTable();

                connection.Open();
                sqlDA.Fill(dtCharacters);
                connection.Close();

                foreach (DataRow row in dtCharacters.Rows)
                {
                    characterList.Add(new characterModel
                    {
                        characterID = Convert.ToInt32(row["characterID"]),
                        characterName = row["characterName"].ToString(),
                        characterRace = row["characterRace"].ToString(),
                        characterClassOrType = row["characterClassOrType"].ToString(),
                        characterSubClass = row["characterSubClass"].ToString(),
                        characterDescription = row["characterDescription"].ToString(),
                        characterSkills = row["characterSkills"].ToString(),
                        characterImage = row["characterImage"].ToString(),
                        characterSTR = Convert.ToInt32(row["characterSTR"]),
                        characterDEX = Convert.ToInt32(row["characterDEX"]),
                        characterCON = Convert.ToInt32(row["characterCON"]),
                        characterINT = Convert.ToInt32(row["characterINT"]),
                        characterWIS = Convert.ToInt32(row["characterWIS"]),
                        characterCHA = Convert.ToInt32(row["characterCHA"]),
                        characterLevel = Convert.ToInt32(row["characterLevel"]),
                        characterInitiative = Convert.ToInt32(row["characterInitiative"]),
                        characterSpeed = Convert.ToInt32(row["characterSpeed"]),
                        characterAC = Convert.ToInt32(row["characterAC"]),
                        characterHP = Convert.ToInt32(row["characterHP"]),
                        characterIsPublic = Convert.ToInt32(row["characterIsPublic"]),
                        characterUser = row["characterUser"].ToString()
                    }); 
                }
            }
            return characterList;
        }

        //Insert Characters into database
        public bool insertCharacter(characterModel character)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_insertCharacter", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@characterName", character.characterName);
                command.Parameters.AddWithValue("@characterRace", character.characterRace);
                command.Parameters.AddWithValue("@characterClassorType", character.characterClassOrType);
                command.Parameters.AddWithValue("@characterSubClass", character.characterSubClass);
                command.Parameters.AddWithValue("@characterDescription", character.characterDescription);
                command.Parameters.AddWithValue("@characterSkills", character.characterSkills);
                command.Parameters.AddWithValue("@characterImage", character.characterImage);
                command.Parameters.AddWithValue("@characterSTR", character.characterSTR);
                command.Parameters.AddWithValue("@characterDEX", character.characterDEX);
                command.Parameters.AddWithValue("@characterCON", character.characterCON);
                command.Parameters.AddWithValue("@characterINT", character.characterINT);
                command.Parameters.AddWithValue("@characterWIS", character.characterWIS);
                command.Parameters.AddWithValue("@characterCHA", character.characterCHA);
                command.Parameters.AddWithValue("@characterLevel", character.characterLevel);
                command.Parameters.AddWithValue("@characterInitiative", character.characterInitiative);
                command.Parameters.AddWithValue("@characterSpeed", character.characterSpeed);
                command.Parameters.AddWithValue("@characterAC", character.characterAC);
                command.Parameters.AddWithValue("@characterHp", character.characterHP);
                command.Parameters.AddWithValue("@characterIsPublic", character.characterIsPublic);
                command.Parameters.AddWithValue("@characterUser", character.characterUser);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if(id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get characters by ID from the database
        public List<characterModel> getCharacterByID(int characterID)
        {
            List<characterModel> characterList = new List<characterModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_getCharacterByID";
                command.Parameters.AddWithValue("@characterID", characterID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtCharacters = new DataTable();

                connection.Open();
                sqlDA.Fill(dtCharacters);
                connection.Close();

                foreach (DataRow row in dtCharacters.Rows)
                {
                    characterList.Add(new characterModel
                    {
                        characterID = Convert.ToInt32(row["characterID"]),
                        characterName = row["characterName"].ToString(),
                        characterRace = row["characterRace"].ToString(),
                        characterClassOrType = row["characterClassOrType"].ToString(),
                        characterSubClass = row["characterSubClass"].ToString(),
                        characterDescription = row["characterDescription"].ToString(),
                        characterSkills = row["characterSkills"].ToString(),
                        characterImage = row["characterImage"].ToString(),
                        characterSTR = Convert.ToInt32(row["characterSTR"]),
                        characterDEX = Convert.ToInt32(row["characterDEX"]),
                        characterCON = Convert.ToInt32(row["characterCON"]),
                        characterINT = Convert.ToInt32(row["characterINT"]),
                        characterWIS = Convert.ToInt32(row["characterWIS"]),
                        characterCHA = Convert.ToInt32(row["characterCHA"]),
                        characterLevel = Convert.ToInt32(row["characterLevel"]),
                        characterInitiative = Convert.ToInt32(row["characterInitiative"]),
                        characterSpeed = Convert.ToInt32(row["characterSpeed"]),
                        characterAC = Convert.ToInt32(row["characterAC"]),
                        characterHP = Convert.ToInt32(row["characterHP"]),
                        characterIsPublic = Convert.ToInt32(row["characterIsPublic"]),
                        characterUser = row["characterUser"].ToString()
                    });
                }
            }
            return characterList;
        }

        // Updates Character in the database
        public bool updateCharacter(characterModel character)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_updateCharacter", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@characterID", character.characterID);
                command.Parameters.AddWithValue("@characterName", character.characterName);
                command.Parameters.AddWithValue("@characterRace", character.characterRace);
                command.Parameters.AddWithValue("@characterClassorType", character.characterClassOrType);
                command.Parameters.AddWithValue("@characterSubClass", character.characterSubClass);
                command.Parameters.AddWithValue("@characterDescription", character.characterDescription);
                command.Parameters.AddWithValue("@characterSkills", character.characterSkills);
                command.Parameters.AddWithValue("@characterImage", character.characterImage);
                command.Parameters.AddWithValue("@characterSTR", character.characterSTR);
                command.Parameters.AddWithValue("@characterDEX", character.characterDEX);
                command.Parameters.AddWithValue("@characterCON", character.characterCON);
                command.Parameters.AddWithValue("@characterINT", character.characterINT);
                command.Parameters.AddWithValue("@characterWIS", character.characterWIS);
                command.Parameters.AddWithValue("@characterCHA", character.characterCHA);
                command.Parameters.AddWithValue("@characterLevel", character.characterLevel);
                command.Parameters.AddWithValue("@characterInitiative", character.characterInitiative);
                command.Parameters.AddWithValue("@characterSpeed", character.characterSpeed);
                command.Parameters.AddWithValue("@characterAC", character.characterAC);
                command.Parameters.AddWithValue("@characterHp", character.characterHP);
                command.Parameters.AddWithValue("@characterIsPublic", character.characterIsPublic);
                command.Parameters.AddWithValue("@characterUser", character.characterUser);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Deletes character from the database
        public string deleteCharacter(int characterID)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_deleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@characterID", characterID);
                command.Parameters.Add("@RETURNMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@RETURNMESSAGE"].Value.ToString();
                connection.Close();
            }
            return result;
        }
    }
}
