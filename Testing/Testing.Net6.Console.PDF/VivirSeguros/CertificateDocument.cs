using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Testing.Net6.Console.PDF.VivirSeguros;

public class CertificateDocument : IDocument
{
    public static Image logo { get; } = Image.FromFile("C:\\Users\\rleon\\Downloads\\min.png");

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(pg =>
            {
                pg.Margin(35);
                pg.Size(PageSizes.A4);
                pg.Header().AlignTop().Element(HeaderCertificate);
                pg.Content();
                pg.Footer();
            });
    }

    void HeaderCertificate(IContainer cnt)
    {
        var styleDefaultLetter = TextStyle.Default.FontSize(16).SemiBold().FontColor(Colors.Pink.Medium);

        cnt.Row(row =>
        {
            row.RelativeItem().AlignLeft().Border(1).Column(c =>
            {
                c.Item().Width(275).Image(logo);
                c.Spacing(5);
                c.Item().Text("Certificado Electronico").Style(styleDefaultLetter);
                c.Item().Text("Decreto Supremo Nro 015-2016-MTC").Style(styleDefaultLetter);
            });

            row.ConstantItem(250).Border(1).AlignRight().Column(c =>
            {
                c.Item().Padding(1);
                c.Item().Text("SOAT").FontSize(50).SemiBold();
            });
        }); 

        //cnt.Row(row =>
        //{
        //    row.ConstantItem(260).Border(1).AlignLeft().Column(c =>
        //    {
        //        c.Spacing(5);
        //        c.Item().Text("Certificado Electronico").Style(styleDefaultLetter);
        //        c.Item().Text("Decreto Supremo Nro 015-2016-MTC").Style(styleDefaultLetter);
        //    });
        //    row.ConstantItem(260).Border(1).AlignRight().Column(c =>
        //    {
        //        c.Item().Padding(10);
        //        c.Item().Text("SOAT").FontSize(65).ExtraBold();
        //    });
        //
        //});

        /*
        cnt.Row(row =>
        {
            row.RelativeItem().Column(c =>
            {
                c.Item().Text("Certificado Electronico").Style(styleDefaultLetter);
                c.Item().Text("Decreto Supremo Nro 015-2016-MTC").Style(styleDefaultLetter);
            });
        });*/
    }
}
