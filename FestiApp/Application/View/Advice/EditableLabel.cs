using System;
using System.Windows.Forms;

namespace FestiApp.View.Advice
{
    public partial class EditableLabel : Control
    {
        private string _text = "";
        public override string Text
        {
            get => _text;
            set
            {
                _text = value;
                label.Text = value;
                textBox.Text = value;
            }
        }

        private bool _editMode = false;
        public bool EditMode
        {
            get => _editMode;
            set
            {
                if (!CanEdit) return;

                _editMode = value;

                if (_editMode)
                {
                    label.Visible = false;
                    textBox.Visible = true;
                }
                else
                {
                    label.Visible = true;
                    textBox.Visible = false;
                }
            }
        }

        public bool CanEdit { get; set; } = true;

        public EditableLabel()
        {
            InitializeComponent();
        }

        private void LabelOnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditMode = true;
        }

        private void TextBoxOnTextChanged(object sender, EventArgs e)
        {
            Text = ((TextBox)sender).Text;
        }
    }
}