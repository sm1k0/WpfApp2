using System.Windows;

namespace NotesApp
{
    public partial class AddEditNoteWindow : Window
    {
        public Note Note { get; private set; }

        public AddEditNoteWindow(Note note = null)
        {
            InitializeComponent();
            if (note != null)
            {
                titleTextBox.Text = note.Title;
                descriptionTextBox.Text = note.Description;
                datePicker.SelectedDate = note.Date;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text) || datePicker.SelectedDate == null)
            {
                MessageBox.Show("Название и дата обязательны к заполнению.");
                return;
            }

            Note = new Note
            {
                Title = titleTextBox.Text,
                Description = descriptionTextBox.Text,
                Date = datePicker.SelectedDate ?? default
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
