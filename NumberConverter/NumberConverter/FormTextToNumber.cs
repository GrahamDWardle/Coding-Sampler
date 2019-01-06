//-----------------------------------------------------------------------
// <copyright file="FormTextToNumber.cs" company="Graham D Wardle">
//     The Number words to number converter sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace NumberConverter
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The Application entry form.
    /// </summary>
    public partial class FormTextToNumber : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormTextToNumber" /> class.
        /// </summary>
        public FormTextToNumber()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event handler for the change in the Entry Text.
        /// </summary>
        /// <param name="sender">The source if the Text Change event.</param>
        /// <param name="e">The Event arguments.</param>
        private void TextBoxEntry_TextChanged(object sender, EventArgs e)
        {
            if (TextToNumber.IsValid(this.textBoxText.Text))
            {
                long number = TextToNumber.DoConvert(this.textBoxText.Text);
                if (number > 0)
                {
                    this.labelResults.Text = string.Format("Valid number: {0}", number);
                }
                else
                {
                    this.labelResults.Text = TextToNumber.Error;
                }
            }
            else
            {
                this.labelResults.Text = TextToNumber.Error;
            }
        }
    }
}
