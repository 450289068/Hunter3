using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace Hunter3
{
    public class HunterRichTextBox : RichTextBox
    {
        List<int> lastPosition = new List<int>();
        List<int> lastPositionLength = new List<int>();
        private TextType _ContentType;
        public TextType ContentType
        {
            get
            {
                return _ContentType;
            }
            set
            {
                _ContentType = value;
            }
        }

        public enum TextType { None, Plain, Xml, ProxyFilter };
        private enum XMLState { Start, inPlain, inElement, inAttribute, inString, inMaybeComment, inComment, inMaybeEscape };
        private enum ProxyFilterState { Start, inComment };

        struct Undos{
            public string content;
            public int start;
            public int length;
            public Undos(string _c, int _s, int _l){
                content = _c;
                start = _s;
                length = _l;
            }
        }

        Stack undos = new Stack();
        Stack redos = new Stack();
        public bool isUndoRedo;

        public void ClearUndoRedoStack()
        {
            undos.Clear();
            redos.Clear();
            Push();
        }

        new public bool CanUndo
        {
            get { return undos.Count > 1; }
        }

        new public bool CanRedo
        {
            get { return redos.Count > 0; }
        }

        public void Push()
        {
            undos.Push(new Undos(Rtf, SelectionStart, SelectionLength));
        }

        new public void Undo()
        {
            TextChanged -= new EventHandler(HunterRichTextBox_TextChanged);
            redos.Push(undos.Pop());
            isUndoRedo = true;
            Undos _ = (Undos)undos.Peek();
            Rtf = _.content;
            Select(_.start,_.length);

            isUndoRedo = false;
            TextChanged += new EventHandler(HunterRichTextBox_TextChanged);
        }

        new public void Redo()
        {
            TextChanged -= new EventHandler(HunterRichTextBox_TextChanged);
            isUndoRedo = true;
            Undos _ = (Undos)redos.Peek();
            Rtf = _.content;
            Select(_.start,_.length);
            undos.Push(redos.Pop());
            isUndoRedo = false;
            TextChanged += new EventHandler(HunterRichTextBox_TextChanged);
        }

        public void ClearRedo()
        {
            redos.Clear();
        }

        public HunterRichTextBox()
        {
            ContentType = TextType.None;
            Init();
        }

        public HunterRichTextBox(TextType t)
        {
            ContentType = t;
            Init();
        }

        void Init()
        {
            AcceptsTab = true;
            LinkClicked += new LinkClickedEventHandler(HunterRichTextBox_LinkClicked);
            WindowsAPI.BeginPaint(this);
            SuspendLayout();
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ForeColor = System.Drawing.Color.White;
            Size = new System.Drawing.Size(728, 475);
            ScrollBars = RichTextBoxScrollBars.Vertical;
            base.Margin = new Padding(0);
            base.ImeMode = ImeMode.On;
            ResumeLayout(false);
            WindowsAPI.EndPaint(this);

            TextChanged += new EventHandler(HunterRichTextBox_TextChanged);
        }

        new public void SelectAll()
        {
            base.SelectAll();
            lastPosition.Add(0);
            lastPositionLength.Add(TextLength);
        }

        new public void LoadFile(String path)
        {
            TextChanged -= new EventHandler(HunterRichTextBox_TextChanged);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            Prettify(true);
            Push();
            TextChanged += new EventHandler(HunterRichTextBox_TextChanged);
        }

        new public void SaveFile(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(Text);
            sw.Close();
            fs.Close();
        }

        public void LoadString(String str)
        {
            TextChanged -= new EventHandler(HunterRichTextBox_TextChanged);
            RichTextBox r = new RichTextBox();
            r.Text = str;
            if (r.Text[0] == '<')
                Text = str;
            else
                Text = r.Text.Substring(1);
            Prettify(true);
            Push();
            TextChanged += new EventHandler(HunterRichTextBox_TextChanged);
        }

        void HunterRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ContentType != TextType.None)
            {
                Font = new System.Drawing.Font("微软雅黑", 10F);
                if (!ReadOnly)
                    if (!isUndoRedo) Push();
                lastPosition.Add((sender as RichTextBox).SelectionStart);
                lastPositionLength.Add((sender as RichTextBox).SelectionLength);
                Prettify();
            }
        }

        void HunterRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public void WriteLine(String str, Color color)
        {
            try
            {
                if (TextLength > 1024 * 128 * 5) Text = "";
                Color defaultColor = Color.White;
                lock (this)
                {
                    Thread.Sleep(10);
                    Select(TextLength, 0);
                    SelectionColor = color;
                    AppendText(str + Environment.NewLine);
                }

            }
            catch
            {
                return;
            }
        }

        public void PrettifyText()
        {
            TextChanged -= new EventHandler(HunterRichTextBox_TextChanged);
            Prettify(true);
            TextChanged += new EventHandler(HunterRichTextBox_TextChanged);
        }

        private void Prettify(bool prettifyWhole = false)
        {
            switch (ContentType)
            {
                case TextType.Plain:
                    SuspendLayout();
                    PrettifyPlain();
                    ResumeLayout();
                    break;
                case TextType.Xml:
                    SuspendLayout();
                    PrettifyXML(prettifyWhole);
                    ResumeLayout();
                    break;
                case TextType.ProxyFilter:
                    SuspendLayout();
                    PrettifyProxyFilter(prettifyWhole);
                    ResumeLayout();
                    break;
            }
        }

        private void PrettifyPlain()
        {
            int currentSelection = SelectionStart, currentSelectionLength = SelectionLength;
            SelectAll();
            ForeColor = HunterConfig.ColorPlain;
            SelectionColor = HunterConfig.ColorPlain;
            Select(currentSelection, currentSelectionLength);
        }

        //从Until位置往前找第一个字符c的位置，没有找到则返回0
        private int FindLast(String s, int until, char c)
        {
            if (s.Length > 0)
            {
                for (int i = until; i >= 0; i--)
                {
                    try
                    {
                        if (s[i] == c) return i;
                    }
                    catch { i--; }
                }
            }
            return 0;
        }

        private int FindFirst(String s, int from, char c)
        {
            if (s.Length > 0)
            {
                for (int i = from; i < s.Length; i++)
                {
                    if (s[i] == c) return i;
                }
            }
            return s.Length - 1;
        }

        private void PrettifyXML(bool prettifyWhole)
        {
            XMLState state = XMLState.Start, lastState = XMLState.Start;
            WindowsAPI.BeginPaint(this);
            int cursor = 0;
            int currentPosition = SelectionStart;
            int startPos = 0;
            char quote = '\0';

            int start, end;
            if (prettifyWhole)
            {
                start = 0;
                end = TextLength - 1;
            }
            else
            {
                if (lastPosition.Count > 1)
                {
                    start = FindLast(Text, lastPosition[lastPosition.Count - 2] > currentPosition ? currentPosition - 1 : lastPosition[lastPosition.Count - 2], '<');
                    end = FindFirst(Text, currentPosition, '>');
                }
                else
                {
                    start = 0;
                    end = TextLength - 1;
                }
            }
            for (int i = start; i <= end; i++)
            {
                cursor = i;

                if (state != XMLState.inString && state != XMLState.inComment)
                {
                    if (Text[i] == '\"')
                    {
                        quote = '\"';
                        SetColor(cursor, 1, HunterConfig.ColorXMLString);
                        lastState = state;
                        state = XMLState.inString;
                        continue;
                    }
                    else if (Text[i] == '\'')
                    {
                        quote = '\'';
                        SetColor(cursor, 1, HunterConfig.ColorXMLString);
                        lastState = state;
                        state = XMLState.inString;
                        continue;
                    }
                }

                switch (state)
                {
                    case XMLState.Start:
                        if (Text[i] == '<')
                        {
                            state = XMLState.inElement;
                        }
                        else if (Text[i] == '&')
                        {
                            lastState = state;
                            state = XMLState.inMaybeEscape;
                            startPos = i;
                        }
                        SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        
                        break;
                    case XMLState.inString:
                        if (Text[i] == quote)
                        {
                            //还原状态
                            state = lastState;
                            quote = '\0';
                        }

                        SetColor(cursor, 1, HunterConfig.ColorXMLString);
                        break;
                    case XMLState.inElement:
                        if (Text[i] != '?' && Text[i] != '>' && !Char.IsWhiteSpace(Text[i]) && Text[i] != '!' && Text[i] != '/')
                        {
                            SetColor(cursor, 1, HunterConfig.ColorXMLElement);
                        }
                        else if (Text[i] == '?' || Text[i] == '/')
                        {
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        else if (Text[i] == '>')
                        {
                            state = XMLState.Start;
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        else if (Text[i] == '!')
                        {
                            lastState = state;
                            state = XMLState.inMaybeComment;
                            startPos = i;
                            SetColor(cursor, 2, HunterConfig.ColorXMLPlain);
                        }
                        else if (Char.IsWhiteSpace(Text[i]))
                        {
                            state = XMLState.inAttribute;
                            startPos = i + 1;
                        }
                        break;
                    case XMLState.inAttribute:
                        if (Text[i] != '=' && !Char.IsWhiteSpace(Text[i]) && Text[i] != '>' && Text[i] != '?' && Text[i] != '<')
                        {
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        else if (Text[i] == '=')
                        {
                            SetColor(startPos, cursor - startPos , HunterConfig.ColorXMLAttribute);
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        else if (Char.IsWhiteSpace(Text[i]))
                        {
                            startPos = i + 1;
                        }
                        else if (Text[i] == '>')
                        {
                            state = XMLState.Start;
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        else if (Text[i] == '<')
                        {
                            state = XMLState.inElement;
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        else if (Text[i] == '?')
                        {
                            SetColor(cursor, 1, HunterConfig.ColorXMLPlain);
                        }
                        break;
                    case XMLState.inMaybeComment:
                        if (Text[i] == '-' && i + 1 < TextLength && Text[i + 1] == '-')
                        {
                            state = XMLState.inComment;
                            SetColor(startPos - 1, 4, HunterConfig.ColorXMLComment);
                            i++;
                        }
                        else
                        {
                            state = lastState;
                        }
                        break;
                    case XMLState.inComment:
                        if (Text[i] == '-')
                        {
                            startPos = i;
                            if (i + 2 < TextLength)
                            {
                                if (Text[i + 1] == '-' && Text[i + 2] == '>')
                                {
                                    state = XMLState.Start;
                                    SetColor(cursor, 3, HunterConfig.ColorXMLComment);
                                    i += 2;
                                }
                            }
                        }
                        else
                        {
                            SetColor(cursor, 1, HunterConfig.ColorXMLComment);
                        }
                        break;
                    case XMLState.inMaybeEscape:
                        if (Text[i] == ';')
                        {
                            state = lastState;
                        }
                        SetColor(startPos, cursor - startPos + 1, HunterConfig.ColorXMLEscape);
                        break;
                }
            }

            Select(currentPosition,0);
            WindowsAPI.EndPaint(this);
        }

        private void PrettifyProxyFilter(bool prettifyWhole)
        {
            
            ProxyFilterState state = ProxyFilterState.Start;
            WindowsAPI.BeginPaint(this);
            int cursor = 0;
            int currentPosition = SelectionStart;

            int start, end;
            start = 0;
            end = TextLength - 1;
            for (int i = start; i <= end; i++)
            {
                cursor = i;

                switch (state)
                {
                    case ProxyFilterState.Start:
                        if (Text[i] == '\'')
                        {
                            state = ProxyFilterState.inComment;
                            SetColor(cursor, 1, HunterConfig.ColorProxyFilterComment);
                        }
                        else
                        {
                            SetColor(cursor, 1, HunterConfig.ColorProxyFilterPlain);
                        }
                        break;
                    case ProxyFilterState.inComment:
                        int Return = Text.IndexOf('\n', i);
                        int length = Return - cursor;
                        SetColor(cursor, length , HunterConfig.ColorProxyFilterComment);
                        state = ProxyFilterState.Start;
                        i += length ;
                        break;
                    
                }
            }

            Select(currentPosition, 0);
            WindowsAPI.EndPaint(this);
        }

        private void SetColor(int Start, int Length, Color c)
        {
            Select(Start, Length);
            SelectionColor = c;

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HunterRichTextBox
            // 
            this.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideSelection = false;
            this.ShowSelectionMargin = true;
            this.ResumeLayout(false);

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!base.AutoWordSelection)
            {
                base.AutoWordSelection = true;
                base.AutoWordSelection = false;
            }
        }
    }
}
