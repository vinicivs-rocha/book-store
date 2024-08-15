namespace Book_Store;

public record BookDto
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required string Genre { get; init; }
    public required double Price { get; init; }
    public required int Quantity { get; init; }
};