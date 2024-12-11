namespace BlazorApp1.Models.Menu;

public class MenuItem
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string IconClass { get; set; }
    public string Badge { get; set; }
    public List<MenuItem> Children { get; set; } = new();
}
