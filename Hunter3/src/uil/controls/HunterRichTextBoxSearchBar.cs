using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace Hunter3
{
    public partial class HunterRichTextBoxSearchBar : UserControl
    {
        private readonly int PaddingRight = 100;
        private List<Match> MatchResults = new List<Match>();
        private List<int> TextSearchResults = new List<int>();
        private int MatchPointer = 0;
        private int TextSearchPointer = 0;

        public RichTextBox BindingRichTextBox { get; set; }
        public Control BindingOutputControl { get; set; }
        private Color SearchForeColor;

        public HunterRichTextBoxSearchBar()
        {
            InitializeComponent();
            Dock = DockStyle.Bottom;
            Height = sFind.Height;
            hSearch.KeyDown +=new KeyEventHandler(hSearch_KeyDown);
            hSearch.TextChanged +=new EventHandler(hSearch_TextChanged);
        }

        public void Init(RichTextBox binding, Control output, Color forecolor){
            BindingRichTextBox = binding;
            BindingOutputControl = output;
            SearchForeColor = forecolor;
            hSearch.ForeColor = forecolor;
            hSearch.Width = Width - hSearch.Left - PaddingRight;
            this.Resize += new EventHandler(HunterRichTextBoxSearchBar_Resize);
            this.VisibleChanged += new EventHandler(HunterRichTextBoxSearchBar_VisibleChanged);
        }

        void HunterRichTextBoxSearchBar_Resize(object sender, EventArgs e)
        {
            hSearch.Width = Width - hSearch.Left - PaddingRight;
        }

        void HunterRichTextBoxSearchBar_VisibleChanged(object sender, EventArgs e)
        {
            hSearch.Width = Width - hSearch.Left - PaddingRight;
        }

        void FindText(bool useRegex, String condition, bool CaseSensitive, bool WholeWord, int StartIndex)
        {
            RegexOptions ro = CaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
            RichTextBoxFinds rtbf = CaseSensitive ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;
            RichTextBoxFinds rtbf2 = WholeWord ? RichTextBoxFinds.WholeWord : RichTextBoxFinds.None;
            if (useRegex)
            {
                Regex regSearchCondition = null;
                Match matchSearch = null;
                try
                {
                    regSearchCondition = new Regex(condition, ro);
                    matchSearch = regSearchCondition.Match(BindingRichTextBox.Text);
                }
                catch (ArgumentException ex)
                {
                    BindingOutputControl.Text = "正则表达式输入有误。" + ex.Message;
                    return;
                }
                catch { }
                MatchResults.Clear();

                while (matchSearch.Success)
                {
                    if (matchSearch.Value != "")
                    {
                        MatchResults.Add(matchSearch);
                    }
                    matchSearch = matchSearch.NextMatch();
                }

                if (MatchResults.Count > 0)
                {
                    int ClosestDistance = int.MaxValue;
                    int ClosestIndex = 0;

                    if (StartIndex < 0)
                    {
                        for (int i = 0; i < MatchResults.Count; i++)
                        {
                            int abs;
                            if ((abs = Math.Abs(MatchResults[i].Index - BindingRichTextBox.SelectionStart)) < ClosestDistance)
                            {
                                ClosestDistance = abs;
                                ClosestIndex = i;
                            }
                        }
                        MatchPointer = ClosestIndex;
                    }
                    else
                    {
                        MatchPointer = StartIndex;
                    }
                    BindingRichTextBox.Select(MatchResults[MatchPointer].Index, MatchResults[MatchPointer].Value.Length);
                    //BindingRichTextBox.ScrollToCaret();
                    BindingOutputControl.Text = "总共找到结果" + MatchResults.Count + "个，目前是第" + (MatchPointer + 1) + "个。";
                }
                else
                {
                    BindingOutputControl.Text = "无法找到正则表达式所对应的内容：" + condition;
                }
            }
            else
            {
                int StartPosition = 0;
                int findResult = -1;
                TextSearchResults.Clear();
                while ((findResult = BindingRichTextBox.Find(condition, StartPosition, rtbf | rtbf2 | RichTextBoxFinds.NoHighlight)) >= 0)
                {
                    TextSearchResults.Add(findResult);
                    StartPosition = findResult + condition.Length;
                    if (StartPosition >= BindingRichTextBox.TextLength) break;
                }

                if (TextSearchResults.Count > 0)
                {
                    int ClosestDistance = int.MaxValue;
                    int ClosestIndex = 0;

                    if (StartIndex < 0)
                    {
                        for (int i = 0; i < TextSearchResults.Count; i++)
                        {
                            int abs;
                            if ((abs = Math.Abs(TextSearchResults[i] - BindingRichTextBox.SelectionStart)) < ClosestDistance)
                            {
                                ClosestDistance = abs;
                                ClosestIndex = i;
                            }
                        }
                        TextSearchPointer = ClosestIndex;
                    }
                    else
                    {
                        TextSearchPointer = StartIndex;
                    }
                    BindingRichTextBox.Select(TextSearchResults[TextSearchPointer], condition.Length);
                    //BindingRichTextBox.ScrollToCaret();
                    BindingOutputControl.Text = "总共找到结果" + TextSearchResults.Count + "个，目前是第" + (TextSearchPointer + 1) + "个。";
                }
                else
                {
                    BindingOutputControl.Text = "无法找到" + condition;
                }
            }
        }

        void ShowSearchResult()
        {
            if (hSearch.Text == "")
            {
                BindingOutputControl.Text = "就绪";
                return;
            }

            FindText(hbRegex.Checked, hSearch.Text, hbCaseSensitive.Checked, hbWholeWord.Checked, -1);
        }

        void FindNextPrev(int increment)
        {
            if (hbRegex.Checked)
            {
                if (MatchResults.Count > 0)
                {
                    MatchPointer += increment;
                    if (MatchPointer >= MatchResults.Count)
                    {
                        MatchPointer = 0;
                    }
                    if (MatchPointer < 0)
                    {
                        MatchPointer = MatchResults.Count - 1;
                    }
                }
                FindText(hbRegex.Checked, hSearch.Text, hbCaseSensitive.Checked, hbWholeWord.Checked, MatchPointer);
            }
            else
            {
                if (TextSearchResults.Count > 0)
                {
                    TextSearchPointer += increment;
                    if (TextSearchPointer >= TextSearchResults.Count)
                    {
                        TextSearchPointer = 0;
                    }
                    if (TextSearchPointer < 0)
                    {
                        TextSearchPointer = TextSearchResults.Count - 1;
                    }
                }
                FindText(hbRegex.Checked, hSearch.Text, hbCaseSensitive.Checked, hbWholeWord.Checked, TextSearchPointer);
            }
        }

        private void hSearch_TextChanged(object sender, EventArgs e)
        {
            hSearch.ForeColor = SearchForeColor;
            ShowSearchResult();
        }

        private void hbRegex_Click(object sender, EventArgs e)
        {
            ShowSearchResult();
        }

        private void hbCaseSensitive_Click(object sender, EventArgs e)
        {
            ShowSearchResult();
        }

        private void hbWholeWord_Click(object sender, EventArgs e)
        {
            ShowSearchResult();
        }

        private void hbSearchNext_Click(object sender, EventArgs e)
        {
            FindNextPrev(1);
        }

        private void hbFindPrev_Click(object sender, EventArgs e)
        {
            FindNextPrev(-1);
        }

        void hSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                if (e.Shift == true)
                {
                    FindNextPrev(-1);
                }
                else
                {
                    FindNextPrev(1);
                }
            }
        }

        


    }
}
