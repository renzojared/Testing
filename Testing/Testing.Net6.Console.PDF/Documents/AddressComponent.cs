using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Testing.Net6.Console.PDF.Entities;

namespace Testing.Net6.Console.PDF.Documents;

public class AddressComponent : IComponent
{
    private string Title { get; }
    private Address Address { get; }

    public AddressComponent(string title, Address address)
    {
        Title = title;
        Address = address;
    }

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(2);
            col.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

            col.Item().Text(Address.CompanyName);
            col.Item().Text(Address.Street);
            col.Item().Text($"{Address.City}, {Address.State}");
            col.Item().Text(Address.Email);
            col.Item().Text(Address.Phone);
        });
    }
}
