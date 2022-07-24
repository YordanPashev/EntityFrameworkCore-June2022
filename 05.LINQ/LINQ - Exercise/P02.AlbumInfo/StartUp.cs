namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            string result = ExportAlbumsInfo(context, 9);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var producer = context.Producers
                .FirstOrDefault(p => p.Id == producerId);

            string producerName = producer.Name;

            StringBuilder output = new StringBuilder();

            foreach (var album in producer.Albums.OrderByDescending(a => a.Price))
            {
                string albumReleasDate = album.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                output.AppendLine($"-AlbumName: {album.Name}")
                      .AppendLine($"-ReleaseDate: {albumReleasDate}")
                      .AppendLine($"-ProducerName: {producerName}")
                      .AppendLine("-Songs:");

                int songCounter = 0;

                foreach (var song in album.Songs
                                            .OrderByDescending(s => s.Name)
                                            .ThenBy(s => s.Writer))
                {
                    songCounter++;
                    output.AppendLine($"---#{songCounter}")
                          .AppendLine($"---SongName: {song.Name}")
                          .AppendLine($"---Price: {song.Price:F2}")
                          .AppendLine($"---Writer: {song.Writer.Name}");
                }

                output.AppendLine($"-AlbumPrice: {album.Price:F2}");
            }

            return output.ToString().TrimEnd();

            ////Alternative Solution
            //var albums = context.Albums
            //                .Where(p => p.ProducerId == producerId)
            //                .ToArray()
            //                .OrderByDescending(a => a.Price)
            //                .Select(a => new
            //                {
            //                    AlbumName = a.Name,
            //                    AlbumReleaseDate = a.ReleaseDate,
            //                    ProducerName = a.Producer.Name,
            //                    Songs = a.Songs.Select(s => new
            //                    {
            //                        SongName = s.Name.ToString(),
            //                        SonPrice = s.Price,
            //                        SongWriter = s.Writer.Name.ToString()
            //                    }).ToArray()
            //                    .OrderByDescending(s => s.SongName)
            //                    .ThenBy(s => s.SongWriter),
            //                    AlbumPrice = a.Price
            //                })
            //                .ToArray();

            //StringBuilder output = new StringBuilder();

            //foreach (var album in albums)
            //{
            //    string albumReleasDate = album.AlbumReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            //    output.AppendLine($"-AlbumName: {album.AlbumName}")
            //          .AppendLine($"-ReleaseDate: {albumReleasDate}")
            //          .AppendLine($"-ProducerName: {album.ProducerName}")
            //          .AppendLine("-Songs:");

            //    int songCounter = 0;

            //    foreach (var song in album.Songs)
            //    {
            //        songCounter++;
            //        output.AppendLine($"---#{songCounter}")
            //              .AppendLine($"---SongName: {song.SongName}")
            //              .AppendLine($"---Price: {song.SonPrice:F2}")
            //              .AppendLine($"---Writer: {song.SongWriter}");
            //    }

            //    output.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            //}

            //return output.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}




