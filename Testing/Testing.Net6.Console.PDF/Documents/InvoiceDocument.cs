using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Testing.Net6.Console.PDF.Entities;

namespace Testing.Net6.Console.PDF.Documents;

internal class InvoiceDocument : IDocument
{
    public InvoiceModel ModelGeneric { get; }

    public InvoiceDocument(InvoiceModel modelGeneric)
    {
        ModelGeneric = modelGeneric;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(pg =>
            {
                pg.Margin(50);
                pg.Header().Element(ComposeHeader);
                pg.Content().Element(ComposeContent);
                pg.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
    }

    void ComposeHeader(IContainer cnt)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Pink.Medium);

        cnt.Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().Text($"Invoice #{ModelGeneric.InvoiceNumber}").Style(titleStyle);

                col.Item().Text(txt =>
                {
                    txt.Span("Issue date: ").SemiBold();
                    txt.Span($"{ModelGeneric.IssueDate:d}");
                });

                col.Item().Text(txt =>
                {
                    txt.Span("Due date: ").SemiBold();
                    txt.Span($"{ModelGeneric.DueDate:d}");
                });
            });

            row.ConstantItem(100).Height(50).Placeholder();
        });
    }

    void ComposeContent(IContainer cnt)
    {
        cnt.PaddingVertical(40).Column(col =>
        {
            col.Spacing(5);

            col.Item().Row(row =>
            {
                row.RelativeItem().Component(new AddressComponent("From", ModelGeneric.SellerAddress));
                row.ConstantItem(50);
                row.RelativeItem().Component(new AddressComponent("For", ModelGeneric.CustomerAddress));
            });

            col.Item().Element(ComposeTable);

            var totalPrice = ModelGeneric.Items.Sum(c => c.Price * c.Quantity);
            col.Item().AlignRight().Text($"Grand total: {totalPrice}$").FontSize(14);

            if (!string.IsNullOrWhiteSpace(ModelGeneric.Comments))
                col.Item().PaddingTop(25).Element(ComposeComments);
        });
    }
    void ComposeTable(IContainer cnt)
    {
        cnt.Table(tbl =>
        {
            tbl.ColumnsDefinition(col =>
            {
                col.ConstantColumn(25);
                col.RelativeColumn(3);
                col.RelativeColumn();
                col.RelativeColumn();
                col.RelativeColumn();
            });

            tbl.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).Text("Product");
                header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                header.Cell().Element(CellStyle).AlignRight().Text("Total");

                static IContainer CellStyle(IContainer cnt)
                {
                    var retrn = cnt.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    return retrn;
                }
            });

            foreach (var item in ModelGeneric.Items)
            {
                tbl.Cell().Element(CellStyle).Text(ModelGeneric.Items.IndexOf(item) + 1);
                tbl.Cell().Element(CellStyle).Text(item.Name);
                tbl.Cell().Element(CellStyle).AlignRight().Text($"{item.Price}$");
                tbl.Cell().Element(CellStyle).AlignRight().Text(item.Quantity);
                tbl.Cell().Element(CellStyle).AlignRight().Text($"{item.Price * item.Quantity}$");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
        });
    }

    void ComposeComments(IContainer cnt)
    {
        cnt.Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
        {
            col.Spacing(5);
            col.Item().Text("Comments").FontSize(14);
            col.Item().Text(ModelGeneric.Comments);
        });
    }
}
