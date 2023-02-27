using Firebase.Database

namespace KalterKrieg.Data
{
    public class Firebase
    {
        FirebaseClient client = new FirebaseClient("https://projekt-kalter-krieg-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<List<Ereignis>> getAllEreignisseSorted()
        {
            var events = await client.Child("events").OnceAsync<Ereignis>();

            return events?.Select(e => new Ereignis()
            {
                name = e.Key,
                dateStart = e.Object.dateStart,
                dateEnd = e.Object.dateEnd,
                shortSummary = e.Object.shortSummary
            }).ToList();
        }
    }
}
