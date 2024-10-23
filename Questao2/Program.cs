using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
     public static async Task Main(string[] args)
    {
		string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

		teamName = "Barcelona";
		year = 2011;
		totalGoals = await getTotalScoredGoals(teamName, year);
		Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

		// Output expected:
		// Team Paris Saint - Germain scored 109 goals in 2013
		// Team Chelsea scored 92 goals in 2014
	}

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
		int numeroGols = 0;
		int pagina = 1;
		bool maisPaginas;

		do {
			string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={pagina}";
			using(HttpClient client = new HttpClient()) {
				var response = await client.GetStringAsync(url);
				JObject jsonResponse = JObject.Parse(response);
				JArray matches = (JArray)jsonResponse["data"];

				foreach(var match in matches) {
					if(match["team1"].ToString() == team)
						numeroGols += int.Parse(match["team1goals"].ToString());
				}

				maisPaginas = pagina < (int)jsonResponse["total_pages"];
				pagina++;
			}
		} while(maisPaginas);

		pagina = 1;
		do {
			string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={pagina}";
			using(HttpClient client = new HttpClient()) {
				var response = await client.GetStringAsync(url);
				JObject jsonResponse = JObject.Parse(response);
				JArray matches = (JArray)jsonResponse["data"];

				foreach(var match in matches) {
					if(match["team2"].ToString() == team)
						numeroGols += int.Parse(match["team2goals"].ToString());
				}

				maisPaginas = pagina < (int)jsonResponse["total_pages"];
				pagina++;
			}
		} while(maisPaginas);

		return numeroGols;
	}

}