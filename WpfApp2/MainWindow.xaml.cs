using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace NotesApp
{
    public partial class MainWindow : Window
    {
        private List<Note> notes;

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();
            datePicker.SelectedDate = DateTime.Today;
            DisplayNotes(DateTime.Today);
        }

        private void LoadNotes()
        {
            string fileName = "notes.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            else
            {
                notes = new List<Note>();
            }
        }

        private void SaveNotes()
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText("notes.json", json);
        }

        private void DisplayNotes(DateTime date)
        {
            notesListBox.Items.Clear();
            foreach (var note in notes)
            {
                if (note.Date.Date == date.Date)
                {
                    notesListBox.Items.Add(note);
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Today;
            DisplayNotes(selectedDate);
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            var addNoteWindow = new AddEditNoteWindow();
            if (addNoteWindow.ShowDialog() == true)
            {
                notes.Add(addNoteWindow.Note);
                SaveNotes();
                DisplayNotes(datePicker.SelectedDate ?? DateTime.Today);
            }
        }

        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            if (notesListBox.SelectedItem != null)
            {
                var editNoteWindow = new AddEditNoteWindow((Note)notesListBox.SelectedItem);
                if (editNoteWindow.ShowDialog() == true)
                {
                    int index = notesListBox.SelectedIndex;
                    notes[index] = editNoteWindow.Note;
                    SaveNotes();
                    DisplayNotes(datePicker.SelectedDate ?? DateTime.Today);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заметку для редактирования.");
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (notesListBox.SelectedItem != null)
            {
                notes.Remove((Note)notesListBox.SelectedItem);
                SaveNotes();
                DisplayNotes(datePicker.SelectedDate ?? DateTime.Today);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заметку для удаления.");
            }
        }
    }
}
