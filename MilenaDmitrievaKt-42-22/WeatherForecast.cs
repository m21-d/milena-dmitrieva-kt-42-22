namespace MilenaDmitrievaKt_42_22
{
    public class WeatherForecast
    {
        //123456
        //123
        //321
        //5
        //11
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}