using System;

namespace NotesApp
{
    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public bool HasNotes => !string.IsNullOrWhiteSpace(Description);

        public override string ToString()
        {
            return $"{Title} - {Date.ToString("dd/MM/yyyy")}";
        }
    }
}
