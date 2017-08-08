using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                MySqlCommand cmd = new MySqlCommand("SELECT tv.id, tv.short_name, tv.title, tv.picture_path_main, tv.added_on_date, u.name FROM tvshows AS tv JOIN users AS u ON tv.added_by_user = u.id;", sqlConn);
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
                        list.Add(new TVshow()
						{
                            // TODO perhaps, getting values from reader by names is slower that by index
                            ID = reader.GetInt64("id"),
                            Added_on_date = reader.GetDateTime("added_on_date").ToString("dd/MM/yyyy"),
                            Added_by_username = reader.GetString("name"),
							Title = reader.GetString("title"),
							Picture_path_main = reader.GetString("picture_path_main"),
                            Short_name = reader.GetString("short_name")
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

        public void Create(TVshow newTVshow)
		{
			using (MySqlConnection sqlConn = GetConnection())
			{
				sqlConn.Open();
                // TODO move the inserting to the stored procedure
				MySqlCommand cmd = new MySqlCommand(
					@"INSERT INTO tvshows(imdb_link_id, tmdb_link_id, short_name, title, info, genre, description, added_on_date, added_by_user, link_trailer, link_discussion, rating_boy, rating_girl, rating_expert, rating_imdb, rating_imdb_date, dynamism_boy, dynamism_girl, dynamism_expert, realism_boy, realism_girl, realism_expert, originality_boy, originality_girl, originality_expert, involvement_boy, involvement_girl, involvement_expert, visual_boy, visual_girl, visual_expert, actors_boy, actors_girl, actors_expert, music_boy, music_girl, music_expert, humor_boy, humor_girl, humor_expert, verdict_id, picture_path_main, picture_path_detailed) VALUES (
                        @imdb_link_id,
                        @tmdb_link_id,
                        @short_name,
                        @title,
                        @info,
                        @genre,
                        @description,
                        @added_on_date,
                        @added_by_user,
                        @link_trailer,
                        @link_discussion,
                        @rating_boy,
                        @rating_girl,
                        @rating_expert,
                        @rating_imdb,
                        @rating_imdb_date,
                        @dynamism_boy,
                        @dynamism_girl,
                        @dynamism_expert,
                        @realism_boy,
                        @realism_girl,
                        @realism_expert,
                        @originality_boy,
                        @originality_girl,
                        @originality_expert,
                        @involvement_boy,
                        @involvement_girl,
                        @involvement_expert,
                        @visual_boy,
                        @visual_girl,
                        @visual_expert,
                        @actors_boy,
                        @actors_girl,
                        @actors_expert,
                        @music_boy,
                        @music_girl,
                        @music_expert,
                        @humor_boy,
                        @humor_girl,
                        @humor_expert,
                        @verdict_id,
                        @picture_path_main,
                        @picture_path_detailed
                    )",
                    sqlConn
                );
                
                cmd.Parameters.AddWithValue("@imdb_link_id", newTVshow.IMDB_link_id);
                cmd.Parameters.AddWithValue("@tmdb_link_id", null);
                cmd.Parameters.AddWithValue("@short_name", newTVshow.Short_name);
                cmd.Parameters.AddWithValue("@title", newTVshow.Title);
                cmd.Parameters.AddWithValue("@info", newTVshow.Info);
                cmd.Parameters.AddWithValue("@genre", newTVshow.Genre);
                cmd.Parameters.AddWithValue("@description", newTVshow.Description);
                cmd.Parameters.AddWithValue("@added_on_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@added_by_user", 1);
                cmd.Parameters.AddWithValue("@link_trailer", newTVshow.Link_trailer);
                cmd.Parameters.AddWithValue("@link_discussion", newTVshow.Link_discussion);
                cmd.Parameters.AddWithValue("@rating_boy", newTVshow.Rating_boy);
                cmd.Parameters.AddWithValue("@rating_girl", newTVshow.Rating_girl);
                cmd.Parameters.AddWithValue("@rating_expert", newTVshow.Rating_expert);
                cmd.Parameters.AddWithValue("@rating_imdb", newTVshow.Rating_imdb);
                cmd.Parameters.AddWithValue("@rating_imdb_date", newTVshow.Rating_imdb_date);
                cmd.Parameters.AddWithValue("@dynamism_boy", newTVshow.Dynamism_boy);
                cmd.Parameters.AddWithValue("@dynamism_girl", newTVshow.Dynamism_girl);
                cmd.Parameters.AddWithValue("@dynamism_expert", newTVshow.Dynamism_expert);
                cmd.Parameters.AddWithValue("@realism_boy", newTVshow.Realism_boy);
                cmd.Parameters.AddWithValue("@realism_girl", newTVshow.Realism_girl);
                cmd.Parameters.AddWithValue("@realism_expert", newTVshow.Realism_expert);
                cmd.Parameters.AddWithValue("@originality_boy", newTVshow.Originality_boy);
                cmd.Parameters.AddWithValue("@originality_girl", newTVshow.Originality_girl);
                cmd.Parameters.AddWithValue("@originality_expert", newTVshow.Originality_expert);
                cmd.Parameters.AddWithValue("@involvement_boy", newTVshow.Involvement_boy);
                cmd.Parameters.AddWithValue("@involvement_girl", newTVshow.Involvement_girl);
                cmd.Parameters.AddWithValue("@involvement_expert", newTVshow.Involvement_expert);
                cmd.Parameters.AddWithValue("@visual_boy", newTVshow.Visual_boy);
                cmd.Parameters.AddWithValue("@visual_girl", newTVshow.Visual_girl);
                cmd.Parameters.AddWithValue("@visual_expert", newTVshow.Visual_expert);
                cmd.Parameters.AddWithValue("@actors_boy", newTVshow.Actors_boy);
                cmd.Parameters.AddWithValue("@actors_girl", newTVshow.Actors_girl);
                cmd.Parameters.AddWithValue("@actors_expert", newTVshow.Actors_expert);
                cmd.Parameters.AddWithValue("@music_boy", newTVshow.Music_boy);
                cmd.Parameters.AddWithValue("@music_girl", newTVshow.Music_girl);
                cmd.Parameters.AddWithValue("@music_expert", newTVshow.Music_expert);
                cmd.Parameters.AddWithValue("@humor_boy", newTVshow.Humor_boy);
                cmd.Parameters.AddWithValue("@humor_girl", newTVshow.Humor_girl);
                cmd.Parameters.AddWithValue("@humor_expert", newTVshow.Humor_expert);
                cmd.Parameters.AddWithValue("@verdict_id", newTVshow.Verdict_id);
                cmd.Parameters.AddWithValue("@picture_path_main", newTVshow.Picture_path_main);
                cmd.Parameters.AddWithValue("@picture_path_detailed", newTVshow.Picture_path_detailed);

                cmd.ExecuteNonQuery();
			}
		}
    }

    // TODO create a dictionary for custom/localized validation error messages
    public class TVshow
    {
        // --- сериал

        public long ID { get; set; }
        [Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Display(Name = "Идентификатор IMDB", Prompt = "ID из ссылки на IMDB, например: tt1898069")]
        public string IMDB_link_id { get; set; }
		//public string TMDB_link_id { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(100)]
        [Display(Name = "Короткое название", Prompt = "короткое название сериала через дефис, например: american-gods")]
        public string Short_name { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(200)]
        [Display(Name = "Полное название", Prompt = "полное название сериала, например: American Gods")]
        public string Title { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(1000)]
        [Display(Name = "Информация", Prompt = "2017 год, 1 сезон из 7 серий по 60 минут...")]
        public string Info { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(100)]
        [Display(Name = "Жанр", Prompt = "муниципальная комедия с элементами трагитриллера")]
        public string Genre { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(5000)]
        [Display(Name = "Описание", Prompt = "Если вы с удивлением наблюдаете, что в 7-часовом фильме одновременно играют Николь Кидман, Риз Уизерспун и Шайлин Вудли, значит...")]
        public string Description { get; set; }

        public string Added_on_date { get; set; }
        public long Added_by_user { get; set; }
        public string Added_by_username { get; set; }
		
        [Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Url]
		[StringLength(500)]
        [Display(Name = "Ссылка на трейлер")]
		public string Link_trailer { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Url]
		[StringLength(500)]
        [Display(Name = "Ссылка на обсуждение")]
		public string Link_discussion { get; set; }

		// --- рейтинг

		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(500)]
        [Display(Name = "Главная картинка рейтинга")]
        public string Picture_path_main { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[StringLength(500)]
        [Display(Name = "Подробная картинка рейтинга")]
        public string Picture_path_detailed { get; set; }
        [Display(Name = "Вердикт")]
        public string Verdict { get; set; }
        [Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
        [Display(Name = "Мужской рейтинг")]
        public float? Rating_boy { get; set; }
        [Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
        [Display(Name = "Женский рейтинг")]
        public float? Rating_girl { get; set; }
        [Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
        [Display(Name = "Экспертный рейтинг")]
        public float? Rating_expert { get; set; }
        [Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
        [Display(Name = "Рейтинг на IMDB")]
        public float? Rating_imdb { get; set; }
        [Display(Name = "Дата рейтинга на IMDB")]
        public string Rating_imdb_date { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Display(Name = "Вердикт")]
        public string Verdict_id { get; set; }

        // --- метрики рейтинга

        // динамизм
        [Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Динамизм, мужчины")]
		public float? Dynamism_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Динамизм, женщины")]
		public float? Dynamism_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Динамизм, эксперты")]
		public float? Dynamism_expert { get; set; }

		// реалистичность
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Реалистичность, мужчины")]
		public float? Realism_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Реалистичность, женщины")]
		public float? Realism_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Реалистичность, эксперты")]
		public float? Realism_expert { get; set; }

		// оригинальность
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Оригинальность, мужчины")]
		public float? Originality_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Оригинальность, женщины")]
		public float? Originality_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Оригинальность, эксперты")]
		public float? Originality_expert { get; set; }

		// вовлечённость
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Вовлечённость, мужчины")]
		public float? Involvement_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Вовлечённость, женщины")]
		public float? Involvement_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Вовлечённость, эксперты")]
		public float? Involvement_expert { get; set; }

		// картинка
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Картинка, мужчины")]
		public float? Visual_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Картинка, женщины")]
		public float? Visual_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Картинка, эксперты")]
		public float? Visual_expert { get; set; }

		// актёрский состав
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Актёрский состав, мужчины")]
		public float? Actors_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Актёрский состав, женщины")]
		public float? Actors_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Актёрский состав, эксперты")]
		public float? Actors_expert { get; set; }

		// музыка
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Музыка, мужчины")]
		public float? Music_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Музыка, женщины")]
		public float? Music_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Музыка, эксперты")]
		public float? Music_expert { get; set; }

		// юмор
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
        [Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Юмор, мужчины")]
		public float? Humor_boy { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Юмор, женщины")]
		public float? Humor_girl { get; set; }
		[Required(ErrorMessage = "[{0}] обязательно для заполнения")]
		[Range(0, 10.0, ErrorMessage = "[{0}] должно быть между {1} и {2}")]
		[Display(Name = "Юмор, эксперты")]
		public float? Humor_expert { get; set; }
    }
}
