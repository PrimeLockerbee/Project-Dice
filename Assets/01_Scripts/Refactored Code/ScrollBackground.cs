using iTextSharp.text;
using iTextSharp.text.pdf;

public class ScrollBackground : PdfPageEventHelper
{
    private Image background;

    public ScrollBackground(string imagePath)
    {
        background = Image.GetInstance(imagePath);
        background.SetAbsolutePosition(0, 0);
        background.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfContentByte canvas = writer.DirectContentUnder;
        canvas.AddImage(background);
    }
}
