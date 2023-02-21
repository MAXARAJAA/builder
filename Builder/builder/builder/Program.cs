using System.Text;

public class Product
{
    public string Name { get; set; }
    public double resolution { get; set; }
    public double size { get; set; }
    public int refreshRate { get; set; }
    public string PanelType { get; set; }
}
public class ProductMonitor
{

    public string BodyPart { get; set; }

    public override string ToString() =>
        new StringBuilder()

        .AppendLine(BodyPart)
        .ToString();
}
public interface IProductMonitorBuilder
{

    void BuildBody();


}
public class ProductMonitorBuilder : IProductMonitorBuilder
{
    private ProductMonitor _productMonitor;
    private IEnumerable<Product> _products;

    public ProductMonitorBuilder(IEnumerable<Product> products)
    {
        _products = products;
        _productMonitor = new ProductMonitor();
    }



    public void BuildBody()
    {
        _productMonitor.BodyPart = string.Join(Environment.NewLine, _products.Select(p => $"\nProduct name: {p.Name}," +
        $" \nproduct resolution: {p.resolution} , \nproduct size: {p.size}, \nproduct refreshRate: {p.refreshRate} , \nproduct PanelType: {p.PanelType}"));
    }



    public ProductMonitor GetReport()
    {
        var productMonitor = _productMonitor;
        Clear();
        return productMonitor;
    }

    private void Clear() => _productMonitor = new ProductMonitor();
}
public class ProductMonitorDirector
{
    private readonly IProductMonitorBuilder _productMonitorBuilder;

    public ProductMonitorDirector(IProductMonitorBuilder productMonitorBuilder)
    {
        _productMonitorBuilder = productMonitorBuilder;
    }

    public void BuildMonitor()
    {

        _productMonitorBuilder.BuildBody();

    }
}
class Program
{
    static void Main(string[] args)
    {
        var products = new List<Product>
        {
            new Product { Name = "Standart", resolution = 21 ,size = 20, refreshRate = 60 ,PanelType = "TN" },
            new Product { Name = "Gaming", resolution = 15.6,size = 30 , refreshRate = 144, PanelType = "VA"}

        };

        var builder = new ProductMonitorBuilder(products);
        var director = new ProductMonitorDirector(builder);
        director.BuildMonitor();

        var report = builder.GetReport();
        Console.WriteLine(report);
    }
}