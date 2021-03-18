using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Roommates.Models;


namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        /// <summary>
        ///  When new RoomRepository is instantiated, pass the connection string along to the BaseRepository
        /// </summary>
        public RoommateRepository(string connectonString) : base(connectonString) { }

        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    //When you add a menu option for searching for a roommate by their Id, the output to the screen should output their first name,
                    //their rent portion, and the name of the room they occupy. Hint: You'll want to use a JOIN statement in your SQL query
                    cmd.CommandText = @"SELECT Roommate.Id, FirstName, RentPortion, Room.Name
                                       FROM Roommate 
                                       JOIN Room ON Room.id = Roommate.RoomId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Roommate.Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("FirstName");
                        string nameValue = reader.GetString(nameColumnPosition);

                        Roommate roommate = new Roommate
                        {
                            Id= idValue,
                            FirstName = nameValue,

                        }
                    }

                }
            }
        }

    }
}
