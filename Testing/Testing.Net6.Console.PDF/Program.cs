// See https://aka.ms/new-console-template for more information
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Testing.Net6.Console.PDF.VivirSeguros;

QuestPDF.Settings.License = LicenseType.Community;

/*
var filePath = "/Users/renzojared/Downloads/untitled\ folder";

var model = InvoiceDocumentDataSource.GetInvoiceDetails();
var document = new InvoiceDocument(model);
document.GeneratePdf(filePath);

Process.Start("explorer.exe", filePath);*/

//Byte[] byteJson = File.ReadAllBytes("/Users/renzojared/Downloads/logo.png");
//var jsonBase64 = Convert.ToBase64String(byteJson);

var filePath = "C:/Users/rleon/Downloads/Test/ejem.pdf";
var document = new CertificateDocument();

document.GeneratePdf(filePath);

Byte[] pdf = document.GeneratePdf();
var stirng = Convert.ToBase64String(pdf);

Console.WriteLine(stirng);
Console.ReadLine();

//Process.Start("explorer.exe", filePath);

/*
Document.Create(cont =>
{
    cont.Page(pg =>
    {
        pg.Size(PageSizes.A5);
        pg.Margin(10, Unit.Millimetre);
        pg.PageColor(Colors.White);
        pg.DefaultTextStyle(x => x.FontSize(14));

        pg.Header()
        .Text("Compania de Seguros")
        .SemiBold().FontSize(25).FontColor(Colors.Blue.Medium);

        pg.Content()
        .PaddingVertical(1, Unit.Centimetre)
        .Column(x =>
        {
            x.Spacing(20);
            x.Item().Text(Placeholders.LoremIpsum());
            x.Item().Image(Placeholders.Image(1, 1));
        });

        pg.Footer()
        .AlignCenter()
        .Text(x =>
        {
            x.Span("Page ");
            x.CurrentPageNumber();
        });
    });
}).GeneratePdf("C:\\Users\\rleon\\Downloads\\miprimer.pdf");
*/
