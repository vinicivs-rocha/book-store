namespace Book_Store;

public record UpdateBookDto
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public string? Genre { get; init; }
    public double? Price { get; init; }
    public int? Quantity { get; init; }
};