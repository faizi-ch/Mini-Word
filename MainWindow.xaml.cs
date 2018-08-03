using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.XtraRichEdit;
using Microsoft.Win32;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        private bool isFileOpened = false;
        private string openedFileLocation;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RichEditControl_TextChanged(object sender, EventArgs e)
        {
            int chars = RichTextEditControl.CharCount;
            CharsCountBarStaticItem.Content = "Characters: " + chars;

            int words = RichTextEditControl.WordCount;
            WordsCountBarStaticItem.Content = "Words: " + words;

            int lines = RichTextEditControl.ParagraphCount;
            LinesCountBarStaticItem.Content = "Lines: " + lines;

            SaveBarButtonItem.IsEnabled = true;

            if (RichTextEditControl.CanUndo)
            {
                UndoBarButtonItem.IsEnabled = true;
            }
            else
            {
                UndoBarButtonItem.IsEnabled = false;
            }

            if (RichTextEditControl.CanRedo)
            {
                RedoBarButtonItem.IsEnabled = true;
            }
            else
            {
                RedoBarButtonItem.IsEnabled = false;
            }
        }

        private void BarEditItem_EditValueChanged(object sender, RoutedEventArgs e)
        {
            //RichTextEditControl.ApplyTemplate();
            //RichTextEditControl.ActiveView.ZoomFactor=1.5f;
        }

        private void NewBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextEditControl.CreateNewDocument();}

        private void HorizontalRulerBarCheckItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (HorizontalRulerBarCheckItem.IsChecked==false)
            {
                RichTextEditControl.HorizontalRulerVisibility = Visibility.Hidden;}
            else
            {
                RichTextEditControl.HorizontalRulerVisibility = Visibility.Visible;
                
            }
            }

        private void VerticalRulerBarCheckItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (VerticalRulerBarCheckItem.IsChecked==false)
            {
                RichTextEditControl.VerticalRulerVisibility = Visibility.Hidden;
            }
            else
            {
                RichTextEditControl.VerticalRulerVisibility = Visibility.Visible;
                
            }
        }

        private void RichTextEditControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            /*RichTextEditControl.HorizontalRulerVisibility = Visibility.Hidden;
            RichTextEditControl.VerticalRulerVisibility = Visibility.Hidden;
            RichTextEditControl.ApplyTemplate();*/
        }

        private void OpenBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open File";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter =
                "All Supported Files (*.txt, *.rtf, *.doc, *.docx, *.htm, *.html, *.mht, *.odt, *.xml, *.epub|*.txt; *.rtf; *.doc; *.docx; *.htm; *.html; *.mht; *.odt; *.xml; *.epub|Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Microsoft Word 97-2003 (*.doc)|*.doc| Microsoft Word 2007 (*.docx)|*.docx|HTML (*.htm, *.html)|*.htm; *.html|WebArchive (*.mht)|*.mht|Open Document (*.odt)|*.odt|XML Document (*.xml)|*.xml|Electronic Publication (*.epub)|*.epub";

            if (openFileDialog.ShowDialog()==true)
            {
                openedFileLocation = openFileDialog.FileName;
                RichTextEditControl.Document.LoadDocument(openedFileLocation, DocumentFormat.Undefined);
                isFileOpened = true;
            }
            
        }

        private void SaveBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            string fileDirectory;
            string fileName="";
            string selectedFileExtension;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter= "Text files(*.txt) | *.txt | Rich Text Format (*.rtf) | *.rtf | Microsoft Word 97 - 2003(*.doc) | *.doc | Microsoft Word 2007(*.docx) | *.docx | HTML(*.htm, *.html) | *.htm; *.html | WebArchive(*.mht) | *.mht | Open Document(*.odt) | *.odt | XML Document(*.xml) | *.xml | Electronic Publication(*.epub) | *.epub";
            

            
            if (saveFileDialog.ShowDialog() == true)
            {
                DocumentFormat documentFormat=new DocumentFormat();
                switch (saveFileDialog.FilterIndex)
                {
                    case 0:
                        documentFormat=DocumentFormat.PlainText;
                        break;
                    case 1:
                        documentFormat = DocumentFormat.Rtf;
                        break;
                    case 2:
                        documentFormat = DocumentFormat.Doc;
                        break;
                    case 3:
                        documentFormat = DocumentFormat.Doc;
                        break;
                    case 4:
                        documentFormat = DocumentFormat.Html;
                        break;
                    case 5:
                        documentFormat = DocumentFormat.Mht;
                        break;
                    case 6:
                        documentFormat = DocumentFormat.OpenDocument;
                        break;
                    case 7:
                        documentFormat = DocumentFormat.OpenXml;
                        break;
                    case 8:
                        documentFormat = DocumentFormat.ePub;
                        break;
                    default:
                        break;
                }

                System.IO.FileStream fs =
                    (System.IO.FileStream) saveFileDialog.OpenFile();

                
                fileDirectory = saveFileDialog.FileName;

                if (fileName!="")
                {
                    RichTextEditControl.Document.SaveDocument(fileDirectory, documentFormat);
                }
                
            }
        }

        private void SaveAsBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextEditControl.SaveDocumentAs();
        }

        private void UndoBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextEditControl.Undo();
        }

        private void RedoBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextEditControl.Redo();
        }
    }
}
