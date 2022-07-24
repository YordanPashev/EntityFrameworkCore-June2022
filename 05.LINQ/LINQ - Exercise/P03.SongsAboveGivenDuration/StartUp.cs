namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            string result = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            throw new NotImplementedException();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        { 
            TimeSpan minDuration = TimeSpan.FromSeconds(duration);
            var selectedSong = context.Songs
                .Include(s => s.SongPerformers)
                .ThenInclude(s => s.Performer)
                .Include(s => s.Album)
                .ThenInclude(s => s.Producer)
                .Include(s => s.Writer)
                .Where(s => s.Duration > minDuration)
                .Select(s => new
                {
                    SongName = s.Name,
                    Writer = s.Writer.Name,
                    Performer = $"{s.SongPerformers.FirstOrDefault().Performer.FirstName}" + " " +
                                $"{s.SongPerformers.FirstOrDefault().Performer.LastName}",
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c")
                })
                .ToArray()
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer);
                

            StringBuilder output = new StringBuilder();
            int songNumber = 0;

            foreach (var song in selectedSong)
            {
                songNumber++;
                output.AppendLine($"-Song #{songNumber}")
                      .AppendLine($"---SongName: {song.SongName}")
                      .AppendLine($"---Writer: {song.Writer}")
                      .AppendLine($"---Performer: {song.Performer}")
                      .AppendLine($"---AlbumProducer: {song.AlbumProducer}")
                      .AppendLine($"---Duration: {song.Duration}");
            }

            return output.ToString().Trim();

            ////AlternativeSolution
            //var selectedSong = context.Songs
            //        .Include(s => s.SongPerformers)
            //        .ThenInclude(s => s.Performer)
            //        .Include(s => s.Album)
            //        .ThenInclude(s => s.Producer)
            //        .Include(s => s.Writer)
            //        .Where(s => s.Duration > minDuration)
            //        .Select(s => new
            //        {
            //            SongName = s.Name,
            //            Writer = s.Writer.Name,
            //            Performer = s.SongPerformers
            //            .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
            //            .FirstOrDefault(),
            //            AlbumProducer = s.Album.Producer.Name,
            //            Duration = s.Duration.ToString("c")
            //        })
            //        .ToArray()
            //        .OrderBy(s => s.SongName)
            //        .ThenBy(s => s.Writer)
            //        .ThenBy(s => s.Performer);
        }
    }
}




