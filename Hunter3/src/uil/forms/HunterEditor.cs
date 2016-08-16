using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;
using System.Text;
using System.Collections.Generic;

namespace Hunter3
{
    public partial class HunterEditor : HunterForm
    {
        private String Caption = "Hunter 3 编辑器";
        private String CaptionFilename
        {
            get
            {
                if (Filename == null)
                    return Caption;
                else
                    return Caption + " [" + Filename + "]";
            }
        }
        private bool AsReflectionObject;
        private bool FileSaved = true;
        private String Filename;
        private String LoadFile;
        private HunterConfig Config;
        private HunterConsole Console;
        private object LoadObject;
        private Type ObjectType;
        private bool AsAModel;
        private String FileFilter;
        //asReflectionObject 表示此编辑器是用反射、序列化来处理文本，还是作为普通文本编辑器来处理文本
        public HunterEditor(HunterConsole c, HunterConfig config, String LoadFile, bool AsAModel, String fileFilter, bool asReflectionObject, Type objectType,  object loadObject, Hunter3.HunterRichTextBox.TextType textType)
        {
            FileFilter = fileFilter;
            this.AsAModel = AsAModel;
            this.LoadFile = LoadFile;
            ObjectType = objectType;
            Config = config;
            Console = c;
            this.AsReflectionObject = asReflectionObject;
            LoadObject = loadObject;
            InitializeComponent();

            hSearchBar.Init(hTextBox, tsLabel, HunterConfig.ColorBarForeColor);
            hHTMLGetterBar.Init(hTextBox, HunterConfig.ColorBarForeColor);
            hTextBox.ContentType = textType;
            tsLabel.BackColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MainToolStrip = msMenu;
            try
            {
                if (LoadFile != null)
                {
                    hTextBox.LoadFile(LoadFile);
                    if (AsAModel)
                    {
                        FileSaved = false;
                    }
                    else
                    {
                        Filename = LoadFile;
                    }

                    ClearDirty();
                }
            }
            catch (Exception ex)
            {
                Console.WriteException(ex);
            }

            if (!AsReflectionObject)
            {
                sContainer.Panel2Collapsed = true;
            }
            else
            {
                try
                {
                    RefreshXML();
                    LoadProperty();

                }
                catch (Exception ex)
                {
                    Console.WriteException(ex);
                }
            }

            FormClosing += new FormClosingEventHandler(HunterEditor_FormClosing);
            propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
            hTextBox.SelectionChanged += new EventHandler(hTextBox_SelectionChanged);
            hTextBox.TextChanged += new EventHandler((object s, EventArgs ea) =>
            {
                if (hTextBox.Modified)
                {
                    FileSaved = false;
                    Text = CaptionFilename + " *";
                }
                RefreshUI();
                RefreshPropertyGrid();
            });
            RefreshUI();
        }

        void HunterEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr;

