using System;
using System.Collections.Generic;
using System.Linq;

internal static class ListGenerators
{
    public static List<Product> ProductList { get; } = new()
    {
        new("Chai", "Beverages", 18m, 39),
        new("Chang", "Beverages", 19m, 17),
        new("Aniseed Syrup", "Condiments", 10m, 13),
        new("Chef Anton's Cajun Seasoning", "Condiments", 22m, 53),
        new("Ikura", "Seafood", 31m, 20),
        new("Konbu", "Seafood", 6m, 24)
    };

    public static List<Customer> CustomerList { get; } = new()
    {
        new("ALFKI", new List<Order> { new(new DateTime(1997, 8, 25)) }),
        new("ANATR", new List<Order> { new(new DateTime(1996, 9, 18)) })
    };
}

internal sealed record Product(string ProductName, string Category, decimal UnitPrice, int UnitsInStock);

internal sealed record Customer(string CustomerID, List<Order> Orders);

internal sealed record Order(DateTime OrderDate);

namespace Linq_ConsoleApp7
{
internal static class Program
{
    private static void Main()
    {
        var q1 = ListGenerators.ProductList.Where(p => p.Category == "Seafood");
        foreach (var p in q1) Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

        var q2 = ListGenerators.ProductList.Select(p => p.ProductName);
        foreach (var name in q2) Console.WriteLine(name);

        var q3 = ListGenerators.ProductList.OrderBy(p => p.UnitPrice);
        foreach (var p in q3) Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

        var q4 = ListGenerators.ProductList.Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);
        foreach (var p in q4) Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

        var q5 = ListGenerators.ProductList.Where(p => p.UnitsInStock > 0 && p.Category == "Condiments");
        foreach (var p in q5) Console.WriteLine($"{p.ProductName} - Stock: {p.UnitsInStock}");

        var q6 = ListGenerators.ProductList.Select(p => new
        {
            Name = p.ProductName,
            Price = p.UnitPrice,
            StockStatus = p.UnitsInStock > 0 ? "Available" : "Out of Stock"
        });
        foreach (var p in q6) Console.WriteLine($"{p.Name} - {p.Price} - {p.StockStatus}");

        var q7 = ListGenerators.ProductList.Select((p, i) => new { Index = i + 1, p.ProductName });
        foreach (var p in q7) Console.WriteLine($"{p.Index}. {p.ProductName}");

        var q8 = ListGenerators.ProductList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
        foreach (var p in q8) Console.WriteLine($"{p.Category} - {p.ProductName} - {p.UnitPrice}");

        var q9 = ListGenerators.ProductList.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitsInStock);
        foreach (var p in q9) Console.WriteLine($"{p.ProductName} - Stock: {p.UnitsInStock}");

        var q10 = from c in ListGenerators.CustomerList
                  from o in c.Orders
                  where o.OrderDate.Year >= 1997
                  select new { c.CustomerID, o.OrderDate };
        foreach (var x in q10) Console.WriteLine($"{x.CustomerID} - {x.OrderDate:yyyy-MM-dd}");

        var q11 = ListGenerators.ProductList.Select((p, i) => new { Position = i + 1, p.ProductName });
        foreach (var p in q11) Console.WriteLine($"{p.Position} - {p.ProductName}");

        string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
        var q12 = words.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
        foreach (var word in q12) Console.WriteLine(word);

        var q13 = words.Where(w => w.Length > 1 && char.ToLower(w[1]) == 'i').Reverse();
        foreach (var word in q13) Console.WriteLine(word);
    }
}
}
