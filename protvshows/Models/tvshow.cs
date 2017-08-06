using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace protvshows.Models
{
	public class tvshowContext
	{
		public string ConnectionString { get; set; }

		public tvshowContext(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}

        public List<tvshow> GetAlltvshows()
		{
            List<tvshow> list = new List<tvshow>();

            using (MySqlConnection sqlConn = GetConnection())
			{
				sqlConn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tvshow_model;", sqlConn);
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
                        list.Add(new tvshow()
						{
                            // perhaps, getting values from reader by names is slower that by index
							//id = reader.GetInt64("id"),
                            //imdb_link_id = reader.GetString("imdb_link_id"),
                            //tmdb_link_id = reader.GetString("tmdb_link_id"),
							short_name = reader.GetString("short_name"),
							title = reader.GetString("title"),
							//info = reader.GetString("info"),
							//genre = reader.GetString("genre"),
							//description = reader.GetString("description"),
							//link_trailer = reader.GetString("link_trailer"),
							//rating_id = reader.GetInt64("rating_id"),
							//added_on_date = reader.GetDateTime("added_on_date"),
							//added_by_user = reader.GetInt64("added_by_user"),
							picture_path_main = reader.GetString("picture_path_main"),
                            //picture_path_detailed = reader.GetString("picture_path_detailed"),
                            //verdict = reader.GetString("verdict")
						});
					}
				}
			}

			return list;
		}

        public tvshow Gettvshow(string short_name)
        {
            tvshow requestedTVshow = null;

            using (MySqlConnection sqlConn = GetConnection())
            {
                sqlConn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tvshow_model WHERE short_name = @sn", sqlConn);
                cmd.Parameters.AddWithValue("@sn", short_name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        requestedTVshow = new tvshow()
                        {
                            //id = reader.GetInt64("id"),
                            imdb_link_id = reader.GetString("imdb_link_id"),
                            //tmdb_link_id = reader.GetString("tmdb_link_id"),
                            //short_name = reader.GetString("short_name"),
                            title = reader.GetString("title"),
                            info = reader.GetString("info"),
                            genre = reader.GetString("genre"),
                            description = reader.GetString("description"),
                            //rating_id = reader.GetInt64("rating_id"),
                            //added_on_date = reader.GetDateTime("added_on_date"),
                            //added_by_user = reader.GetInt64("added_by_user"),
                            picture_path_main = reader.GetString("picture_path_main"),
                            picture_path_detailed = reader.GetString("picture_path_detailed"),
                            verdict = reader.GetString("verdict"),
                            rating_boy = reader.GetFloat("rating_boy"),
                            rating_girl = reader.GetFloat("rating_girl"),
                            rating_expert = reader.GetFloat("rating_expert"),
                            rating_imdb = reader.GetFloat("rating_imdb"),
                            rating_imdb_date = reader.GetDateTime("rating_imdb_date").ToString("dd/MM/yyyy"),
                            link_trailer = reader.GetString("link_trailer"),
                            link_discussion = reader.GetString("link_discussion").ToString()
                        };
                    }
                }
            }

            return requestedTVshow;
        }
    }

    public class tvshow
    {
		public long id { get; set; }
		public string imdb_link_id { get; set; }
        //public string tmdb_link_id { get; set; }
        public string short_name { get; set; }
        public string title { get; set; }
        public string info { get; set; }
        public string genre { get; set; }
        public string description { get; set; }
        public long rating_id { get; set; }
        public string added_on_date { get; set; }
        public long added_by_user { get; set; }
        public string picture_path_main { get; set; }
        public string picture_path_detailed { get; set; }
        public string verdict { get; set; }
        public float rating_boy { get; set; }
        public float rating_girl { get; set; }
        public float rating_expert { get; set; }
        public float rating_imdb { get; set; }
        public string rating_imdb_date { get; set; }
        public string link_trailer { get; set; }
        public string link_discussion { get; set; }
    }
}
