using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using WinForm = System.Windows.Forms;

namespace FlowerShop
{
    //System.Windows.Forms
    //Microsoft.Office.Interop.Word;
    using Word = Microsoft.Office.Interop.Word;
    using WinForm = System.Windows.Forms;
    internal class WordService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PathFile">относительный путь. например "Word/WordFile.docx"</param>
        public WordService(string PathFile)
        {
            TemplateFileName = PathFile;
            wordApp.Visible = false;
            wordDocument = wordApp.Documents.Open(TemplateFileName);
        }

        private string _templateFileName;
        public string TemplateFileName
        {
            get
            {
                return System.IO.Path.GetFullPath(_templateFileName);
            }
            set
            {
                _templateFileName = value;
            }
        }

        Application wordApp = new Word.Application();
        Document wordDocument;
        /// <summary>
        /// функция поиска и замена текста
        /// </summary>
        /// <param name="stubToReplace">поиск слова</param>
        /// <param name="text">замена слова</param>
        public void ReplaceWordStub(String stubToReplace, string text)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        public void ToWord()
        {

            WinForm.SaveFileDialog saveFileDialog1 = new WinForm.SaveFileDialog();
            saveFileDialog1.Filter = "Word Files (*.docx)|*.docx";
            saveFileDialog1.Title = "Сохранить документ";
            if (saveFileDialog1.ShowDialog() == WinForm.DialogResult.OK)
            {
                wordDocument.SaveAs2(saveFileDialog1.FileName);
                wordApp.Visible = true;
            }
        }
    }
}
