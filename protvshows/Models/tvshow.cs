using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace protvshows.Models
{
	public class TVshowContext
	{
		public string ConnectionString { get; set; }

		public TVshowContext(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}

        public List<TVshow> GetAlltvshows()
		{
            List<TVshow> list = new List<TVshow>();

            using (MySqlConnection sqlConn = GetConnection())
			{
				sqlConn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT short_name, title, picture_path_main FROM tvshows;", sqlConn);
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
                        list.Add(new TVshow()
						{
                            // TODO perhaps, getting values from reader by names is slower that by index
							Short_name = reader.GetString("short_name"),
							Title = reader.GetString("title"),
							Picture_path_main = reader.GetString("picture_path_main")
						});
					}
				}
			}

			return list;
		}

        public TVshow Gettvshow(string short_name)
        {
            TVshow requestedTVshow = null;

            using (MySqlConnection sqlConn = GetConnection())
            {
                sqlConn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tvshow_rating WHERE short_name = @sn", sqlConn);
                cmd.Parameters.AddWithValue("@sn", short_name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        requestedTVshow = new TVshow()
                        {
                            //id = reader.GetInt64("id"),
                            IMDB_link_id = reader.GetString("imdb_link_id"),
                            //tmdb_link_id = reader.GetString("tmdb_link_id"),
                            //short_name = reader.GetString("short_name"),
                            Title = reader.GetString("title"),
                            Info = reader.GetString("info"),
                            Genre = reader.GetString("genre"),
                            Description = reader.GetString("description"),
                            //rating_id = reader.GetInt64("rating_id"),
                            //added_on_date = reader.GetDateTime("added_on_date"),
                            //added_by_user = reader.GetInt64("added_by_user"),
                            Picture_path_main = reader.GetString("picture_path_main"),
                            Picture_path_detailed = reader.GetString("picture_path_detailed"),
                            Verdict = reader.GetString("verdict"),
                            Rating_boy = reader.GetFloat("rating_boy"),
                            Rating_girl = reader.GetFloat("rating_girl"),
                            Rating_expert = reader.GetFloat("rating_expert"),
                            Rating_imdb = reader.GetFloat("rating_imdb"),
                            Rating_imdb_date = reader.GetDateTime("rating_imdb_date").ToString("dd/MM/yyyy"),
                            Link_trailer = reader.GetString("link_trailer"),
                            Link_discussion = reader.GetString("link_discussion").ToString()
                        };
                    }
                }
            }

            return requestedTVshow;
        }

  //      public TVshow Create(TVshow newTVshow)
		//{
		//	using (MySqlConnection sqlConn = GetConnection())
		//	{
		//		sqlConn.Open();
		//		MySqlCommand cmd = new MySqlCommand("INSERT INTO ratings FROM tvshow_model WHERE short_name = @sn", sqlConn);
		//		cmd.Parameters.AddWithValue("@sn", short_name);
		//		using (MySqlDataReader reader = cmd.ExecuteReader())
		//		{
		//			if (reader.Read())
		//			{
		//				requestedTVshow = new TVshow()
		//				{
		//					//id = reader.GetInt64("id"),
		//					IMDB_link_id = reader.GetString("imdb_link_id"),
		//					//tmdb_link_id = reader.GetString("tmdb_link_id"),
		//					//short_name = reader.GetString("short_name"),
		//					Title = reader.GetString("title"),
		//					Info = reader.GetString("info"),
		//					Genre = reader.GetString("genre"),
		//					Description = reader.GetString("description"),
		//					//rating_id = reader.GetInt64("rating_id"),
		//					//added_on_date = reader.GetDateTime("added_on_date"),
		//					//added_by_user = reader.GetInt64("added_by_user"),
		//					Picture_path_main = reader.GetString("picture_path_main"),
		//					Picture_path_detailed = reader.GetString("picture_path_detailed"),
		//					Verdict = reader.GetString("verdict"),
		//					Rating_boy = reader.GetFloat("rating_boy"),
		//					Rating_girl = reader.GetFloat("rating_girl"),
		//					Rating_expert = reader.GetFloat("rating_expert"),
		//					Rating_imdb = reader.GetFloat("rating_imdb"),
		//					Rating_imdb_date = reader.GetDateTime("rating_imdb_date").ToString("dd/MM/yyyy"),
		//					Link_trailer = reader.GetString("link_trailer"),
		//					Link_discussion = reader.GetString("link_discussion").ToString()
		//				};
		//			}
		//		}
		//	}

		//	return requestedTVshow;
		//}
    }

    public class TVshow
    {
        public long ID { get; set; }
        public string IMDB_link_id { get; set; }
        //public string TMDB_link_id { get; set; }
        public string Short_name { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public long Rating_id { get; set; }
        public string Added_on_date { get; set; }
        public long Added_by_user { get; set; }
        public string Picture_path_main { get; set; }
        public string Picture_path_detailed { get; set; }
        public string Verdict { get; set; }
        public float Rating_boy { get; set; }
        public float Rating_girl { get; set; }
        public float Rating_expert { get; set; }
        public float Rating_imdb { get; set; }
        public string Rating_imdb_date { get; set; }
        public string Link_trailer { get; set; }
        public string Link_discussion { get; set; }
    }
}
