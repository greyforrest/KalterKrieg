using Firebase.Database;
using System;
using System.Globalization;

namespace KalterKrieg.Data
{
    public class FirebaseService
    {
        FirebaseClient client = new FirebaseClient("https://projekt-kalter-krieg-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<List<Ereignis>> getAllEreignisseSorted()
        {
            var events = await client.Child("events").OnceAsync<Ereignis>();

            List<Ereignis> allEvents = events?.Select(e => new Ereignis()
            {
                name = e.Key,
                dateStart = e.Object.dateStart,
                dateEnd = e.Object.dateEnd,
                shortSummary = e.Object.shortSummary
            }).ToList();

            foreach( Ereignis e in allEvents)
            {
                try
                {
                    e.end = DateTime.ParseExact(e.dateEnd, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                } catch
                {
                    e.end = DateTime.ParseExact(e.dateEnd, "yyyy", CultureInfo.InvariantCulture);

                }

                try
                {
                    e.start = DateTime.ParseExact(e.dateStart, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                } catch
                {
                    e.start = DateTime.ParseExact(e.dateStart, "yyyy", CultureInfo.InvariantCulture);
                }
            }
            allEvents.Sort((x,y) => x.start.CompareTo(y.start));
            return allEvents;
        }

        public async Task<List<Frage>> getQuiz()
        {
            var quiz = await client.Child("quiz").OnceAsync<Frage>();

            return quiz?.Select(e => new Frage()
            {
                question = e.Key,
                answers = e.Object.answers,
                correctAnswer = e.Object.correctAnswer
            }).ToList();
        }
    }
}
