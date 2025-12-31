// Models/WeatherData.cs
namespace DayliMvc.Models.WeatherData;

public class WeatherDataFront
{
    public DateTime createdTime { get; set; }
    public DateTime referenceTime { get; set; }
    public Geometry? geometry { get; set; }
    public List<TimeSeries>? timeSeries { get; set; }
}

public class Geometry
{
    public string type { get; set; } = string.Empty;
    public double[] coordinates { get; set; } = Array.Empty<double>();
}
public class TimeSeries
{
    public DateTime time { get; set; }
    public DateTime intervalParametersStartTime { get; set; }
    public Data? data { get; set; }
}

public class Data
{
    public double air_temperature { get; set; }
    public int wind_from_direction { get; set; }
    public double wind_speed { get; set; }
    public double wind_speed_of_gust { get; set; }
    public int relative_humidity { get; set; }
    public double air_pressure_at_mean_sea_level { get; set; }
    public double visibility_in_air { get; set; }
    public double thunderstorm_probability { get; set; }
    public double probability_of_frozen_precipitation { get; set; }
    public int cloud_area_fraction { get; set; }
    public int low_type_cloud_area_fraction { get; set; }
    public int medium_type_cloud_area_fraction { get; set; }
    public int high_type_cloud_area_fraction { get; set; }
    public int cloud_base_altitude { get; set; }
    public int cloud_top_altitude { get; set; }
    public double precipitation_amount_mean_deterministic { get; set; }
    public double precipitation_amount_mean { get; set; }
    public double precipitation_amount_min { get; set; }
    public double precipitation_amount_max { get; set; }
    public double precipitation_amount_median { get; set; }
    public int probability_of_precipitation { get; set; }
    public double precipitation_frozen_part { get; set; }
    public double predominant_precipitation_type_at_surface { get; set; }
    public double symbol_code { get; set; }
}