            if (!FileSaved)
            {
                dr = MessageBox.Show("是否保存对" + Filename + "的修改？", "Hunter 3", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            }
            else
            {
                dr = System.Windows.Forms.DialogResult.No;
            }

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Save();
                e.Cancel = !FileSaved;
            }
            else if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
            }

        }

        void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshXML();
        }

        void RefreshXML()
        {
            if (AsReflectionObject)
            {
                try
                {
                    XmlSerializer xmls = new XmlSerializer(ObjectType);

                    //WindowsAPI.BeginPaint(hTextBox);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Encoding = Encoding.UTF8;
                        settings.Indent = true;
                        settings.IndentChars = "\t";
                        settings.NewLineChars = Environment.NewLine;
                        settings.ConformanceLevel = ConformanceLevel.Document;
                        using (XmlWriter writer = XmlTextWriter.Create(ms, settings))
                        {
                            xmls.Serialize(writer, LoadObject);
                        }
                        string xml = Encoding.UTF8.GetString(ms.ToArray());
                        hTextBox.LoadString(xml);
                    }
                    //WindowsAPI.EndPaint(hTextBox);
                }
                catch
                {
                    tsLabel.Text = "属性结构发生错误。";
                }
            }
        }

        void RefreshPropertyGrid()
        {
            if (AsReflectionObject)
            {
                XmlSerializer ser = new XmlSerializer(typeof(ProjectInfo));
                StringReader sr = new StringReader(hTextBox.Text);
                try
                {
                    LoadObject = (ProjectInfo)ser.Deserialize(sr);
                    tsLabel.Text = "就绪";
                }
                catch
                {
                    tsLabel.Text = "文档结构发生错误。";
                }
                sr.Close();
                propertyGrid.SelectedObject = LoadObject;
            }
        }

        void LoadProperty()
        {
            if (AsReflectionObject)
                propertyGrid.SelectedObject = this.LoadObject;
        }

        void hTextBox_SelectionChanged(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            miUndo.Enabled = hTextBox.CanUndo;
            miRedo.Enabled = hTextBox.CanRedo;

            if (hTextBox.SelectionLength > 0)
            {
                miCut.Enabled = true;
                miCopy.Enabled = true;
            }
            else
            {
                miCut.Enabled = false;
                miCopy.Enabled = false;
            }
            miPaste.Enabled = hTextBox.CanPaste(DataFormats.GetFormat(DataFormats.Text));
        }

        private void miPaste_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = WindowsAPI.GetFocusedControl() as RichTextBox;
            if (rtb != null)
                rtb.Paste(DataFormats.GetFormat(DataFormats.Text));
            RefreshUI();
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            RefreshUI();
            RichTextBox rtb = WindowsAPI.GetFocusedControl() as RichTextBox;
            if (rtb != null)
                rtb.Copy();
        }

        private void miRedo_Click(object sender, EventArgs e)
        {
            hTextBox.Redo();
            RefreshUI();
        }

        private void miUndo_Click(object sender, EventArgs e)
        {
            hTextBox.Undo();
            RefreshUI();
        }

        private void miAll_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = WindowsAPI.GetFocusedControl() as RichTextBox;
            if (rtb != null)
                rtb.SelectAll();
        }

        private void miCut_Click(object sender, EventArgs e)
        {
            RefreshUI();
            RichTextBox rtb = WindowsAPI.GetFocusedControl() as RichTextBox;
            if (rtb != null)
                rtb.Cut();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        new void Close()
        {
            base.Close();
        }

        void SaveAs()
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Hunter 3 项目(*.h3)|*.h3";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Filename = fd.FileName;
            }
            else return;
            hTextBox.SaveFile(Filename, RichTextBoxStreamType.PlainText);
            
            FileSaved = true;
            ClearDirty();
        }

        void Save()
        {
            if (!File.Exists(Filename))
            {
                SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = FileFilter;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Filename = fd.FileName;
                }
                else return;
            }
            hTextBox.SaveFile(Filename);
            FileSaved = true;
            ClearDirty();
        }

        void ClearDirty()
        {
            Text = Text.Substring(0, Text.Length - 1);
            Text = CaptionFilename;
        }

        private void miFindToogle_Click(object sender, EventArgs e)
        {
            hSearchBar.Visible = miFindToogle.Checked;
        }

        private void miHTMLGetter_Click(object sender, EventArgs e)
        {
            hHTMLGetterBar.Visible = miHTMLGetter.Checked;
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            Hunter3EditorNew();
        }

        public void Hunter3EditorNew()
        {
            String Filter = "所有格式 (*.*)|*.*";
            new HunterEditor(Console, Config, null, false, Filter, false, null, null, HunterRichTextBox.TextType.Plain).Show();
        }

        public void Hunter3EditorOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            String Filter = "所有格式 (*.*)|*.*"; ;
            ofd.Filter = Filter;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    new HunterEditor(Console, Config, ofd.FileName, false, Filter, false, typeof(StrategyData), null, HunterRichTextBox.TextType.Xml).Show();
                }
                catch (Exception ex)
                {
                    Console.WriteException(ex);
                }
            }
            else
            {
                return;
            }
        }

        public void OpenFileByHunter3Editor()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            String Filter = "Hunter 3 策略(*.h3s)|*.h3s";
            ofd.Filter = Filter;
            HunterRichTextBox.TextType t = HunterRichTextBox.TextType.Plain;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    String filename = ofd.FileName;
                    switch (Path.GetExtension(filename))
                    {
                        case ".h3":
                            t = HunterRichTextBox.TextType.Xml;
                            break;
                        case ".hip":
                            t = HunterRichTextBox.TextType.ProxyFilter;
                            break;
                        case ".h3s":
                            t = HunterRichTextBox.TextType.Xml;
                            break;
                    }

                    new HunterEditor(Console, Config, ofd.FileName, false, Filter, false, null, null, t).Show();
                }
                catch (Exception ex)
                {
                    Console.WriteException(ex);
                }
            }
            else
            {
                return;
            }
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            Hunter3EditorOpen();
        }

        private void miSyntaxPlain_Click(object sender, EventArgs e)
        {
            ToSyntax(HunterRichTextBox.TextType.Plain);
        }

        private void miSyntaxXML_Click(object sender, EventArgs e)
        {
            ToSyntax(HunterRichTextBox.TextType.Xml);
        }

        public void ToSyntax(HunterRichTextBox.TextType t)
        {
            hTextBox.ContentType = t;
            
            hTextBox.PrettifyText();
        }

        private void miToXMLEntity_Click(object sender, EventArgs e)
        {
            EntityTransform(true);
        }

        public void EntityTransform(bool fromCharToEntity)
        {
            Dictionary<String, String> EntityChar = new Dictionary<String, String>();
            EntityChar.Add("&amp;", "&");
            EntityChar.Add("&lt;", "<");
            EntityChar.Add("&gt;", ">");
            EntityChar.Add("&apos;", "'");
            EntityChar.Add("&quot;", "\"");
            Dictionary<String, String> CharEntity = new Dictionary<String, String>();
            foreach ( String s in EntityChar.Keys ){
                if (s == "&") continue;
                CharEntity.Add(EntityChar[s], s);
            }
            CharEntity.Add("&amp;", "&");

            if (fromCharToEntity)
            {
                String result = hTextBox.SelectedText;
                foreach (String s in CharEntity.Keys)
                {
                    int f = result.IndexOf(s);
                    result = result.Replace(s, CharEntity[s]);
                }
                hTextBox.SelectedText = result;
                hTextBox.Select(hTextBox.SelectionStart - result.Length, result.Length);
            }
            else
            {
                String result = hTextBox.SelectedText;
                foreach (String s in EntityChar.Keys)
                    result = result.Replace(s, EntityChar[s]);
                hTextBox.SelectedText = result;
                hTextBox.Select(hTextBox.SelectionStart - result.Length, result.Length);
            }
        }

        private void miToXMLCharacter_Click(object sender, EventArgs e)
        {
            EntityTransform(false);
        }
    }
}
