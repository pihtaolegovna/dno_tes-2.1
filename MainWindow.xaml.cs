using dno_tes_2._1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowChromeExample
{
    public partial class MainWindow : Window
    {
        public static DateTime selecteddate;
        public static List<Note> Notes = new List<Note>();
        public static BindingList<String> Notesname = new BindingList<String>();
        public static BindingList<String> NotesText = new BindingList<String>();

        public MainWindow()
        {
            InitializeComponent();

            dtpckr.SelectedDate = DateTime.Now;

            Notes = Note.ReadFromFile<Note>(Note.jsonpath);
            if (Notes == null)
            {
                Notes = new List<Note>();
                Notes.Add(new Note("Введите имя новой заметки или выберите существующую", "Выберите дату заметки, для сохранения нажмите кнопку, для удаления нажмите кнопку", DateTime.Now.ToShortDateString()));
                Note.SaveToFile(Notes, Note.jsonpath);
                Notes = Note.ReadFromFile<Note>(Note.jsonpath);
            }
                

            update();
        }

        public void update()
        {
            Notesname.Clear();
            NotesText.Clear();
            try
            {
                foreach (var note in Notes)
                {
                    if (note.date == dtpckr.SelectedDate.Value.ToShortDateString())
                    {
                        dtpckr.Text = dtpckr.SelectedDate.Value.ToShortDateString();
                        Notesname.Add(note.name);
                        NotesText.Add(note.text);
                    }

                }
                NotesListBox.ItemsSource = Notesname;
            }
            catch { }
            
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            Note.SaveToFile(Notes, Note.jsonpath);
            SystemCommands.CloseWindow(this);
        }

        private void MainWindowStateChangeRaised(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.BorderThickness = new Thickness(8);
                RestoreButton.Visibility = Visibility.Visible;
                MaximizeButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainWindowBorder.BorderThickness = new Thickness(0);
                RestoreButton.Visibility = Visibility.Collapsed;
                MaximizeButton.Visibility = Visibility.Visible;
            }
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                NoteFieldTextBox.Text = NotesText[NotesListBox.SelectedIndex];
                NameTextBox.Text = Notesname[NotesListBox.SelectedIndex];
            }
            catch { }
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            Notes.Add(new Note(NameTextBox.Text, NoteFieldTextBox.Text, dtpckr.SelectedDate.Value.ToShortDateString()));
            Notesname.Add(NoteFieldTextBox.Text);
            NotesText.Add(NameTextBox.Text);

            Notes.RemoveAt(NotesListBox.SelectedIndex);
            Notesname.RemoveAt(NotesListBox.SelectedIndex);
            NotesText.RemoveAt(NotesListBox.SelectedIndex);

            
            update();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(NameTextBox.Text == ""))
                {
                    Notes.Add(new Note(NameTextBox.Text, NoteFieldTextBox.Text, dtpckr.SelectedDate.Value.ToShortDateString()));

                    update();
                }
            }
            catch
            {

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Notes.RemoveAt(NotesListBox.SelectedIndex);
                Notesname.RemoveAt(NotesListBox.SelectedIndex);
                NotesText.RemoveAt(NotesListBox.SelectedIndex);
                update();
            }
            catch { }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            update();
        }
    }
}
