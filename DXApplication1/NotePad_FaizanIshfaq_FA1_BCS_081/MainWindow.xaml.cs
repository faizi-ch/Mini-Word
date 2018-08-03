using System;
using System.Collections.Generic;
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
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.XtraRichEdit;
using Microsoft.Win32;
using DevExpress.Internal.WinApi;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;


namespace NotePad_FaizanIshfaq_FA1_BCS_081
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        private string openedFileLocation;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextBox.CreateNewDocument();
        }

        private void NewBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open File";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter =
                "All Supported Files (*.txt, *.rtf, *.doc, *.docx, *.htm, *.html, *.mht, *.odt, *.xml, *.epub|*.txt; *.rtf; *.doc; *.docx; *.htm; *.html; *.mht; *.odt; *.xml; *.epub|Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Microsoft Word 97-2003 (*.doc)|*.doc| Microsoft Word 2007 (*.docx)|*.docx|HTML (*.htm, *.html)|*.htm; *.html|WebArchive (*.mht)|*.mht|Open Document (*.odt)|*.odt|XML Document (*.xml)|*.xml|Electronic Publication (*.epub)|*.epub";

            if (openFileDialog.ShowDialog() == true)
            {
                openedFileLocation = openFileDialog.FileName;
                RichTextBox.Document.LoadDocument(openedFileLocation, DocumentFormat.Undefined);
                //isFileOpened = true;
            }
        }

        private void SaveBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            string fileDirectory;
            string fileName = "";
            string selectedFileExtension;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "Text files(*.txt) | *.txt | Rich Text Format (*.rtf) | *.rtf | Microsoft Word 97 - 2003(*.doc) | *.doc | Microsoft Word 2007(*.docx) | *.docx | HTML(*.htm, *.html) | *.htm; *.html | WebArchive(*.mht) | *.mht | Open Document(*.odt) | *.odt | XML Document(*.xml) | *.xml | Electronic Publication(*.epub) | *.epub";



            if (saveFileDialog.ShowDialog() == true)
            {
                DocumentFormat documentFormat = new DocumentFormat();
                switch (saveFileDialog.FilterIndex)
                {
                    case 0:
                        documentFormat = DocumentFormat.PlainText;
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
                    (System.IO.FileStream)saveFileDialog.OpenFile();


                fileDirectory = saveFileDialog.FileName;

                if (fileName != "")
                {
                    RichTextBox.Document.SaveDocument(fileDirectory, documentFormat);
                }

            }
        }

        private void SaveAsBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextBox.SaveDocumentAs();
        }

        private void UndoButton_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextBox.Undo();
        }

        private void RedoButton_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            RichTextBox.Redo();
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RichTextBox.CanUndo)
            {
                UndoButton.IsEnabled = true;
            }
            else
            {
                UndoButton.IsEnabled = false;
            }

            if (RichTextBox.CanRedo)
            {
                RedoButton.IsEnabled = true;
            }
            else
            {
                RedoButton.IsEnabled = false;
            }

            int chars = RichTextBox.CharCount;
            CharactersCountBarStaticItem.Content = "Characters: " + chars;

            int words = RichTextBox.WordCount;
            WordsCountBarStaticItem.Content = "Words: " + words;

            int lines = RichTextBox.ParagraphCount;
            LinesCountBarStaticItem.Content = "Lines: " + lines;

            SaveButton.IsEnabled = true;

            
        }

        private void HorizontalRulerCheckButton_IsStylusCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (HorizontalRulerCheckButton.IsChecked == false)
            {
                RichTextBox.HorizontalRulerVisibility = Visibility.Hidden;
            }
            else
            {
                RichTextBox.HorizontalRulerVisibility = Visibility.Visible;

            }
        }

        private void VerticalRulerCheckButton_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (VerticalRulerCheckButton.IsChecked == false)
            {
                RichTextBox.HorizontalRulerVisibility = Visibility.Hidden;
            }
            else
            {
                RichTextBox.HorizontalRulerVisibility = Visibility.Visible;

            }
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            MessageBoxResult msgBoxResult = WinUIMessageBox.Show("Are you sure you want to cancel without save?",
                                     "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                if (!SaveButton.IsEnabled)
                {
                    SaveBarButtonItem_ItemClick(SaveButton, new ItemClickEventArgs(SaveButton, e.Link));
                }
            }

            
        }
    }
}
